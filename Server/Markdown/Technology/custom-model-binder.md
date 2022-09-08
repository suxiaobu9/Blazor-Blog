# Custom Model Binder

## 環境

1. .Net Framework 4.8 MVC

1. .Net 5 MVC

## .Net Framework 4.8 MVC

- LoginModel.cs

- ```csharp
    public class LoginModel
    {
        [DisplayName("帳號")]
        public string Account { get; set; }

        [DisplayName("密碼")]
        [DataType(DataType.Password)]
        public SecureString Password { get; set; }
    }
  ```

  [![image](https://user-images.githubusercontent.com/37999690/125567870-bac32b80-8509-42a4-a929-d7dda2139f91.png "image")](https://user-images.githubusercontent.com/37999690/125567870-bac32b80-8509-42a4-a929-d7dda2139f91.png)

### 沒有 Model Bind 的狀況

- 因為 Browser 傳回後端的是 string，而 Model 的 Password 是 SecureString，所以接不到

  [![image](https://user-images.githubusercontent.com/37999690/125568399-4ccaac3b-690e-4e07-9793-f19d18ed1ec4.png "image")](https://user-images.githubusercontent.com/37999690/125568399-4ccaac3b-690e-4e07-9793-f19d18ed1ec4.png)

### 增加 SecureString 的 Binding

- SecureStringBinder.cs

  ```csharp
    public class SecureStringBinder :IModelBinder
    {
        public object BindModel(ControllerContext controllerContext , ModelBindingContext bindingContext)
        {
            var value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            if (value == null || string.IsNullOrWhiteSpace(value.AttemptedValue))
                return null;

            var secureString = new SecureString();

            foreach (var item in value.AttemptedValue)
                secureString.AppendChar(item);

            return secureString;
        }
    }
  ```

- Global.asax.cs

  ```csharp
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
          ...

          ModelBinders.Binders.Add(typeof(SecureString), new SecureStringBinder());
        }
    }
  ```

- Controller 接收到 Password，且可以從 SecureString 轉成 String

  [![image](https://user-images.githubusercontent.com/37999690/125569233-6f032740-792e-4794-a1bd-f1c486057539.png "image")](https://user-images.githubusercontent.com/37999690/125569233-6f032740-792e-4794-a1bd-f1c486057539.png)

## .Net 5 MVC

- LoginModel.cs

  ```csharp
    public class LoginModel
    {
        [DisplayName("帳號")]
        public string Account { get; set; }

        [DisplayName("密碼")]
        [DataType(DataType.Password)]
        public SecureString Password { get; set; }
    }
  ```

- SecureStringBinder.cs

  ```cs
    public class SecureStringBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
                throw new ArgumentNullException(nameof(bindingContext));

            var modelName = bindingContext.ModelName;

            var ValueProviderResult = bindingContext.ValueProvider.GetValue(modelName);

            if(ValueProviderResult == ValueProviderResult.None)
                return Task.CompletedTask;

            var value = ValueProviderResult.FirstValue;

            if(string.IsNullOrWhiteSpace(value))
                return Task.CompletedTask;

            var secureString = new SecureString();

            foreach (var c in value)
                secureString.AppendChar(c);

            bindingContext.Result = ModelBindingResult.Success(secureString);

            return Task.CompletedTask;
        }
    }
  ```

## 方法 1

直接在 model 的 property 加上 attribute

- LoginModel.cs

```csharp
public class LoginModel
{
    [DisplayName("帳號")]
    public string Account { get; set; }

    [DisplayName("密碼")]
    [DataType(DataType.Password)]
    [ModelBinder(BinderType =typeof(SecureStringBinder))]
    public SecureString Password { get; set; }
}
```

## 方法 2

在 ConfigureServices 中增加 binding

- LoginModel.cs

```csharp
public class LoginModel
{
    [DisplayName("帳號")]
    public string Account { get; set; }

    [DisplayName("密碼")]
    [DataType(DataType.Password)]
    public SecureString Password { get; set; }
}
```

- SecureStringBinderProvider.cs

```csharp
public class SecureStringBinderProvider: IModelBinderProvider
{
    public IModelBinder GetBinder(ModelBinderProviderContext context)
    {
        if (context is null) throw new ArgumentNullException(nameof(context));

        if (context.Metadata.ModelType == typeof(SecureString))
            return new BinderTypeModelBinder(typeof(SecureStringBinder));

        return null;
    }
}
```

- Startup.css

```csharp
 public class Startup
 {
     ...

     public void ConfigureServices(IServiceCollection services)
     {
         services.AddControllersWithViews(x=> {
             x.ModelBinderProviders.Insert(0, new SecureStringBinderProvider());
         });
     }

     ...
 }

```
