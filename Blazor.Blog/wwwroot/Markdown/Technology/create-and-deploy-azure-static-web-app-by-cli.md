# 建立 Blazor 的 Azure 靜態網頁並用指令部署

## 套件

- [SWA CLI](https://azure.github.io/static-web-apps-cli/)

## 建立與發佈專案

- 建立一個 WASM 專案，沒有 .Net Core hosted 的那種
  ![image](https://user-images.githubusercontent.com/37999690/196342251-4a2954bc-5ef3-4090-8488-f87b313eaf6e.png)

- 下指令 Publish 網站

  ```powershell
  dotnet publish --output './Publish'
  ```

  ![image](https://user-images.githubusercontent.com/37999690/196342776-e731e540-287d-4f2e-afa1-7668f2685d5b.png)

  - 圖中的 wwwroot 資料夾就是發佈出來的靜態網站，準備更新上 Azure

## 建立 Azure static web app

![image](https://user-images.githubusercontent.com/37999690/196345467-8a1206c8-fbbd-4f6f-91e8-adb654bb9be3.png)

![image](https://user-images.githubusercontent.com/37999690/196345645-798f7f30-fcfe-48df-8fb0-b229a0cbd149.png)

![image](https://user-images.githubusercontent.com/37999690/196345898-caebbe3e-65b9-42ed-923c-c0aea9df621e.png)

- URL 就是靜態網頁的網址
  ![image](https://user-images.githubusercontent.com/37999690/196349278-88ac3703-9014-4959-9d7c-c347624ec56d.png)

## 安裝安裝 SWA CLI 並部署程式

- 安裝 SWA CLI

```powershell
npm install -g @azure/static-web-apps-cli
```

- SWA 部署指令 [官方文件](https://azure.github.io/static-web-apps-cli/docs/cli/swa-deploy#options)

```powershell
swa deploy `
-a './wwwroot' `
-d 'd2e2020e01219678ad*******************************************' `
--env 'Production'

```

- `-a, --app-location` 待發佈的程式資料夾路徑
  ![image](https://user-images.githubusercontent.com/37999690/196360614-45015aca-4276-4ee0-8324-ed5e5f9f3a71.png)
- `-d, --deployment-token` 靜態 Web 應用程式的部署權杖
  ![image](https://user-images.githubusercontent.com/37999690/196361679-5d2b8f7f-837f-4e6d-90e5-2a5b7fb22334.png)
- `--env` 靜態 Web 應用程式的部署環境

## 成功部署

- 成功部署後 Production 的狀態會變成 Ready
  ![image](https://user-images.githubusercontent.com/37999690/196366614-b935f0e8-12a1-45fa-a23d-d7a9a1d4cfac.png)

  ![image](https://user-images.githubusercontent.com/37999690/196366752-7e8a93cc-c1db-4761-92db-8dab5f20c8f0.png)
