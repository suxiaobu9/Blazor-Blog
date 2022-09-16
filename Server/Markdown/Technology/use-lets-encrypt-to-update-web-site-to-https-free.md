# 利用 Let's Encrypt 將 Azure App Service 網站免費升級成 https

## 環境

- Azure
- NoIp
- [Azure Let's Encrypt](https://github.com/sjkp/letsencrypt-siteextension/wiki/How-to-install) 1.0.6

## 說明

### 本篇會以 Azure App Service 作為範例，App Service Plan 定價層至少要 B1 才有 自訂網域/SSL 的功能

### No-IP 建立新 domain

- ![image](https://user-images.githubusercontent.com/37999690/189659040-576417a8-3b16-4b29-ba37-156dade77b16.png)

### 設定目標 IP

- ![image](https://user-images.githubusercontent.com/37999690/189660809-d57a8060-4b62-4548-9774-58a31b9c455f.png)

- ![image](https://user-images.githubusercontent.com/37999690/189661053-48857281-7017-4a82-8fad-317b72887846.png)

### 設定網域驗證識別碼

- ![image](https://user-images.githubusercontent.com/37999690/189661301-5e16143d-1308-4ef3-bfd3-cdf0b8b6a0a4.png)

- ![image](https://user-images.githubusercontent.com/37999690/189660558-d8d5933f-f64e-41ef-90d1-17ad914ce005.png)

- ![image](https://user-images.githubusercontent.com/37999690/189661778-c599e9f5-0366-4400-9394-f34af96ccebf.png)

- ![image](https://user-images.githubusercontent.com/37999690/189661954-e99bfd44-9912-4fa7-aa88-74b4f91884f0.png)

### 新增自訂網域

- ![image](https://user-images.githubusercontent.com/37999690/189662192-d10df096-2723-4898-a388-8cadcea04be9.png)

- ![image](https://user-images.githubusercontent.com/37999690/189662787-b3338506-3c5c-42fc-8992-c4edb03ff8b9.png)

- ![image](https://user-images.githubusercontent.com/37999690/189662578-e16873ed-4466-44ad-9faa-0d9947b6d5d6.png)

- ![image](https://user-images.githubusercontent.com/37999690/189663451-a7fa6b98-c967-4b4e-b5c3-a4a2d791f884.png)

### 開啟 AAD (Azure Active Directory)

- ![image](https://user-images.githubusercontent.com/37999690/189619721-90445e53-59b9-4e93-9645-5fc084c9e09a.png)

### 進入應用程式註冊並點新增註冊

- ![image](https://user-images.githubusercontent.com/37999690/189620439-2f1d84bd-14d7-435e-b222-e58a81cc49d6.png)

### 輸入識別用的應用程式名稱

- ![image](https://user-images.githubusercontent.com/37999690/189647159-3a9f471e-9531-4ce1-bc08-8f7b4f80f643.png)

### 建立應用程式的金鑰

- ![image](https://user-images.githubusercontent.com/37999690/189648173-7cf7a47a-b06a-492d-8300-81a737424816.png)

### 記住秘密識別碼的值，後面用的到

- ![image](https://user-images.githubusercontent.com/37999690/189665239-7cf29baf-34d6-4f6f-9cc2-432ec02760c4.png)

### 到訂用帳號增加 `存取控制 (IAM)`

- ![image](https://user-images.githubusercontent.com/37999690/189652050-e341cc91-c835-4cdd-8931-65c048e990d6.png)

- ![image](https://user-images.githubusercontent.com/37999690/189652187-8819f0d6-2147-4e11-b16c-694ae9fdad4f.png)

- ![image](https://user-images.githubusercontent.com/37999690/189653297-a4627cb0-2a2c-4f7a-9942-680bc1410301.png)

- ![image](https://user-images.githubusercontent.com/37999690/189652568-e0afde04-b082-4df7-a25b-c4bd0c2cfa4e.png)

- ![image](https://user-images.githubusercontent.com/37999690/189653794-8dccfa15-3a63-478b-8fa8-578531ca7169.png)

### 增加儲存體帳戶

- ![image](https://user-images.githubusercontent.com/37999690/189657259-eb6b8c14-37c4-4053-a773-5cb3301ad16e.png)

- ![image](https://user-images.githubusercontent.com/37999690/189657444-d68c655c-c191-431b-ad0c-063a58f57406.png)

- ![image](https://user-images.githubusercontent.com/37999690/189657714-f2d459af-2324-4d9a-a18f-c0f6a540450e.png)

- ![image](https://user-images.githubusercontent.com/37999690/189657942-589a1e74-e64b-4dbd-b439-d2099620a56a.png)

### 到 App Service `擴充功能` 新增 `Azure Let's Encrypt`

- ![image](https://user-images.githubusercontent.com/37999690/189655274-bb20d63b-ddd5-4d06-acad-0310459b1219.png)

### 選擇應用程式與接受法律條款

- ![image](https://user-images.githubusercontent.com/37999690/189655569-8d09cdb9-0881-4aa7-8cc2-8da3572a2d16.png)

- ![image](https://user-images.githubusercontent.com/37999690/189656245-d6964b51-c34d-4854-b915-614174012d62.png)

- ![image](https://user-images.githubusercontent.com/37999690/189656333-c4595f7b-15a8-4832-9737-f739df4968dd.png)

### 等一段時間，讓程式裝起來

- ![image](https://user-images.githubusercontent.com/37999690/189656715-c9a5673d-4343-42da-9524-93db5e277b92.png)

- ![image](https://user-images.githubusercontent.com/37999690/189656876-2ed4db28-0adf-401c-aec4-88f73de33975.png)

### 瀏覽網頁會需要一段時間才開得起來

### 填入設定值

- ![image](https://user-images.githubusercontent.com/37999690/189658263-09778c14-0048-4e15-92d1-debfd561725b.png)

- `Tenant`

  - ![image](https://user-images.githubusercontent.com/37999690/189664134-b1c8d65a-9c55-4fc9-9b15-d8292af76a90.png)

- `SubscriptionId`

  - ![image](https://user-images.githubusercontent.com/37999690/189664420-52ec00fd-401c-4a56-ba40-321ee9001bea.png)

- `ClientId`

  - ![image](https://user-images.githubusercontent.com/37999690/189664660-e8004550-72b4-453a-9223-25d6a5c38292.png)

- `ClientSecret`

  - ![image](https://user-images.githubusercontent.com/37999690/189665239-7cf29baf-34d6-4f6f-9cc2-432ec02760c4.png)

- `ResourceGroupName`

  - ![image](https://user-images.githubusercontent.com/37999690/189665589-c99fd3bb-3ed5-4bdd-9f58-7254dfe3eed0.png)

- `ServicePlanResourceGroupName`

  - ![image](https://user-images.githubusercontent.com/37999690/189665976-075961fd-1f3e-4aef-8f66-0d17db7289b9.png)

- `DashboardConnectionString`、`StorageConnectionString`

  - ![image](https://user-images.githubusercontent.com/37999690/189666122-4855463b-43ff-4cf5-8e07-31e40145d4fe.png)

- ![image](https://user-images.githubusercontent.com/37999690/189667496-34a6f5dc-fabf-46e7-bcf9-da9d278acf5f.png)

- ![image](https://user-images.githubusercontent.com/37999690/189668360-e30a9ba5-2970-46a0-8376-309f70bc5928.png)

### 設定 SSL

- ![image](https://user-images.githubusercontent.com/37999690/189668319-4fedbfe1-46fe-4e31-93ff-56fb0d750a98.png)

- ![image](https://user-images.githubusercontent.com/37999690/189668786-5dd32230-0d4b-4090-b5d9-66273504628f.png)

### 設定成功

- ![image](https://user-images.githubusercontent.com/37999690/189669490-1dcc4ba1-0d65-4ca9-af24-da1f33806ab4.png)

- ![image](https://user-images.githubusercontent.com/37999690/189669633-45c7cd71-def9-497b-8357-4c1fe9d3a4b5.png)

- ![image](https://user-images.githubusercontent.com/37999690/189669924-9b3df1a6-ec12-40a4-8d8f-291f1abba9b7.png)
