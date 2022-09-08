# 重灌後Visual Studio沒辦法還原Nuget套件

最近因為電腦有些問題所以就自己重灌了，全部程式都重灌好了之後發現 VS Community 2019 在 Restore 舊專案的 Nuget 套件居然失敗了 !?

檢查後發現 `套件來源` 居然沒有加入官方來源，解決方法也很簡單，自己加回去就好了 !

Nuget 官方來源 `https://api.nuget.org/v3/index.json`

## 方法一

最簡單粗暴的方法

[![image](https://user-images.githubusercontent.com/37999690/131604463-813ca67a-0c37-4ebf-83dc-95eaca4a7851.png "image")](https://user-images.githubusercontent.com/37999690/131604463-813ca67a-0c37-4ebf-83dc-95eaca4a7851.png)

## 方法二

下指令加回來，不過需要先安裝 `nuget.commandline` 就是了

```ps
choco install -y nuget.commandline
```

安裝後重開 powershell 或是 cmd

```ps
nuget source add -name nuget -source https://api.nuget.org/v3/index.json
```

必須重啟 Visual Stuido 不然會吃不到設定
