# Azure DevOps Auto Deploy

## 環境

1. .net 5 MVC
2. Azure DevOps
3. Azure App Server 免費方案

## 設定 DevOps Pipelines

- Pipelines -> Pipelines -> Create Pipelines

  [![image](https://user-images.githubusercontent.com/37999690/125182194-91638400-e23e-11eb-9779-700fee984852.png "image")](https://user-images.githubusercontent.com/37999690/125182194-91638400-e23e-11eb-9779-700fee984852.png)

- 選擇放 Code 的地方，這邊是示範 Code 放在 Azure DevOps 裡，所以選擇 Azure Repos Git

  [![image](https://user-images.githubusercontent.com/37999690/125182210-adffbc00-e23e-11eb-9a74-1fa8643e4f21.png "image")](https://user-images.githubusercontent.com/37999690/125182210-adffbc00-e23e-11eb-9a74-1fa8643e4f21.png)

- 選擇專案

  [![image](https://user-images.githubusercontent.com/37999690/125182218-c079f580-e23e-11eb-862d-1eea1e4e6120.png "image")](https://user-images.githubusercontent.com/37999690/125182218-c079f580-e23e-11eb-862d-1eea1e4e6120.png)

- 選擇專案架構，這邊事先準備好的專案是 .net 5 的，所以選擇 ASP .NET Core(.Net Framework)

  [![image](https://user-images.githubusercontent.com/37999690/125182225-cf60a800-e23e-11eb-8974-5a576a3acd03.png "image")](https://user-images.githubusercontent.com/37999690/125182225-cf60a800-e23e-11eb-8974-5a576a3acd03.png)

  [![image](https://user-images.githubusercontent.com/37999690/125182236-dedff100-e23e-11eb-8ef5-7cf740d61b85.png "image")](https://user-images.githubusercontent.com/37999690/125182236-dedff100-e23e-11eb-8ef5-7cf740d61b85.png)

- 產出預設的 YAML 後，要多新增一個步驟 -> Publish build artifact

  [![image](https://user-images.githubusercontent.com/37999690/125182241-ed2e0d00-e23e-11eb-8c7c-884dfdccc488.png "image")](https://user-images.githubusercontent.com/37999690/125182241-ed2e0d00-e23e-11eb-8c7c-884dfdccc488.png)

  [![image](https://user-images.githubusercontent.com/37999690/125182245-fcad5600-e23e-11eb-9de6-c3d63bdaf1e4.png "image")](https://user-images.githubusercontent.com/37999690/125182245-fcad5600-e23e-11eb-9de6-c3d63bdaf1e4.png)

- 執行產生好的 YAML

  [![image](https://user-images.githubusercontent.com/37999690/125182253-0b940880-e23f-11eb-8ee1-f0d7d37a2fe9.png "image")](https://user-images.githubusercontent.com/37999690/125182253-0b940880-e23f-11eb-8ee1-f0d7d37a2fe9.png)

  [![image](https://user-images.githubusercontent.com/37999690/125182260-1c447e80-e23f-11eb-9027-957e1d8bcef3.png "image")](https://user-images.githubusercontent.com/37999690/125182260-1c447e80-e23f-11eb-9027-957e1d8bcef3.png)

- 等個一段時間，讓 Pipeline 跑完

  [![image](https://user-images.githubusercontent.com/37999690/125182263-29616d80-e23f-11eb-9c71-39f6ebbbae16.png "image")](https://user-images.githubusercontent.com/37999690/125182263-29616d80-e23f-11eb-9c71-39f6ebbbae16.png)

## 設定 Releases

- Pipeline -> Releases -> New pipeline

  [![image](https://user-images.githubusercontent.com/37999690/125182268-37af8980-e23f-11eb-98b6-bf076bae4bb4.png "image")](https://user-images.githubusercontent.com/37999690/125182268-37af8980-e23f-11eb-98b6-bf076bae4bb4.png)

- 選擇服務的 Template

  [![image](https://user-images.githubusercontent.com/37999690/125182271-4433e200-e23f-11eb-822a-5db9c50becc5.png "image")](https://user-images.githubusercontent.com/37999690/125182271-4433e200-e23f-11eb-822a-5db9c50becc5.png)

- 選剛剛架好的 Pipeline

  [![image](https://user-images.githubusercontent.com/37999690/125182280-5281fe00-e23f-11eb-889e-002ce459e5ce.png "image")](https://user-images.githubusercontent.com/37999690/125182280-5281fe00-e23f-11eb-889e-002ce459e5ce.png)

- 選擇資源群組(這邊先示範同一個帳號內的)

  [![image](https://user-images.githubusercontent.com/37999690/125182285-5f9eed00-e23f-11eb-9f7d-b4584a40def4.png "image")](https://user-images.githubusercontent.com/37999690/125182285-5f9eed00-e23f-11eb-9f7d-b4584a40def4.png)

  [![image](https://user-images.githubusercontent.com/37999690/125182292-72b1bd00-e23f-11eb-98fa-ef610ceaa0d3.png "image")](https://user-images.githubusercontent.com/37999690/125182292-72b1bd00-e23f-11eb-98fa-ef610ceaa0d3.png)

- 建立 Release

  [![image](https://user-images.githubusercontent.com/37999690/125182299-81986f80-e23f-11eb-90a3-008c7df47502.png "image")](https://user-images.githubusercontent.com/37999690/125182299-81986f80-e23f-11eb-90a3-008c7df47502.png)

  [![image](https://user-images.githubusercontent.com/37999690/125182311-9aa12080-e23f-11eb-9c13-200b7f89d104.png "image")](https://user-images.githubusercontent.com/37999690/125182311-9aa12080-e23f-11eb-9c13-200b7f89d104.png)

- Release 建立中

  [![image](https://user-images.githubusercontent.com/37999690/125182318-a856a600-e23f-11eb-9c19-23b09d95e406.png "image")](https://user-images.githubusercontent.com/37999690/125182318-a856a600-e23f-11eb-9c19-23b09d95e406.png)

- 建立成功

  [![image](https://user-images.githubusercontent.com/37999690/125182323-b60c2b80-e23f-11eb-9a99-d2344fa94d78.png "image")](https://user-images.githubusercontent.com/37999690/125182323-b60c2b80-e23f-11eb-9a99-d2344fa94d78.png)

  [![image](https://user-images.githubusercontent.com/37999690/125182327-c4f2de00-e23f-11eb-8b03-9a7abdfaf7de.png "image")](https://user-images.githubusercontent.com/37999690/125182327-c4f2de00-e23f-11eb-8b03-9a7abdfaf7de.png)

- Hello World !

  [![image](https://user-images.githubusercontent.com/37999690/125182340-d4722700-e23f-11eb-98da-65fc7d14e8f2.png "image")](https://user-images.githubusercontent.com/37999690/125182340-d4722700-e23f-11eb-98da-65fc7d14e8f2.png)
