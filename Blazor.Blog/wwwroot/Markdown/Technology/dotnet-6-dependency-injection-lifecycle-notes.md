# .Net 6 依賴注入 LifeCycle 筆記

## 注入過程情境

[![image](https://github-production-user-asset-6210df.s3.amazonaws.com/37999690/265734325-d789fd2d-efc4-438e-92a5-2588d921a757.png)](https://mermaid.live/edit#pako:eNqrVkrOT0lVslIqTi0sTc1LTnXJTEwvSsyNyVMAgucrup_u6VfQtbNTcPFUcM7PK0nMzEstslJ41jHx-awWBY_83FSQaFF-Tk5qkcLLOQ0vljUqeAYn5hbkpEKMQNYHM-jFit1P27peLFwBNKhz39OODTAtUBmQ0Z55xSWJQPdoPF2_7Mm-bk0cpkFdaKXwdGLXixVbnuxdoAA16tmM9XBjkQ1U0lHKTS3KTcxMAfq6GmRsjFJJRmpuaoySFZCZkliUHaMUk1cLVJdYWpIfXJmXrGRVUlSaqqNUWpCSWAILIZhgakpmSX6RLyQUwYFZCwBezYhU)

## 生命週期

註冊在 Service 有三種生命週期

- Singleton

  > 在整個應用程式的生命週期只會有一個實例。在第一次建立完成後，這個實例會被重複使用，在每次建構類別時取到的實例都是同一個

- Scoped

  > 每一個 Request 都會重新建立一個實例，在這個 Request 中建構的類別都會取到這個實例

- Transient

  > 每次建構類別時都會新建立一個實例

```csharp
//           (實例)   (介面)
public class Sample : ISample
{
  public Sample()
  {
    // 建構
  }
}
```

## 範例

### Program.cs

```csharp
builder.Services.AddSingleton<ISingletonSample, SingletonSample>();
builder.Services.AddScoped<IScopedSample, ScopedSample>();
builder.Services.AddTransient<ITransientSample, TransientSample1>();
builder.Services.AddTransient<ITransientSample, TransientSample2>();
```

### ISingletonSample、SingletonSample

```csharp
public interface ISingletonSample { }

public class SingletonSample : ISingletonSample
{
    public SingletonSample()
    {
        Console.WriteLine($"{nameof(SingletonSample)} Construct");
    }
}
```

### IScopedSample、ScopedSample

```csharp
public interface IScopedSample { }

public class ScopedSample : IScopedSample
{
    public ScopedSample()
    {
        Console.WriteLine($"{nameof(ScopedSample)} Construct");
    }
}

```

### ITransientSample、TransientSample1

```csharp
public interface ITransientSample
{
    public int GetSingletonHashCode();
    public int GetScopedHashCode();
}

public class TransientSample : ITransientSample
{
    private readonly IScopedSample scopedSample;
    private readonly ISingletonSample singletonSample;

    public TransientSample(IScopedSample scopedSample,
        ISingletonSample singletonSample)
    {
        Console.WriteLine($"\n{nameof(TransientSample)} Construct start");

        this.scopedSample = scopedSample;
        Console.WriteLine($"${nameof(TransientSample)}: {nameof(scopedSample)}: {scopedSample.GetHashCode()}");

        this.singletonSample = singletonSample;
        Console.WriteLine($"${nameof(TransientSample)}: {nameof(singletonSample)}: {singletonSample.GetHashCode()}");

        Console.WriteLine($"{nameof(TransientSample)} Construct end\n");
    }

    public int GetScopedHashCode()
    {
        return scopedSample.GetHashCode();
    }

    public int GetSingletonHashCode()
    {
        return singletonSample.GetHashCode();
    }
}
```

### HomeController

```csharp
public class HomeController : Controller
{
    private readonly ITransientSample transientSamples1;
    private readonly ITransientSample transientSamples2;

    public HomeController(ITransientSample transientSample1, ITransientSample transientSample2)
    {
        Console.WriteLine($"\n{nameof(HomeController)} Construct start");

        this.transientSamples1 = transientSample1;
        this.transientSamples2 = transientSample2;

        Console.WriteLine($"{nameof(HomeController)} Construct end\n");
    }

    public IActionResult Index()
    {
        Console.WriteLine();
        Console.WriteLine("                Singleton      Scoped        Transient");
        Console.WriteLine($"HashCode        {transientSamples1.GetSingletonHashCode()}       {transientSamples1.GetScopedHashCode()}      {transientSamples1.GetHashCode()}");
        Console.WriteLine($"HashCode        {transientSamples2.GetSingletonHashCode()}       {transientSamples2.GetScopedHashCode()}      {transientSamples2.GetHashCode()}");
        Console.WriteLine();
        Console.WriteLine($"------------------------------------------------------------");


        return View();
    }
}
```

這段程式中 `ITransientSample` 註冊了兩個實例 `TransientSample`，這個實例注入了 `ISingletonSample`、`IScopedSample`，並且在 Construct 加入了訊息來觀察行為。

### 第一次啟動網頁後的 Console

![image](https://github-production-user-asset-6210df.s3.amazonaws.com/37999690/265729330-5f3fcc82-47b6-4b2c-b275-65a16a5bd9aa.png)

在這段訊息中可以發現

- 因為在 `HomeController` 注入了兩次 ITransientSample，所以 transientSample1 與 transientSample2 的 HashCode 不同
- `TransientSample` 注入了 ISingletonSample 與 IScopedSample，但 SingletonSample 與 ScopedSample 只建構了一次，且 transientSample`1` 中 ISingletonSample 與 transientSample`2` 中 ISingletonSample 的 HashCode 相同，IScopedSample 也是如此。

### 重新整理首頁，等於再發一次 Request

![image](https://github-production-user-asset-6210df.s3.amazonaws.com/37999690/265731940-28d976bc-b3a5-49ea-852b-cad595958426.png)

在第二次的訊息中驗證了

- `transientSample1` 與 `transientSample2` 的 HashCode 還是不同，且與第一次沒有重複
- SingletonSample 沒有再次被建構，且 ISingletonSample 的 HashCode 與第一次的相同
- ScopedSample 再次被建構，且第二次 transientSample`1` 與 transientSample`2` 中 IScopedSample 的 HashCode 相同，但與第一次的不同
