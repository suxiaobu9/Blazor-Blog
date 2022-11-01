# C# 淺嚐 Lazy\<T\> - 延遲初始設定

```csharp
Console.WriteLine("Program start ...");
Console.WriteLine($"------------------{Environment.NewLine}");

Console.WriteLine("Start new user class");
var user = new User("Bob");
Console.WriteLine("End new user class");
Console.WriteLine(user.Name);

Console.WriteLine($"{Environment.NewLine}------------------{Environment.NewLine}");

Console.WriteLine("Start lazy new user class");
var lazy_user = new Lazy<User>(() => new User("Ari"));
Console.WriteLine("End lazy new user class");
Console.WriteLine(lazy_user.Value.Name);


public class User
{
    public User(string userName)
    {
        this.Name = userName;

        Console.WriteLine($"[Constructor] New User, Name = {this.Name}");
    }

    public string Name { get; private set; }
}

```

- 輸出結果

![image](https://user-images.githubusercontent.com/37999690/199192632-13fcbb3d-65ed-45e4-9a06-6a2008ae4a2c.png)

- 傳統的 new 在當下就會去執行 constructor，在用了 Lazy\<T\> 之後，在需要用到 class 時才會執行 constructor，避免浪費。
