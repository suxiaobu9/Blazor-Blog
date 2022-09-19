# 將 .NET 程式打包與佈署到 Azure App Service 上的幾種方式 (Windows OS)

## 環境

- 已安裝 Visual Studio (msbuild)
- .NET 6 SDK
- msdeploy
- Azure CLI

## 打包

### .NET Framework 4.8

- 利用 msbuild 的 cli 來打包程式會需要另外安裝 msdeploy

  ![image](https://user-images.githubusercontent.com/37999690/190894994-8df0f9bc-d398-4ecc-8f23-1b52f25d9305.png)

- 指令

  ```powershell

  # 註 1
  $platform = 'Any CPU'
  # 註 2
  $configuration = 'Debug'
  $sln = 'D:\SourceCode\dotnet_framework_48_mvc\dotnet_framework_48_mvc.sln'
  # 打包好檔案的路徑
  $releaseDirPath = 'D:\Publish\dotnet_framework_48_mvc'

  # 移除發佈目錄下的所有檔案
  Remove-Item -Path "$releaseDirPath\*" -Recurse

  # 開頭的這個 . 很重要，不能拿掉
  . 'C:\Program Files\Microsoft Visual Studio\2022\Community\Msbuild\Current\Bin\MSBuild.exe' `
        $sln `
        /p:platform=$platform `
        /p:configuration=$configuration `
        <# 必須要有安裝 msdeploy DeployOnBuild 才有意義 #> `
        /p:DeployOnBuild=true `
        /p:PackageLocation=$releaseDirPath `
        <# 避免發佈時置換掉 web.config 的連線資訊 #> `
        /p:AutoParameterizationWebConfigConnectionStrings=False

  ```

  - 註 1 、 註 2
    > \$platform、$configuration 的設定必須存在於 sln 或是 csproj，需要看指定 build 哪個檔案來決定要給什麼
    >
    > ![image](https://user-images.githubusercontent.com/37999690/190902988-23095ca3-2136-48f7-9f6f-38a5cacbc155.png)
    >
    > ![image](https://user-images.githubusercontent.com/37999690/190902973-ba6ae06a-e749-4bd8-8639-daeb8a1a722d.png)

### .net 6

- 需要先安裝 .net 6 SDK
  - <https://dotnet.microsoft.com/en-us/download/dotnet/6.0>
- 指令

  ```powershell

  $sln = 'D:\SourceCode\dotnet_6_mvc\dotnet_6_mvc.sln'
  $output = 'D:\Publish\dotnet_6_mvc'
  $webAppZip = "$output\webApp.zip"
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

### 暫停網站後再更新 (app_offline.htm)

- 要先取得發行設定檔

  ![image](https://user-images.githubusercontent.com/37999690/190939121-ec8ec9fd-f630-4fd1-b382-dfc2122d995d.png)

- 需要發行設定檔的這些資料

  ![image](https://user-images.githubusercontent.com/37999690/190940085-9c160dd8-7a62-4f18-955b-b3033f6bf8f3.png)

- 指令

  ```powershell

  $releaseDirPath = "D:\Publish\dotnet_framework_48_mvc"
  $deployExePath = "C:\Program Files (x86)\IIS\Microsoft Web Deploy V3\msdeploy.exe"
  # 網頁的專案名稱
  $webAppName = 'dotnet_framework_48_mvc'
  $publishUrl = 'ari-dotnet-framework-48-demo.scm.azurewebsites.net:443'
  $msdeploySite = 'ari-dotnet-framework-48-demo'
  $userName = '$ari-dotnet-framework-48-demo'
  $userPWD = 'MLueL2h****************'
  $remoteServerAuthInfo = "computerName='https://$publishUrl/msdeploy.axd?site=$msdeploySite',userName='$userName',password='$userPWD'"

  Remove-Item -Path "$releaseDirPath\$webAppName\*" -Recurse

  Expand-Archive -LiteralPath "$releaseDirPath\$webAppName.zip" "$releaseDirPath\$webAppName"

  $appWebContent = "$releaseDirPath\$webAppName\Content\D_C\SourceCode\$webAppName\$webAppName\obj\Debug\Package\PackageTmp"

  $appOffline = "$appWebContent\App_Offline_Close.htm"

  # 將 App_Offline_Close.htm 上傳到雲端，並且把名子改成 app_offline.htm
  . $deployExePath `
    -verb:sync `
    -source:contentPath=$appOffline `
    -dest:contentPath="$msdeploySite/app_offline.htm",$remoteServerAuthInfo,authtype="Basic",includeAcls="False"

  # 將網站上傳到雲端
  . $deployExePath `
    -verb:sync `
    -source:contentPath=$appWebContent `
    -dest:contentPath=$msdeploySite,$remoteServerAuthInfo,authtype="Basic",includeAcls="False" `
    -retryAttempts:10 `
    -retryInterval:3000 `
    -enableRule:DoNotDeleteRule

  # 刪除 app_offline.htm
  . $deployExePath `
    -verb:delete `
    -dest:contentPath=$msdeploySite/app_offline.htm,$remoteServerAuthInfo,authtype="Basic",includeAcls="False"

  ```

### 利用 App Service slot 更新網站

- App Service Plan 定價層至少需要 S1
- 需要先 assign 權限給 App Service

  ![image](https://user-images.githubusercontent.com/37999690/190944258-f1bccb4a-0196-4d9b-84e7-730ec65680a7.png)

  ![image](https://user-images.githubusercontent.com/37999690/190944370-58da045f-098b-41c2-8391-ce81bf50922b.png)

  ![image](https://user-images.githubusercontent.com/37999690/190944418-66726aaf-eaaa-459f-9b5b-b700782b90bd.png)

  ![image](https://user-images.githubusercontent.com/37999690/190948506-01a4fe5d-d97c-4cb5-ad49-69b54a9a76d7.png)

  - 需要把下圖的值記錄下來，發布程式時會用到

  ![image](https://user-images.githubusercontent.com/37999690/190948645-ef624d24-8d15-4581-aad5-b292df4e4006.png)

  ![image](https://user-images.githubusercontent.com/37999690/190948829-da219d21-7649-48d2-a593-a097e8ef27f7.png)

  ![image](https://user-images.githubusercontent.com/37999690/190948881-2f8a1700-a848-453d-b8ff-63c8f39c94be.png)

  ![image](https://user-images.githubusercontent.com/37999690/190948984-cd00f5de-5413-4007-b9eb-11e1a57bacd1.png)

  ![image](https://user-images.githubusercontent.com/37999690/190949004-624c4fc9-9e2c-4b47-b9b6-08fb10cd9974.png)

  ![image](https://user-images.githubusercontent.com/37999690/190949041-965ee4e3-b45d-4571-9f4e-bb4f960c81c6.png)

  ![image](https://user-images.githubusercontent.com/37999690/190949072-cc519e2e-58af-4ac5-bfd6-a309ccfb6a49.png)

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
