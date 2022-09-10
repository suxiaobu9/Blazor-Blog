# 移除特殊名稱的資料夾

這幾天再研究將 angular 發佈到相對路徑，意外踩了個雷。

反正經常犯蠢，就紀錄一下

## 開始

- 將 build 的指令改成以下，並且執行 `npm run build`

  [![image](https://user-images.githubusercontent.com/37999690/125589323-6f8a7860-9a76-4973-8171-1efa690a1883.png "image")](https://user-images.githubusercontent.com/37999690/125589323-6f8a7860-9a76-4973-8171-1efa690a1883.png)

- 執行完成後 ... 資料夾中出現了特殊的資料夾

  [![image](https://user-images.githubusercontent.com/37999690/125589704-914f5b1b-df82-4c5d-aa8d-20ba654519ae.png "image")](https://user-images.githubusercontent.com/37999690/125589704-914f5b1b-df82-4c5d-aa8d-20ba654519ae.png)

- 這是一個開不了也刪不掉的資料夾

  [![image](https://user-images.githubusercontent.com/37999690/125589890-ce845745-b335-4c62-a08f-32151ac059fb.png "image")](https://user-images.githubusercontent.com/37999690/125589890-ce845745-b335-4c62-a08f-32151ac059fb.png)

  [![image](https://user-images.githubusercontent.com/37999690/125589953-a5903f4c-1651-4995-beb2-9aa0ecfff676.png "image")](https://user-images.githubusercontent.com/37999690/125589953-a5903f4c-1651-4995-beb2-9aa0ecfff676.png)

## 處理方式

- 在 cmd 下指令 `dir /x`後可以取得該資料夾的短檔名

  [![image](https://user-images.githubusercontent.com/37999690/125590479-c38fafc8-930e-40eb-87f5-278288732467.png "image")](https://user-images.githubusercontent.com/37999690/125590479-c38fafc8-930e-40eb-87f5-278288732467.png)

- 在 cmd 下指令變更該資料夾的名稱 `rename 'FC2E~1 FC2E~1`

  [![image](https://user-images.githubusercontent.com/37999690/125597090-f438f8a6-3ec5-49e1-8d5a-93fe2ee3f2ab.png "image")](https://user-images.githubusercontent.com/37999690/125597090-f438f8a6-3ec5-49e1-8d5a-93fe2ee3f2ab.png)

- 順利刪除該資料夾

  [![image](https://user-images.githubusercontent.com/37999690/125597378-afeca042-4902-485e-a2be-6b9f6c4952d9.png "image")](https://user-images.githubusercontent.com/37999690/125597378-afeca042-4902-485e-a2be-6b9f6c4952d9.png)
