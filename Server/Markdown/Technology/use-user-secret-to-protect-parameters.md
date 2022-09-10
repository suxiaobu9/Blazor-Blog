# 使用 User Secret 保護程式參數

## 環境

- Visual Studio 2019 Community
- .net 5

## 用途

- 避免開發階段不小心把 連線字串 還有一些 Config 的小秘密公諸於世

## 設定

在使用前要先注意一下環境變數是不是 Development (開發環境)

[![image](https://user-images.githubusercontent.com/37999690/125182626-32077300-e242-11eb-9673-54806f34de65.png "image")](https://user-images.githubusercontent.com/37999690/125182626-32077300-e242-11eb-9673-54806f34de65.png)

[![image](https://user-images.githubusercontent.com/37999690/125182630-3c297180-e242-11eb-8ae1-b2c1c92e14a4.png "image")](https://user-images.githubusercontent.com/37999690/125182630-3c297180-e242-11eb-8ae1-b2c1c92e14a4.png)

這個設定會存在 launchSettings.json

[![image](https://user-images.githubusercontent.com/37999690/125182636-464b7000-e242-11eb-98eb-1ad1a26b9f98.png "image")](https://user-images.githubusercontent.com/37999690/125182636-464b7000-e242-11eb-98eb-1ad1a26b9f98.png)

對專案點 管理使用者密碼 (Manage User Secrets)

[![image](https://user-images.githubusercontent.com/37999690/125182644-54998c00-e242-11eb-9a2c-be4d7aa2fc71.png "image")](https://user-images.githubusercontent.com/37999690/125182644-54998c00-e242-11eb-9a2c-be4d7aa2fc71.png)

產生 secrets.json

路徑會根據作業系統不同

- Windows : `%APPDATA%\microsoft\UserSecrets\<userSecretsId>\secrets.json`
- Linux : `~/.microsoft/usersecrets/<userSecretsId>/secrets.json`
- Mac : `~/.microsoft/usersecrets/<userSecretsId>/secrets.json`
- [![image](https://user-images.githubusercontent.com/37999690/125182651-6713c580-e242-11eb-8ad5-0143d651bc06.png "image")](https://user-images.githubusercontent.com/37999690/125182651-6713c580-e242-11eb-8ad5-0143d651bc06.png)

userSecretsId 會是隨機產生的

如果不同開發者要在同一個專案用同樣的 User Secrets 就要把整個 `<userSecretsId>` 資料夾複製過去

[![image](https://user-images.githubusercontent.com/37999690/125182659-74c94b00-e242-11eb-9e50-0727fb638977.png "image")](https://user-images.githubusercontent.com/37999690/125182659-74c94b00-e242-11eb-9e50-0727fb638977.png)

原始碼中的 `userSecretsId` 會記錄在 `.csproj`中

[![image](https://user-images.githubusercontent.com/37999690/125182662-7e52b300-e242-11eb-8c57-9f75133aefac.png "image")](https://user-images.githubusercontent.com/37999690/125182662-7e52b300-e242-11eb-8c57-9f75133aefac.png)

設定後兩個檔案應該要像這樣

[![image](https://user-images.githubusercontent.com/37999690/125182666-89a5de80-e242-11eb-9f86-5f06235f8aab.png "image")](https://user-images.githubusercontent.com/37999690/125182666-89a5de80-e242-11eb-9f86-5f06235f8aab.png)
