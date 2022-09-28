# Blazor WebAssembly PWA 自動更新 Client 端程式版本

## 環境

- .Net 6
- Azure App Service

## 說明

- 最近在寫 Blazor WASM PWA (Blazor WebAssembly Progressive Web App) 時，在更新 App Service 的程式完成後，在瀏覽器上的網頁卻沒有更新，在看過文件後才發現是跟 PWA 的原理有關。

- 在預設的情況下，當使用者進入 Blazor WASM PWA 網站時，會先檢查瀏覽器是否有緩存的的程式，`第一次` 訪問網站時會將 WASM PWA 程式下載後放入瀏覽器緩存中，`第二次` 訪問網站時瀏覽器就會先使用緩存的 WASM PWA 程式，並且在伺服器有回應時再檢查 `瀏覽器 WASM PWA 程式版本` 與 `伺服器的 WASM PWA 版本` 是否相同，若是版本不同的話就會下載伺服器的 WASM PWA 程式緩存到瀏覽器。

- 但這時候使用者還是在使用舊版本的 WASM PWA 程式，也不知道已經有新版本的程式可以用了，只有在重新整理網頁或是重新打開瀏覽器才可以使用新的程式，所會需要添加一點程式來使網頁在有新版本時，可以自動重新整理網頁。

## 修改程式

### service-worker.js、service-worker.published.js

- service-worker.js 是一個虛擬的 Service worker，在開發階段會將所有的 Request 轉發到 Server，確保在開發過程中使用的程式都是從 Server 下載下來的，而不是瀏覽器的緩存版本

- service-worker.published.js 這才是在 `dotnet publish` 後的程式會使用的程式，如果要在開發環境測試的話就要把 `service-worker.published.js` 的程式全部貼到 `service-worker.js`，才可以模擬在部署環境的效果，`service-worker.js` 的內容必須保留下來，並在開發結束後復原

### 在 service-worker.published.js 加入程式碼

- 將以下程式碼加入到 `service-worker.published.js` 的 `onInstall` function

  ```javascript
  self.skipWaiting();
  ```

- 加入後的完整程式碼

  ```javascript
  // Caution! Be sure you understand the caveats before publishing an application with
  // offline support. See https://aka.ms/blazor-offline-considerations
  
  self.importScripts('./service-worker-assets.js');
  self.addEventListener('install', event => event.waitUntil(onInstall(event)));
  self.addEventListener('activate', event => event.waitUntil(onActivate(event)));
  self.addEventListener('fetch', event => event.respondWith(onFetch(event)));
  
  const cacheNamePrefix = 'offline-cache-';
  const cacheName = `${cacheNamePrefix}${self.assetsManifest.version}`;
  const offlineAssetsInclude = [ /\.dll$/, /\.pdb$/, /\.wasm/, /\.html/, /\.js$/, /\.json$/, /\.css$/, /\.woff$/, /\.png$/, /\.jpe?g$/, /\.gif$/, /\.ico$/, /\.blat$/, /\.dat$/ ];
  const offlineAssetsExclude = [ /^service-worker\.js$/ ];
  
  async function onInstall(event) {
      console.info('Service worker: Install');
  
      self.skipWaiting();
  
      // Fetch and cache all matching items from the assets manifest
      const assetsRequests = self.assetsManifest.assets
          .filter(asset => offlineAssetsInclude.some(pattern => pattern.test(asset.url)))
          .filter(asset => !offlineAssetsExclude.some(pattern => pattern.test(asset.url)))
          .map(asset => new Request(asset.url, { integrity: asset.hash, cache: 'no-cache' }));
      await caches.open(cacheName).then(cache => cache.addAll(assetsRequests));
  }
  
  async function onActivate(event) {
      console.info('Service worker: Activate');
  
      // Delete unused caches
      const cacheKeys = await caches.keys();
      await Promise.all(cacheKeys
          .filter(key => key.startsWith(cacheNamePrefix) && key !== cacheName)
          .map(key => caches.delete(key)));
  }
  
  async function onFetch(event) {
      let cachedResponse = null;
      if (event.request.method === 'GET') {
          // For all navigation requests, try to serve index.html from cache
          // If you need some URLs to be server-rendered, edit the following check to exclude those URLs
          const shouldServeIndexHtml = event.request.mode === 'navigate';
  
          const request = shouldServeIndexHtml ? 'index.html' : event.request;
          const cache = await caches.open(cacheName);
          cachedResponse = await cache.match(request);
      }
  
      return cachedResponse || fetch(event.request);
  }

  ```

### 在 `wwwroot` 的目錄下新增 `service-worker-registrator.js`

```javascript
window.updateAvailable = new Promise((resolve, reject) => {
  if (!("serviceWorker" in navigator)) {
    const errorMessage = `This browser doesn't support service workers`;
    console.error(errorMessage);
    reject(errorMessage);
    return;
  }

  navigator.serviceWorker
    .register("/service-worker.js")
    .then((registration) => {
      console.info(`Service worker registration successful (scope: ${registration.scope})`);

      registration.onupdatefound = () => {
        const installingServiceWorker = registration.installing;
        installingServiceWorker.onstatechange = () => {
          if (installingServiceWorker.state === "installed") {
            resolve(!!navigator.serviceWorker.controller);
          }
        };
      };
    })
    .catch((error) => {
      console.error("Service worker registration failed with error:", error);
      reject(error);
    });
});

window.registerForUpdateAvailableNotification = (caller, methodName) => {
  window.updateAvailable.then((isUpdateAvailable) => {
    if (isUpdateAvailable) {
      caller.invokeMethodAsync(methodName).then();
    }
  });
};
```

### 修改 index.html

將以下程式碼

```html
<script>navigator.serviceWorker.register("service-worker.js");</script>
```

換成

```html
<script src="service-worker-registrator.js"></script>
```

### MainLayout.razor 加入以下程式碼

```csharp
@inject IJSRuntime JSRuntime
@inject NavigationManager NavigationManager

@code
{
  protected override async Task OnInitializedAsync()
  {
      await RegisterForUpdateAvailableNotification();
  }

  private async Task RegisterForUpdateAvailableNotification()
  {
      await JSRuntime.InvokeAsync<object>(
          identifier: "registerForUpdateAvailableNotification",
          DotNetObjectReference.Create(this),
          nameof(OnUpdateAvailable));
  }

  [JSInvokable(nameof(OnUpdateAvailable))]
  public Task OnUpdateAvailable()
  {
      NavigationManager.NavigateTo("", true);
  }
}

```

## 參考資料

- [Blazor WASM PWA — Adding a “New Update Available” notification](https://whuysentruit.medium.com/blazor-wasm-pwa-adding-a-new-update-available-notification-d9f65c4ad13)
