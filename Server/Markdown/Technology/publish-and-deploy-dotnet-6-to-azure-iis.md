# 將 .NET 6 程式打包與部署到 Azure App Service 上 (Windows OS)

## 環境

- .NET 6 SDK
- Azure CLI

## 打包

### .NET 6

- 需要先安裝 .net 6 SDK
  - <https://dotnet.microsoft.com/en-us/download/dotnet/6.0>
- 指令

  ```powershell

  $sln = 'D:\SourceCode\dotnet_6_mvc\dotnet_6_mvc.sln'
  $output = 'D:\Publish\dotnet_6_mvc'
  $buildConfiguration = 'Release'
  $EnvironmentName = 'Production'

  Remove-Item -Path "$output\*" -Recurse

  dotnet publish `
      $sln `
      --configuration $buildConfiguration `
      --output $output `
      /p:EnvironmentName=$EnvironmentName

  ```

## 更新網站

### 利用 App Service slot 更新網站

- App Service Plan 定價層至少需要 S1
- 需要先 assign 權限給 App Service

  - [Azure 新增 User Assign 給服務，以及取得 Key](/article/assigh-user-role-to-azure-service)

- 指令

  - 需要先安裝 [Azure CLI](https://learn.microsoft.com/en-us/cli/azure/install-azure-cli-windows?tabs=powershell) 需要以系統管理員開啟 Powershell
  - 安裝完成後需要重新開啟 Powershell

    ```powershell
    Set-ExecutionPolicy -ExecutionPolicy RemoteSigned -Scope CurrentUser
    $ProgressPreference = 'SilentlyContinue';
    Invoke-WebRequest `
    -Uri https://aka.ms/installazurecliwindows `
    -OutFile .\AzureCLI.msi; Start-Process msiexec.exe `
    -Wait `
    -ArgumentList '/I AzureCLI.msi /quiet';
    rm .\AzureCLI.msi
    ```

  ```powershell

  $output = 'D:\Publish\dotnet_6_mvc'
  $webAppZip = "$output\dotnet_6_mvc.zip"

  # 註 3
  $tenant = 'miniasp.onmicrosoft.com'

  # 註 4
  $appId = '8afd77b8-8ee6-4e69-b43e-8cb07840218f'
  # 註 5
  $pw = 'KtQ8Q****************************'

  # 註 6
  $appServiceName = 'ari-dotnet-6-demo'
  # 註 7
  $resourceGroup = 'projectdemo'
  $slot = 'staging'

  # 壓縮檔案
  Compress-Archive "$output\*" $webAppZip -F

  #登入 azure
  az login `
      --service-principal `
      -u $appId `
      -p $pw `
      --tenant $tenant

  # 刪除舊的 slot，如果沒有的話會自動跳過
  az webapp `
      deployment `
      slot `
      delete `
      --name $appServiceName `
      --resource-group $resourceGroup `
      --slot $slot

  # 建立 slot
  az webapp `
      deployment `
      slot `
      create `
      --name $appServiceName `
      --resource-group $resourceGroup `
      --configuration-source $appServiceName `
      --slot $slot

  # 將 slot 設定成 auto swap，slot 的程式更新後會自動暖機、並 swap slot
  # 設定同開啟 slot 組態中的 一般設定 > 部署位置 > 啟用自動交換
  az webapp `
      deployment `
      slot `
      auto-swap `
      --name $appServiceName `
      --resource-group $resourceGroup `
      --slot $slot

  # 將打包好的程式更新到 slot
  az webapp `
      deploy `
      --resource-group $resourceGroup `
      --name $appServiceName `
      --src-path $webAppZip `
      --type zip `
      --async true `
      --clean true `
      --slot $slot

  ```

  - 註 3
    ![image](https://user-images.githubusercontent.com/37999690/190952838-9f848fce-231e-4af1-85aa-808a6ab5e4f6.png)

  - 註 4
    ![image](https://user-images.githubusercontent.com/37999690/190952970-5b34cd7b-ee8a-4f99-9d8c-7cbf356914b6.png)

  - 註 5
    ![image](https://user-images.githubusercontent.com/37999690/190953018-58e93eff-e8ba-456f-b672-7abe0a1b54cf.png)

  - 註 6、註 7
    ![image](https://user-images.githubusercontent.com/37999690/190953082-2f09ffda-cfc4-4bd6-9838-5e3e705b8823.png)
