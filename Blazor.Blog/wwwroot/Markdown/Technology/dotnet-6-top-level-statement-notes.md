# .Net 6 console Top Level Statements 筆記

## 使用 appsettings.json

- `AddJsonFile`

  > Microsoft.Extensions.Configuration.Json

- `AddEnvironmentVariables`

  > Microsoft.Extensions.Configuration.EnvironmentVariables

- `config.GetSection("UserInfo").Get<UserInfo>()`

  > Microsoft.Extensions.Configuration.Binder

- appsettings.json 記得要設定 `Copy to Output Directory` 為 `Copy always` 或 `Copy if newer`
  ![image](https://user-images.githubusercontent.com/37999690/199229987-8ae86ac2-7fb8-44c8-93e0-c6303b5ca175.png)

```csharp
using Microsoft.Extensions.Configuration;


IConfiguration config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", true, true)
    .AddJsonFile($"appsettings.{environmentName}.json", true, true)
    .AddEnvironmentVariables()
    .Build();

var area = config.GetSection("Area").Value;

UserInfo userInfo = config.GetSection("UserInfo").Get<UserInfo>();

public class UserInfo
{
    public string? Name { get; set; }

    public string? Language{get;set;}
}
```

## 依賴注入 DI (Dependency Injection)

```csharp
using Microsoft.Extensions.DependencyInjection;


var serviceCollection = new ServiceCollection();

serviceCollection.AddTransient<UserInfo>();

var services = serviceCollection.BuildServiceProvider();

var userInfo = services.GetRequiredService<UserInfo>();

Console.WriteLine(userInfo.GetName());

public class UserInfo
{
    private string Name { get; set; } = "Ari";

    public string? Language { get; set; }

    public string GetName()
    {
        return Name;
    }

}
```
