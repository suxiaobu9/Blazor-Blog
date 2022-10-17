# .net core 3 注入相同介面、多個實作

## Interface

```csharp
public interface IOperatingSystem
{
    public OperatingSystemType OSType { get; }
    public void Run();
}
```

## 多個實作

```csharp
public class WindowsOS : IOperatingSystem
{
    public OperatingSystemType OSType => OperatingSystemType.Windows;

    public void Run()
    {
        Console.WriteLine(OperatingSystemType.Windows);
    }
}
```

```csharp
public class MacOS : IOperatingSystem
{
    public OperatingSystemType OSType => OperatingSystemType.Mac;

    public void Run()
    {
        Console.WriteLine(OperatingSystemType.Mac);
    }
}
```

```csharp
public class LinuxOS : IOperatingSystem
{
    public OperatingSystemType OSType => OperatingSystemType.Linux;

    public void Run()
    {
        Console.WriteLine(OperatingSystemType.Linux);
    }
}
```

```csharp
public class UbuntuOS : IOperatingSystem
{
    public OperatingSystemType OSType => OperatingSystemType.Ubuntu;

    public void Run()
    {
        Console.WriteLine(OperatingSystemType.Ubuntu);
    }
}
```

## 註冊實作

```csharp
public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddSingleton<IOperatingSystem, WindowsOS>();
        services.AddSingleton<IOperatingSystem, MacOS>();
        services.AddSingleton<IOperatingSystem, LinuxOS>();
        services.AddSingleton<IOperatingSystem, UbuntuOS>();
    }
}
```

## Injection

### 有指定實作的 Type，取得指定的實作

Constructor Injection 時用 `IEnumerable<T>` 注入，取得所有實作，再依照情境取得所需要的實作

```csharp
public class HomeController : ControllerBase
{
    private readonly IOperatingSystem _windows;

    public HomeController(IEnumerable<IOperatingSystem> os)
    {
        _windows = os.Single(x => x.OSType == OperatingSystemType.Mac);
    }

    [HttpGet]
    public void Get()
    {
        _windows.Run();
        // 執行結果 Console 輸出 Mac
    }

}
```

### 未指定實作的 Type，取得 Interface 最後一個註冊的實作

在 Injection 時僅注入 `T`，則取得最新的實作(最後註冊的實作)

這個範例在 Startup 中最後(最新)註冊的是

```csharp
services.AddSingleton<IOperatingSystem, UbuntuOS>();
```

以下為範例

```csharp
private readonly IOperatingSystem _windows;

public HomeController(IOperatingSystem os)
{
    _windows = os;
}

[HttpGet]
public void Get()
{
    _windows.Run();
    // 執行結果 Console 輸出 Ubuntu
}
```

## 注意

通常只建議註冊`Singleton`的服務這樣使用，`Transient`與`Scoped`在注入時會`new`新的實作，若是該 Interface 有 5 個實作，但只用到其中 1 個，會造成效能上的浪費

## 參考資料

[ASP.NET Core 3 系列 - 注入多個相同的介面 (Interface)](https://blog.johnwu.cc/article/asp-net-core-3-di-same-interface.html)
