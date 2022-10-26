# 利用指令在 IIS 建立一個 .Net 網站

## 前言

- 以下指令需要在 PowerShell 以系統管理員權限執行

## 先讓 Windows Update 更新

```powershell
# 以系統管理者權限開啟 Powershell 並執行以下指令

Install-PackageProvider -Name Nuget -MinimumVersion 2.8.5.201 -Force

Install-Module PSWindowsUpdate -Force

Import-Module PSWindowsUpdate  -Force

Get-WindowsUpdate

Install-WindowsUpdate -AcceptAll -MicrosoftUpdate -AutoReboot

```

## 確定要部署的應用程式已經放到 `C:\tmp\WebPOS` 資料夾

- 因為指令會從這邊複製程式

## 安裝 IIS、安裝 .NET Runtime、建立應用程式站台

````powershell
# 以系統管理者權限開啟 Powershell 並執行以下指令

# 啟用 IIS 相關功能
Enable-WindowsOptionalFeature `
    -FeatureName IIS-ASPNET45,`
    IIS-HttpRedirect,`
    IIS-RequestMonitor,`
    IIS-URLAuthorization,`
    IIS-IPSecurity,`
    IIS-ApplicationInit,`
    IIS-BasicAuthentication,`
    IIS-ManagementService,`
    IIS-WindowsAuthentication `
    -Online -All

# 下載 ASP.NET Core Runtime 6.0.10 (with Hosting Bundle)
$downloadHostingRuntimeExeUrl = 'https://download.visualstudio.microsoft.com/download/pr/870aa66a-733e-45fa-aecb-27aaec423f40/833d0387587b9fb35e47e75f2cfe0288/dotnet-hosting-6.0.10-win.exe'

$hostingRuntimeExePath = 'C:\tmp\dotnet-hosting-6.0.10-win.exe'

Invoke-WebRequest -Uri $downloadHostingRuntimeExeUrl -OutFile $hostingRuntimeExePath

. $hostingRuntimeExePath /install /quiet /norestart

$Installed = $False

# 確認 ASP.NET Core Runtime 6.0.10 (with Hosting Bundle) 是否安裝好了
while ($Installed -eq $False) {
    Write-Host "while"
    try {
        Write-Host "try"
        $DotNetCoreItems = Get-Item -ErrorAction Stop `
        -Path 'Registry::HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Microsoft\Updates\.NET'
        $DotNetCoreItems.GetSubKeyNames() | Where-Object `
        { $_ -Match "Microsoft .NET.*Windows Server Hosting" } |`
        ForEach-Object {
            $Installed = $True
            Write-Host "The host has installed $_"
        }
    }
    catch {
        Write-Host "catch"
        Start-Sleep -Seconds 5
    }
}

Write-Host "The host has Installed $Installed"

# 重新啟動 iis 功能
net stop was /y
net start w3svc

$WebPOSProcessPath = 'C:\tmp\WebPOS'

if (-not (test-path $WebPOSProcessPath)) {
    Write-Host "**********"
    Write-Host "**********"
    Write-Host "**********"
    Write-Host " "
    Write-Host "$WebPOSProcessPath not found !"
    Write-Host " "
    Write-Host "**********"
    Write-Host "**********"
    Write-Host "**********"
    pause
    exit
}

$iisAppPath = 'C:\inetpub'

# 將程式丟到指定的 iis 目錄
Copy-Item -Path $WebPOSProcessPath -Destination $iisAppPath -Recurse

$appName = 'WebPOS'

cd c:\Windows\System32\inetsrv

# iis 新增應用程式集區
./appcmd add apppool /name:$appName /managedRuntimeVersion:""

# iis 新增站台
./appcmd add site `
/name:$appName `
/bindings:http://*:81 `
/physicalpath:"$iisAppPath\$appName"

# 變更站台的應用程式集區
./appcmd set app "$appName/" -applicationPool:$appName

# 開啟瀏覽器
Start-Process "http://localhost:81/"

```
````
