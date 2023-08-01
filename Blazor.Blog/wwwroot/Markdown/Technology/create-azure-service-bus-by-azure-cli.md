# 利用 azure cli 快速建立 Azure Service Bus 服務

```ps1
# Azure Active Directory 中應用程式註冊的 憑證及秘密
$Password = 'p******************************~'

# Azure Active Directory 中應用程式註冊的 應用程式 (用戶端) 識別碼
$ApplicationId = '{GUID}'

# Azure Active Directory 概觀畫面的 租用戶識別碼
$TenantId = '{GUID}'
az login --service-principal -u $ApplicationId -p $Password --tenant $TenantId

# 需要將 應用程式 加入到 資源群組 存取控制 (IAM) 的角色指派，並給參與者 (在 具有特殊權限的系統管理員角色 頁簽下)
$QueueName = 'JobSeekerCrawer-dev'
$SourceGroupName = 'Demo'
$AuthorizationRule = 'ReadWrite'
az servicebus namespace create --resource-group $SourceGroupName --name $QueueName --location eastasia --sku Basic
az servicebus namespace authorization-rule create --resource-group $SourceGroupName --namespace-name $QueueName --name $AuthorizationRule --rights Send Listen

az servicebus queue create --name queue_company_id_for_104 --namespace-name $QueueName --resource-group $SourceGroupName
az servicebus queue create --name queue_job_id_for_104 --namespace-name $QueueName --resource-group $SourceGroupName

az servicebus namespace authorization-rule keys list --resource-group $SourceGroupName --namespace-name $QueueName --name $AuthorizationRule --query primaryConnectionString --output tsv

```

## 參考資料

- [az servicebus | Microsoft Learn](https://learn.microsoft.com/en-us/cli/azure/servicebus?view=azure-cli-latest)
