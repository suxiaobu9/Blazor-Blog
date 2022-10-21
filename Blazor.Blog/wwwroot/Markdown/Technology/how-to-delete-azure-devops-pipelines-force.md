# Azure Devops Pipelines 無法刪除的處理方式

偶爾在 Azure Devops 刪除 Pipelines 時會出現以下錯誤訊息導致無法刪除

> One or more builds associated with the requested pipeline(s) are retained by a release. The pipeline(s) and builds will not be deleted.

這時候可以開 PowerShell 下指令將 Pipeline Build 出來的東西清除後，就可以正常刪除了

```powershell
$personalAccessToken = "輸入PAT"
$organization = "組織名稱"
$project = "專案名稱"
$apiVersion = "6.0"

$token = [System.Convert]::ToBase64String([System.Text.Encoding]::ASCII.GetBytes(":$($personalAccessToken)"))
$header = @{authorization = "Basic $token"}

function DeleteLease($definitionId) {
    $url = "https://dev.azure.com/$organization/$project/_apis/build/retention/leases?api-version=$($apiVersion)&definitionId=$definitionId"
    $leases = (Invoke-RestMethod -Method GET -Uri $url -ContentType "application/json" -Headers $header )

    foreach ($lease in $leases.value) {
        $leaseId = $lease.leaseId
        $url = "https://dev.azure.com/$organization/$project/_apis/build/retention/leases?ids=$($leaseId)&api-version=$($apiVersion)"
        Invoke-RestMethod -Method DELETE -Uri $url -ContentType "application/json" -Headers $header
        Write-Host "DELETE $url"
    }
}

$url = "https://dev.azure.com/$organization/$project/_apis/build/definitions?api-version=$apiVersion"

$buildDefinitions = Invoke-RestMethod -Uri $url -Method Get -ContentType "application/json" -Headers $header

foreach ($def in $builddefinitions.value) {
    Write-Host $def.id $def.queueStatus $def.name
    DeleteLease $def.id
}
```

## 參考資料

- [Azure Pipelines 無法刪除 Build Pipeline 的處理方法](https://blog.miniasp.com/post/2022/06/06/Azure-Pipelines-unable-delete-that-retained-by-release)
