# Vue 2 多環境開發

## 環境

1. Vue 2.x
2. Webpack 4.x

## 資料夾結構

[![image](https://user-images.githubusercontent.com/37999690/125182560-9d9d1080-e241-11eb-8810-79dac7c64138.png "image")](https://user-images.githubusercontent.com/37999690/125182560-9d9d1080-e241-11eb-8810-79dac7c64138.png)

## 檔案內容

```js
// 檔名 .env
// 這是開發環境用的

NODE_ENV = "development";
VUE_APP_BASEAPIURL = "https://localhost:5001/";
```

```js
// 檔名 .env.test
// 這是測試環境用的

NODE_ENV = "test";
VUE_APP_BASEAPIURL = "https://test.xxxxx/";
```

```js
// 檔名 .env.prod
// 這是正式環境用的

NODE_ENV = "production";
VUE_APP_BASEAPIURL = "https://production.xxxxx/";
```

```json
// package.json設置

... 以上略 ...

"scripts": {
   "===== 開發 環境 =====": "npm run serve",
   "serve": "vue-cli-service serve",
   "build": "vue-cli-service build",

   "===== 測試 環境 =====": "npm run build:test",
   "serve:test": "vue-cli-service serve --mode test",
   "build:test": "vue-cli-service build --mode test",

   "===== 正式 環境 =====": "npm run build:prod",
   "serve:prod": "vue-cli-service serve --mode prod",
   "build:prod": "vue-cli-service build --mode prod",

   "lint": "vue-cli-service lint"
 },

... 以下略 ...

```

## 啟動命令

```js
npm run serve // 啟動 開發環境

npm run serve:test // 啟動 測試環境
npm run build:test // 打包 測試環境

npm run serve:prod // 啟動 正式環境
npm run build:prod // 打包 正式環境
```

## 用法

```js
// 開發環境

console.log(process.env.VUE_APP_BASEAPIURL);
// https://localhost:5001/
```

```js
// 測試環境

console.log(process.env.VUE_APP_BASEAPIURL);
// https://test.xxxxx/
```

```js
// 正式環境

console.log(process.env.VUE_APP_BASEAPIURL);
// https://production.xxxxx/
```
