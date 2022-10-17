# 將 .NET Framework 程式打包與部署到 Azure App Service 上 (Windows OS)

## 環境

- Visual Studio (msbuild)
- msdeploy

## 打包

### .NET Framework 4.8

- 利用 msbuild 的 cli 來打包程式會需要另外安裝 msdeploy

  ![image](https://user-images.githubusercontent.com/37999690/196203871-c92f71c2-c599-4f66-b0a2-fed830c1a165.png)

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
