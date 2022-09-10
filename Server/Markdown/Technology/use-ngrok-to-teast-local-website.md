# 使用 Ngrok 測試地端網站

## 環境

- 先去裝好[Ngrok](https://ngrok.com/download)

- 或是利用 `Chocolatey` 裝 [https://community.chocolatey.org/packages/ngrok](https://community.chocolatey.org/packages/ngrok)

## 開始

辦好 Ngrok 的帳號並且[取得 token](https://dashboard.ngrok.com/get-started/your-authtoken)

[![image](https://user-images.githubusercontent.com/37999690/125182739-29fc0300-e243-11eb-92b3-9f346289a3d3.png "image")](https://user-images.githubusercontent.com/37999690/125182739-29fc0300-e243-11eb-92b3-9f346289a3d3.png)

驗證 Ngrok 代理`ngrok authtoken {Your Token}`

[![image](https://user-images.githubusercontent.com/37999690/125182745-37b18880-e243-11eb-96b9-5479a6f47685.png "image")](https://user-images.githubusercontent.com/37999690/125182745-37b18880-e243-11eb-96b9-5479a6f47685.png)

免費的 Ngeok 帳號不能代理 https，所以記得把 SSL 關掉

[![image](https://user-images.githubusercontent.com/37999690/125182751-4304b400-e243-11eb-8cbd-ed18bb175f08.png "image")](https://user-images.githubusercontent.com/37999690/125182751-4304b400-e243-11eb-8cbd-ed18bb175f08.png)

地端

[![image](https://user-images.githubusercontent.com/37999690/125182760-5283fd00-e243-11eb-8e9f-ba16e9fa49ec.png "image")](https://user-images.githubusercontent.com/37999690/125182760-5283fd00-e243-11eb-8e9f-ba16e9fa49ec.png)

下代理指令 `ngrok http {port} -host-header="localhost:{port}"`

[![image](https://user-images.githubusercontent.com/37999690/125182764-5e6fbf00-e243-11eb-94bd-7e12b4c1fde9.png "image")](https://user-images.githubusercontent.com/37999690/125182764-5e6fbf00-e243-11eb-94bd-7e12b4c1fde9.png)

這裡就是代理的網址

[![image](https://user-images.githubusercontent.com/37999690/125182776-6c254480-e243-11eb-9369-9b3409012a44.png "image")](https://user-images.githubusercontent.com/37999690/125182776-6c254480-e243-11eb-9369-9b3409012a44.png)
[![image](https://user-images.githubusercontent.com/37999690/125182787-7ba48d80-e243-11eb-9fec-9baaec39bbf6.png "image")](https://user-images.githubusercontent.com/37999690/125182787-7ba48d80-e243-11eb-9fec-9baaec39bbf6.png)

呼叫時 Command Line 也會顯示出被 Request 的東西

[![image](https://user-images.githubusercontent.com/37999690/125182798-8c550380-e243-11eb-8d70-8fbcc639d53b.png "image")](https://user-images.githubusercontent.com/37999690/125182798-8c550380-e243-11eb-8d70-8fbcc639d53b.png)
