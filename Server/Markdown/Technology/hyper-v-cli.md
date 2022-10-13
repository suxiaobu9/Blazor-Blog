# 利用 Powershell 新增、匯入、匯出 Hyper-V 虛擬機

## 前言

- PowerShell 指令皆需要再 `系統管理員` 權限下執行

## 新增

```powershell
$date = Get-Date -Format "yyyy-MM-dd-HH-mm-ss"

# VM 名稱
$VMName = "Win-10-$date"
# 虛擬交換器名稱
$SWName = 'Default Switch'
# 放 VM 檔案的位子
$VMPath = 'D:\Hyper'
# OS 安裝檔路徑
$ISOPath = "D:\Windows.iso"

$GiB = 1024 * 1024 * 1024
# 硬碟大小
$disk = 100 * $GiB
# 啟動記憶體
$memory = 2 * $GiB
# CPU 數量
$CPUCount = 5

$VM = New-VM -Name $VMName `
    -SwitchName $SWName `
    -Path $VMPath `
    -NewVHDPath "${VMPath}\${VMName}\Virtual Hard Disks\${VMName}.vhdx" `
    -MemoryStartupBytes $memory `
    -Generation 1 `
    -NewVHDSizeBytes $disk

# 設定 CPU 數量
$VM | Set-VMProcessor -Count $CPUCount
# 啟動 CPU 支援巢狀虛擬化
$VM | Set-VMProcessor -ExposeVirtualizationExtensions $true
# 啟動動態記憶體
$VM | Set-VMMemory -DynamicMemoryEnabled $true
# 設定 OS 安裝檔路徑
$VM | Get-VMDvdDrive | Set-VMDvdDrive -Path $ISOPath
# 啟動 VM
$VM | Start-VM
# 連線至 VM
VMConnect $env:COMPUTERNAME $VMName

```

## 匯出

```powershell
Export-VM -Name 'Win-10-2022-10-11-17-12-03' -Path 'D:\Back'
```

## 匯入

```powershell
$date = Get-Date -Format "yyyy-MM-dd-HH-mm-ss"
# 複製後的 VM 名稱
$VMName = "Win-10-$date"
# 複製後的 VM 存放位子
$VMPath = 'D:\Hyper'

$VM = Import-VM `
    -Path "D:\Back\Win-10-2022-10-11-17-12-03\Virtual Machines\D090B116-61F4-4A5C-976A-432C1BC6AC7F.vmcx" `
    -Copy `
    -GenerateNewId `
    -VhdDestinationPath "$VMPath\$VMName" `
    -VirtualMachinePath "$VMPath\$VMName"

# 重新命名匯入的 VM
Rename-VM $VM -NewName $VMName

$VM | Start-VM

# 連線至 VM
VMConnect $env:COMPUTERNAME $VMName

```
