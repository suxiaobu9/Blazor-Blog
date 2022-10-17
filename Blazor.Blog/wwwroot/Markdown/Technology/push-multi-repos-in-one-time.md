# Git 一次性 Push 至多個 Remote Repository

## 設定 Remote url

1. 下指令設定

   - 原本的專案 Remote 設定

     [![image](https://user-images.githubusercontent.com/37999690/128452504-4b1c0bee-18c0-459a-a3b0-1cf7490b9d09.png "image")](https://user-images.githubusercontent.com/37999690/128452504-4b1c0bee-18c0-459a-a3b0-1cf7490b9d09.png)

   - 加入其他 remote

     ```cmd
     git remote set-url --add --push origin {https://gitserver/repository.git}
     ```

     > 如果原本已有 remote 設定，且沒設定過 pushurl (上述的指令)，那麼原本的設定會被覆蓋掉
     > [![image](https://user-images.githubusercontent.com/37999690/128454622-26c59a13-5ecd-40ae-829c-bbf99e122ed5.png "image")](https://user-images.githubusercontent.com/37999690/128454622-26c59a13-5ecd-40ae-829c-bbf99e122ed5.png)
     > 後面會說哪時候會被覆蓋 ( \*註 1 Q )

   - 設定好會長這樣
     [![image](https://user-images.githubusercontent.com/37999690/128454859-ccdb53a2-4453-4478-ab41-723f1dd4048a.png "image")](https://user-images.githubusercontent.com/37999690/128454859-ccdb53a2-4453-4478-ab41-723f1dd4048a.png)

2. 直接改 config

   - 檔案位置

     [![image](https://user-images.githubusercontent.com/37999690/128455039-a535e6a3-6b74-4fcb-872f-6bb341ba353a.png "image")](https://user-images.githubusercontent.com/37999690/128455039-a535e6a3-6b74-4fcb-872f-6bb341ba353a.png)

   - 主要需要對這邊動手腳

     [![image](https://user-images.githubusercontent.com/37999690/128455249-9f56b391-a4c1-433e-aaf9-a57679a52636.png "image")](https://user-images.githubusercontent.com/37999690/128455249-9f56b391-a4c1-433e-aaf9-a57679a52636.png)

   - 加入 `pushurl`

     ```powershell
      # 原本的remote url
      pushurl = https://github.com/suxiaobu9/mult-repo1.git
      # 備份用的 remote url
      pushurl = https://gitlab.com/bu9_tw/mult-repo2.git
     ```

     [![image](https://user-images.githubusercontent.com/37999690/128455843-e66ea5a9-0f09-4410-afea-06493e33051a.png "image")](https://user-images.githubusercontent.com/37999690/128455843-e66ea5a9-0f09-4410-afea-06493e33051a.png)

     > ( \*註 1 A ) 當這邊沒有 pushurl 時，下設定 remote push url 的指令時會覆蓋掉原本的設定
     >
     > [![image](https://user-images.githubusercontent.com/37999690/128455676-9ca9c730-ef44-43cc-9f74-a62806708e1d.png "image")](https://user-images.githubusercontent.com/37999690/128455676-9ca9c730-ef44-43cc-9f74-a62806708e1d.png)
     >
     > [![image](https://user-images.githubusercontent.com/37999690/128455633-a8b28fa6-f30f-4b2f-86d4-817c3a3400cf.png "image")](https://user-images.githubusercontent.com/37999690/128455633-a8b28fa6-f30f-4b2f-86d4-817c3a3400cf.png)

## 一次 push 到多個 remote

- 使用 SourceTree

  [![image](https://user-images.githubusercontent.com/37999690/128456280-92b33a27-da6e-48a0-9719-6ce0fe0f6e72.png "image")](https://user-images.githubusercontent.com/37999690/128456280-92b33a27-da6e-48a0-9719-6ce0fe0f6e72.png)
