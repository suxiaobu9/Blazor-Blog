# 檢查 PowerShell Script 是否是以管理員權限執行

在重灌電腦時為了方便所以就寫了一些 `PowerShell` 語法來簡化人為操作的動作以及錯誤，但是某些語法會需要 `Administrator` 才可以正常執行，對 `.ps1` 檔案按右鍵又沒有以管理員權限執行的選項。

## 語法

```powershell
# 驗證是否為 Administrator 權限

function Test-Administrator {
    Write-Host 'Test-Administrator'
    $user = [Security.Principal.WindowsIdentity]::GetCurrent();
    (New-Object Security.Principal.WindowsPrincipal $user).IsInRole([Security.Principal.WindowsBuiltinRole]::Administrator)
}

```

```powershell
# 待執行的指令

# 將工作目錄設定到 .ps1 的資料夾
Set-Location ([System.IO.Path]::GetDirectoryName($PSCommandPath))

if (Test-Administrator) {
    # 這邊執行需要執行的語法
    Write-Host "I am administrator !"
}
else {
    # 以管理員權限執行 .ps1 檔案
    Start-Process Powershell -Verb RunAs -ArgumentList "$PSCommandPath"
}

```

當然也可以用 `Start-Process -Verb RunAs` 來直接以管理員權限執行 `.ps1` 檔案

```powershell
# InstallApp.ps1

choco install googlechrome -y

```

```powershell
# AddKeyboard.ps1

$UserLanguageList = New-WinUserLanguageList -Language 'zh-TW'
$UserLanguageList.Add('en-US')
Set-WinUserLanguageList -LanguageList $UserLanguageList -Force
Set-WinDefaultInputMethodOverride -InputTip '0409:00000409'

```

```powershell
# batch.ps1

Start-Process Powershell -Verb RunAs -ArgumentList ".'$(Get-Location)\InstallApp.ps1'"
Start-Process Powershell -Verb RunAs -ArgumentList ".'$(Get-Location)\AddKeyboard.ps1'"

```
