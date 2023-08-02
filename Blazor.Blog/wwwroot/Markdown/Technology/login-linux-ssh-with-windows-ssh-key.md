# 從 Windows 利用金鑰免密碼登入 Linux SSH

## 建立 Linux 的 OpenSSH Server

- 更新系統並安裝 OpenSSH 伺服器

  ```bash
  sudo apt update
  sudo apt upgrade
  sudo apt install openssh-server
  ```

- 啟動 OpenSSH 伺服器

  ```bash
  # 啟動 OpenSSH
  sudo service ssh start

  # 看 OpenSSH 有沒有啟動
  sudo service ssh status

  mkdir ~/.ssh
  ```

- 設置防火牆

  ```bash
  # 啟動防火牆
  sudo ufw enable

  # 允許 SSH 的預設 port 22
  sudo ufw allow 22

  # 移除 port
  # sudo ufw delete allow 22

  ```

## Windows 建立 Key 並複製到 SSH Service

- 生成 ed25519 Key

  ```ps1
  ssh-keygen -t ed25519
  # 如果需要相容舊系統，可以用 RSA
  # ssh-keygen -t rsa -b 4096
  ```

  ![image](https://github.com/suxiaobu9/JobSeeker/assets/37999690/6af3b000-d99c-4fc4-9633-047e1fbe913d)

- 將 Key 複製到 Linux 中

  ```ps1
  # ~/.ssh/authorized_keys 不存在，用這指令
  scp "$env:USERPROFILE\.ssh\id_ed25519.pub" username@server_ip:~/.ssh/authorized_keys

  # ~/.ssh/authorized_keys 存在，用這指令
  cat "$env:USERPROFILE\.ssh\id_ed25519.pub" | ssh username@server_ip 'cat >> ~/.ssh/authorized_keys'
  ```

- 啟動 ssh-agent 並將 Key 交給 ssh-agent 保管

  - 以 `管理員權限` 啟動 powershell

  ```ps1
  # 將 ssh-agent 設定為開機自動啟動
  Get-Service ssh-agent | Set-Service -StartupType Automatic

  # 啟動 ssh-agent 服務
  Start-Service ssh-agent

  # 將 Key 交給 ssh-agent 保管，如果 Key 有設定密碼，下一步輸入完密碼後就完成了
  # 託管給 ssh-agent 的 Key 無法再取出
  ssh-add $env:USERPROFILE\.ssh\id_ed25519
  ```

- 免密碼登入進遠端 server

  ```bash
  ssh username@server_ip
  ```

- 備份 `id_ed25519`、`id_ed25519.pub` 並移除
  - 原本的 `id_ed25519`、`id_ed25519.pub` 會放在 `$env:USERPROFILE\.ssh\` 目錄下，是很好竊取的目標，所以需要將他備份到其他地方，並將 `$env:USERPROFILE\.ssh\` 目錄下的 `id_ed25519`、`id_ed25519.pub` 移除

## 參考資料

- [從 Windows 使用金鑰免密碼登入 SSH/SFTP/SCP](https://blog.darkthread.net/blog/ssh-key-auth/)
