# Appsettings in mstest

## 環境

- .net 5 MVC or API (不重要，有 appsettings.json 就好)

- MsTest with .net 5

- MsTest 安裝以下套件

  ```PM
  # Microsoft.Extensions.Configuration
  dotnet add package Microsoft.Extensions.Configuration

  # Microsoft.Extensions.Configuration.Json
  dotnet add package Microsoft.Extensions.Configuration.Json

  # Microsoft.Extensions.Configuration.Binder
  dotnet add package Microsoft.Extensions.Configuration.Binder
  ```

- MsTest 對 MVC or API 專案 加入專案參考

  [![image](https://user-images.githubusercontent.com/37999690/126445256-53ab8ad1-db96-4811-8410-aa6be15091b3.png "image")](https://user-images.githubusercontent.com/37999690/126445256-53ab8ad1-db96-4811-8410-aa6be15091b3.png)

## Code

```csharp
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Extensions.Options;

namespace AppsettingDemo.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestInitialize]
        public void Init()
        {
            var employee = new Employee();
            IConfigurationRoot config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", false) // optional = false 代表此檔案是必要的，如果檔案不存在的話就會報錯
                .AddJsonFile("appsettings.Development.json", true)
                .AddUserSecrets("136c****-****-****-****-*******2782c") // 參考csproj的UserSecretsId，沒有設定就不用
                .Build();

            config.GetSection("Employee").Bind(employee); // 將資料bind進model

            IOptions<Employee> ioptionsEmployee = Options.Create(employee); // 將model轉成IOptions<T>

        }
    }

    public class Employee
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Location { get; set; }
    }
}

```
