# Windows 環境好用的遠端桌面管理工具 - mRemoteNG (Multi-Remote Next Generation Connection Manager)

最近因為設備多了且同時有 Windows 與 Linux 的設備，只有 Windows 還可以用 [Microsoft Remote Desktop](https://apps.microsoft.com/store/detail/microsoft-%E9%81%A0%E7%AB%AF%E6%A1%8C%E9%9D%A2/9WZDNCRFJ3PS?hl=zh-tw&gl=tw&rtc=1) 來管理 RDP，如果要加上管理 SSH 連線就要另外找工具了。 [mRemoteNG](https://mremoteng.org/) 就符合我的需求，可以管理 RDP 與 SSH 且介面簡單，同時又是開源以及 **免費** 的方。

## 安裝

### 利用 Winget 安裝

```ps1
winget install PuTTY.PuTTY
winget install mRemoteNG.mRemoteNG
```

### 自行下載並安裝

1. [下載 mRemoteNG](https://mremoteng.org/download)
1. [下載 PuTTY](https://www.chiark.greenend.org.uk/~sgtatham/putty/latest.html)

## 畫面

![image](https://github-production-user-asset-6210df.s3.amazonaws.com/37999690/262383775-67fbdf0d-c83f-4106-931e-427d04b2db2a.png)

- 區域 1 是所有已經設定好的連線
- 區域 2 是連線的設定區
- port 有自己的設定區，不用打在主機名稱/IP 上

## 將 OpenSSH 產生的私鑰轉換成 PuTTY 的私鑰，並登入 SSH

1. 啟動 PuTTYgen 並讀取私鑰

   ```powershell
    puttygen path/to/id_ed25519
   ```

2. 將 PPK 版本設定成第 2 版

   ![image](https://github-production-user-asset-6210df.s3.amazonaws.com/37999690/262405176-6eadd7cc-e001-47bb-b9ab-dbe68d854b18.png)
   ![image](https://github-production-user-asset-6210df.s3.amazonaws.com/37999690/262405806-1cd3cb1c-141f-436c-ac38-08549bdf7b85.png)

3. 點下 `Save private key` 產生並儲存 ppk

   ![image](https://github-production-user-asset-6210df.s3.amazonaws.com/37999690/262393275-cbfb40f5-b3b5-4a9a-89b1-a0aae9eb070b.png)

4. 設定 PuTTY 工作階段

   - 進入設定畫面

     ![image](https://github-production-user-asset-6210df.s3.amazonaws.com/37999690/262396969-8747c024-38ee-43e5-bb6b-1b081f35e9be.png)

   - 進入到 PuTTY 設定畫面

     ![image](https://github-production-user-asset-6210df.s3.amazonaws.com/37999690/262397904-e61a78af-f1b2-4c46-ba45-f3ae63611a18.png)

   - 選擇剛剛產生的 ppk 檔

     ![image](https://github-production-user-asset-6210df.s3.amazonaws.com/37999690/262398818-7dee56c5-d65c-44ba-921e-1c973685d1de.png)

   - 設定 `ip` / `port` / `session name` 並儲存

     > ip 與 port 可選擇性設定

     ![image](https://github-production-user-asset-6210df.s3.amazonaws.com/37999690/262400256-065e1ee2-1adc-48b7-9cc8-b77011a0f16b.png)

   - 設定連線資訊，協定選擇 `SSH 版本 2`

   - 設定 `名稱` `主機名稱/IP` `使用者名稱` `密碼` `port` `PuTTY 工作階段`

     > 密碼為私鑰的密碼，有設定私鑰密碼才需要

     ![image](https://github-production-user-asset-6210df.s3.amazonaws.com/37999690/262406924-cb994a8b-8bed-4ee8-8b6c-eb1b0eb34537.png)

全都設定完成後應該就可以正常連線了
