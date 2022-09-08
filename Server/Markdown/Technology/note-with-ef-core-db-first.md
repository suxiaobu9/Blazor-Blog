# EF Core Db first 筆記

## 環境

- .net 5
- Nuget - Microsoft.EntityFrameworkCore.SqlServer 5.0.x
- Nuget - Microsoft.EntityFrameworkCore.Design 5.0.x

## 開始

1. 安裝 dotnet ef 工具 `PS > dotnet tool install --global dotnet-ef`
2. 在有加入`Microsoft.EntityFrameworkCore.Design`的專案路徑(\*.csproj)下，使用指令來產生或是更新資料庫的相關檔案

   ```powershell
    # -o 輸出資料夾，不填就輸出在當前資料夾
    # -c 產生的Context名稱，不填就預設{DB_name}Context
    # -f 複寫原有檔案
    # https://docs.microsoft.com/zh-tw/ef/core/cli/dotnet#dotnet-ef-dbcontext-scaffold
    dotnet ef dbcontext scaffold "server=localhost,1433;database=EFTest;user=******;password=******;" "Microsoft.EntityFrameworkCore.SqlServer" -o .\ -c EFTestContext -f
   ```

   > 若是出現
   >
   > [![image](https://user-images.githubusercontent.com/37999690/126607840-0c29b2cb-69d5-40dc-9b11-e3c57b8a9a1b.png "image")](https://user-images.githubusercontent.com/37999690/126607840-0c29b2cb-69d5-40dc-9b11-e3c57b8a9a1b.png)
   >
   > 代表需要重 build 一下，或是沒有加入 Microsoft.EntityFrameworkCore.Design

3. 執行完成後會多幾個檔案，包含 EFTestContext 以及和資料庫相對應的 cs model
   [![image](https://user-images.githubusercontent.com/37999690/126612695-17851911-36f9-439d-bd92-2031e37e4cf6.png "image")](https://user-images.githubusercontent.com/37999690/126612695-17851911-36f9-439d-bd92-2031e37e4cf6.png)

4. 將 Context 註冊進 Startup

   ```csharp
   public void ConfigureServices(IServiceCollection services)
   {
       //每次Call Method都注入一個新的
       //services.AddTransient

       //每個LifeCycle注入一個新的
       //services.AddScoped

       //只會在站台啟動時注入一個新的
       //services.AddSingleton
       services.AddTransient<EFTestContext>();
       services.AddControllers();
       services.AddSwaggerGen(c =>
       {
           c.SwaggerDoc("v1", new OpenApiInfo { Title = "EFCoreDemo", Version = "v1" });
       });
   }
   ```

5. 注入後就可以使用了

   ```csharp
   [Route("api/[controller]")]
   [ApiController]
   public class EFTestController : ControllerBase
   {
       private readonly EFTestContext _db;

       public EFTestController(EFTestContext db)
       {
           _db = db;
       }

       [HttpGet]
       public IActionResult Get()
       {
           var employees = _db.Employees.ToList();
           return Ok();
       }
   }
   ```

## 玩一下

- 如果下一堆關聯的 where 條件會 Join 一堆 table，來實驗一下
- 用 SQL Server Profiler 抓語法

1. `var employees = _db.Employees.ToList();`

   ```sql
   SELECT [e].[Id], [e].[DeptId], [e].[Name], [e].[NickName]
   FROM [Employee] AS [e]
   ```

2. `var employees = _db.Employees.Where(x => x.Dept.Id == 1).ToList();`

   ```sql
   SELECT [e].[Id], [e].[DeptId], [e].[Name], [e].[NickName]
   FROM [Employee] AS [e]
   INNER JOIN [Department] AS [d] ON [e].[DeptId] = [d].[Id]
   WHERE [d].[Id] = 1
   ```

3. `var employees = _db.Employees.Where(x => x.Dept.Id == 1 && x.Dept.Comp.Id == 2).ToList();`

   ```sql
   SELECT [e].[Id], [e].[DeptId], [e].[Name], [e].[NickName]
   FROM [Employee] AS [e]
   INNER JOIN [Department] AS [d] ON [e].[DeptId] = [d].[Id]
   INNER JOIN [Company] AS [c] ON [d].[CompId] = [c].[Id]
   WHERE ([d].[Id] = 1) AND ([c].[Id] = 2)
   ```

## 不負責任的 EF core 與 Dapper 效能比較

1. 單純 Select

   ```csharp
   [HttpGet]
   public IActionResult Get()
   {
       var format = "yyyy/MM/dd HH:mm:ss";

       var ef_Start = DateTime.Now.ToString(format);
       for (var i = 0; i < 100000; i++)
       {
           _db.Employees.ToList();
       }
       var ef_End = DateTime.Now.ToString(format);

       var dapper_Start = DateTime.Now.ToString(format);
       for (var i = 0; i < 100000; i++)
       {
           var sql = @"SELECT * FROM Employee";
           using var _defaultConn = new SqlConnection("server=localhost,1433;database=EFTest;user=******;password=******;");
           _defaultConn.Query<Employee>(sql);
       }
       var dapper_End = DateTime.Now.ToString(format);
       return Ok(new { ef_Start, ef_End, dapper_Start, dapper_End });
   }

   ```

   [![image](https://user-images.githubusercontent.com/37999690/126640823-32784de6-c293-4fa4-bd42-04d87b35b024.png "image")](https://user-images.githubusercontent.com/37999690/126640823-32784de6-c293-4fa4-bd42-04d87b35b024.png)

   [![image](https://user-images.githubusercontent.com/37999690/126641589-f9a84d0d-ddc9-466f-8ebe-950a9b79aae4.png "image")](https://user-images.githubusercontent.com/37999690/126641589-f9a84d0d-ddc9-466f-8ebe-950a9b79aae4.png)

   [![image](https://user-images.githubusercontent.com/37999690/126642475-f9d9ad4d-7dde-4bde-aad7-0f24b09439ca.png "image")](https://user-images.githubusercontent.com/37999690/126642475-f9d9ad4d-7dde-4bde-aad7-0f24b09439ca.png)

2. Join 一張 Table

   ```csharp
   [HttpGet]
   public IActionResult Get()
   {
       var format = "yyyy/MM/dd HH:mm:ss";

       var ef_Start = DateTime.Now.ToString(format);
       for (var i = 0; i < 100000; i++)
       {
           _db.Employees.Where(x => x.Dept.Id == 1).ToList();
       }
       var ef_End = DateTime.Now.ToString(format);

       var dapper_Start = DateTime.Now.ToString(format);
       for (var i = 0; i < 100000; i++)
       {
           var sql = @"SELECT [e].[Id], [e].[DeptId], [e].[Name], [e].[NickName]
           FROM [Employee] AS [e]
           INNER JOIN [Department] AS [d] ON [e].[DeptId] = [d].[Id]
           WHERE [d].[Id] = 1";

           using var _defaultConn = new SqlConnection("server=localhost,1433;database=EFTest;user=******;password=******;");
           _defaultConn.Query<Employee>(sql);
       }
       var dapper_End = DateTime.Now.ToString(format);
       return Ok(new { ef_Start, ef_End, dapper_Start, dapper_End });
   }
   ```

   [![image](https://user-images.githubusercontent.com/37999690/126725804-298fe272-ca6a-431d-bc95-9f560a89a38e.png "image")](https://user-images.githubusercontent.com/37999690/126725804-298fe272-ca6a-431d-bc95-9f560a89a38e.png)

   [![image](https://user-images.githubusercontent.com/37999690/126726218-0f445f5b-0718-43cc-bde2-b387032eff18.png "image")](https://user-images.githubusercontent.com/37999690/126726218-0f445f5b-0718-43cc-bde2-b387032eff18.png)

   [![image](https://user-images.githubusercontent.com/37999690/126726690-e2dd309b-f398-4623-9398-6375cf53667d.png "image")](https://user-images.githubusercontent.com/37999690/126726690-e2dd309b-f398-4623-9398-6375cf53667d.png)
