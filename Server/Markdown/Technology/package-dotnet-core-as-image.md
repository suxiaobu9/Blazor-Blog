# 將.net core 包成 Image

## 環境

1. Visual Studio 2019 Community
2. Docker version 20.10.2
3. .net core 5
4. PowerShell

## 步驟

1. 由 Visual Studio 建立 DockerFile

   [![image](https://user-images.githubusercontent.com/37999690/125169773-46b21f80-e1de-11eb-8dba-af60b0a6146a.png "image")](https://user-images.githubusercontent.com/37999690/125169773-46b21f80-e1de-11eb-8dba-af60b0a6146a.png)
   [![image](https://user-images.githubusercontent.com/37999690/125169806-6d705600-e1de-11eb-91bd-25df557afd07.png "image")](https://user-images.githubusercontent.com/37999690/125169806-6d705600-e1de-11eb-91bd-25df557afd07.png)

2. 將 DockerFile 移動至與 sln 相同目錄

   [![image](https://user-images.githubusercontent.com/37999690/125169848-a01a4e80-e1de-11eb-989b-f22b563f27a7.png "image")](https://user-images.githubusercontent.com/37999690/125169848-a01a4e80-e1de-11eb-989b-f22b563f27a7.png)

3. 建置 Image

   - 將 PowerShell 路徑移動到 sln 的目錄下，並下指令

   ```powershell
     PS docker build -t {tag name} .
   ```

   [![image](https://user-images.githubusercontent.com/37999690/125169864-bfb17700-e1de-11eb-96f8-9669bee057e2.png "image")](https://user-images.githubusercontent.com/37999690/125169864-bfb17700-e1de-11eb-96f8-9669bee057e2.png)
   [![image](https://user-images.githubusercontent.com/37999690/125169894-e2dc2680-e1de-11eb-8f84-0002b6463613.png "image")](https://user-images.githubusercontent.com/37999690/125169894-e2dc2680-e1de-11eb-8f84-0002b6463613.png)
   [![image](https://user-images.githubusercontent.com/37999690/125169908-f38c9c80-e1de-11eb-954a-f62ac5e1243e.png "image")](https://user-images.githubusercontent.com/37999690/125169908-f38c9c80-e1de-11eb-954a-f62ac5e1243e.png)

4. 將 Image 執行成 Containers

   [![image](https://user-images.githubusercontent.com/37999690/125169930-04d5a900-e1df-11eb-8cf4-d224688b24ac.png "image")](https://user-images.githubusercontent.com/37999690/125169930-04d5a900-e1df-11eb-8cf4-d224688b24ac.png)
   [![image](https://user-images.githubusercontent.com/37999690/125169969-3189c080-e1df-11eb-8ada-6dfaa52287dc.png "image")](https://user-images.githubusercontent.com/37999690/125169969-3189c080-e1df-11eb-8ada-6dfaa52287dc.png)
   [![image](https://user-images.githubusercontent.com/37999690/125169976-3d758280-e1df-11eb-90d8-37a5f2b802ea.png "image")](https://user-images.githubusercontent.com/37999690/125169976-3d758280-e1df-11eb-90d8-37a5f2b802ea.png)
   [![image](https://user-images.githubusercontent.com/37999690/125169987-4a927180-e1df-11eb-9a94-6506de5fad31.png "image")](https://user-images.githubusercontent.com/37999690/125169987-4a927180-e1df-11eb-9a94-6506de5fad31.png)

5. Push Image To Docker Hub

   - 為 Image 加上 Tag

   ```powershell
     # docker tag {Image名稱} {docker hub帳號}\{Image名稱}
     PS docker tag dotnetcore1 w21qasde3\dotnetcore1
   ```

   [![image](https://user-images.githubusercontent.com/37999690/125170011-6eee4e00-e1df-11eb-996c-592327a8b8fd.png "image")](https://user-images.githubusercontent.com/37999690/125170011-6eee4e00-e1df-11eb-996c-592327a8b8fd.png)
   [![image](https://user-images.githubusercontent.com/37999690/125170016-7a417980-e1df-11eb-9c83-3e4102c0af44.png "image")](https://user-images.githubusercontent.com/37999690/125170016-7a417980-e1df-11eb-9c83-3e4102c0af44.png)

   - 登入 Docker Hub

   ```powershell
     PS docker login --username {username} --password {password}
   ```

   [![image](https://user-images.githubusercontent.com/37999690/125170036-96ddb180-e1df-11eb-8d22-e4c5e0d5a145.png "image")](https://user-images.githubusercontent.com/37999690/125170036-96ddb180-e1df-11eb-8d22-e4c5e0d5a145.png)

   - Push to docker hub

   ```powershell
     PS docker push w21qasde3\dotnetcore1
   ```

   [![image](https://user-images.githubusercontent.com/37999690/125170073-b543ad00-e1df-11eb-931d-1ebc65ffe800.png "image")](https://user-images.githubusercontent.com/37999690/125170073-b543ad00-e1df-11eb-931d-1ebc65ffe800.png)
   [![image](https://user-images.githubusercontent.com/37999690/125170090-c4c2f600-e1df-11eb-98c0-99116f7dcda6.png "image")](https://user-images.githubusercontent.com/37999690/125170090-c4c2f600-e1df-11eb-98c0-99116f7dcda6.png)
   [![image](https://user-images.githubusercontent.com/37999690/125170098-cdb3c780-e1df-11eb-91f7-d50af27f0063.png "image")](https://user-images.githubusercontent.com/37999690/125170098-cdb3c780-e1df-11eb-91f7-d50af27f0063.png)

6. 停止與移除 Container

   ```powershell
     PS docker container ls
     PS docker stop donetcore1
     PS docker container tm dotnetcore1
   ```

   [![image](https://user-images.githubusercontent.com/37999690/125170129-ede38680-e1df-11eb-8404-ebda19eb9380.png "image")](https://user-images.githubusercontent.com/37999690/125170129-ede38680-e1df-11eb-8404-ebda19eb9380.png)

7. 移除 Image

   ```powershell
     PS docker images
     PS docker image rm dotnetcore1
     PS docker image rm w21qasde3\dotnetcore1
   ```

   [![image](https://user-images.githubusercontent.com/37999690/125170144-00f65680-e1e0-11eb-9e9b-d0979181da19.png "image")](https://user-images.githubusercontent.com/37999690/125170144-00f65680-e1e0-11eb-9e9b-d0979181da19.png)
   [![image](https://user-images.githubusercontent.com/37999690/125170149-0c498200-e1e0-11eb-8796-eb7e29ebe6ae.png "image")](https://user-images.githubusercontent.com/37999690/125170149-0c498200-e1e0-11eb-8796-eb7e29ebe6ae.png)

8. 從 Docker Hub 拉 Image 測試

   ```powershell
     PS docker run -d -p 8080:80 --name dotnetcore w21qasde3\dotnetcore1
   ```

   [![image](https://user-images.githubusercontent.com/37999690/125170168-300cc800-e1e0-11eb-8ef9-2df2dbb83505.png "image")](https://user-images.githubusercontent.com/37999690/125170168-300cc800-e1e0-11eb-8ef9-2df2dbb83505.png)
   [![image](https://user-images.githubusercontent.com/37999690/125170177-3ac75d00-e1e0-11eb-8ed7-aa045ce888d6.png "image")](https://user-images.githubusercontent.com/37999690/125170177-3ac75d00-e1e0-11eb-8ed7-aa045ce888d6.png)
