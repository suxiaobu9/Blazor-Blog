# 在.net core 中使用 Quatuz.net

## 環境

- .net 5

## Quartz.net 的主要概念

1. job : 執行任務
2. trigger : 任務觸發器
3. scheduler : 協調 job 與 trigger

## 安裝 Quartz.net

- 指令 `dotnet add package Quartz.Extensions.Hosting`

  [![image](https://user-images.githubusercontent.com/37999690/131617491-ae2c09f2-3ea8-4c3a-9318-8e62fafa6d64.png "image")](https://user-images.githubusercontent.com/37999690/131617491-ae2c09f2-3ea8-4c3a-9318-8e62fafa6d64.png)

## 實做

先建立一個簡單的工作

```csharp
using Microsoft.Extensions.Logging;
using Quartz;
using System;
using System.Threading.Tasks;

namespace WebApplication2.Service
{
    // 防止Quartz同時執行同一個作業
    [DisallowConcurrentExecution]
    public class Alert :IJob
    {
        private readonly ILogger<Alert> _logger;
        public Alert(ILogger<Alert> logger)
        {
            _logger = logger;
        }

        public Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation($"{DateTime.Now:mm:ss} : Hello Quartz !");
            return Task.CompletedTask;
        }
    }
}

```

將工作註冊進排程

```csharp
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Quartz;
using WebApplication2.Service;

namespace WebApplication2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) =>
            {
                services.AddQuartz(q =>
                {
                    q.UseMicrosoftDependencyInjectionJobFactory();
                    var jobKey = new JobKey("AlertJob");
                    q.AddJob<Alert>(opts => opts.WithIdentity(jobKey));
                    q.AddTrigger(opts => opts.ForJob(jobKey)
                                            .WithIdentity("AlertJob-trigger")
                                            .WithCronSchedule("0 0/1 * * * ?"));

                });
                services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);
            }).ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
    }
}

```

設定執行的時間的地方是 `WithCronSchedule("0 0/1 * * * ?")` 以 Cron 表達式來設定 job 時間

執行後每 1 分鐘會執行一次

[![image](https://user-images.githubusercontent.com/37999690/131632834-696b8722-b901-4530-b9d9-2a4dfdc018a6.png "image")](https://user-images.githubusercontent.com/37999690/131632834-696b8722-b901-4530-b9d9-2a4dfdc018a6.png)

## 簡單改一下

一般情況下，會把 cron 寫在 `appsettings.json`

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning",
      "Microsoft.Hosting.Lifetime": "Information"
    }
  },
  "AllowedHosts": "*",
  "Quartz": {
    "AlertJob": "0 0/1 * * * ?"
  }
}
```

把註冊服務的邏輯簡單封裝一下，比較方便使用

```csharp
using Microsoft.Extensions.Configuration;
using Quartz;
using System;

namespace WebApplication2
{
    public static class QuartzConfiguratorExtension
    {
        public static void AddJobAndTrigger<T>(
            this IServiceCollectionQuartzConfigurator quartz,
            IConfiguration config) where T : IJob
        {
            var jobName = typeof(T).Name;

            var appsettingKey = $"Quartz:{jobName}";
            var cronSchedule = config[appsettingKey];

            if (string.IsNullOrWhiteSpace(cronSchedule))
                throw new Exception($"Appsetting 中沒有 {appsettingKey} 的 cron 設定 ");

            var jobKey = new JobKey(jobName);
            quartz.AddJob<T>(opts => opts.WithIdentity(jobKey));

            quartz.AddTrigger(opts => opts.ForJob(jobKey)
                                            .WithIdentity($"{jobName}-trigger")
                                            .WithCronSchedule(cronSchedule));

        }
    }
}

```

修改 Program.cs，使用剛剛封裝的方法

```csharp
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Quartz;
using WebApplication2.Service;

namespace WebApplication2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) =>
            {
                services.AddQuartz(q =>
                {
                    q.UseMicrosoftDependencyInjectionJobFactory();
                    q.AddJobAndTrigger<AlertJob>(hostContext.Configuration);
                });
                services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);
            }).ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
    }
}

```

再執行一次，還是要會 1 分鐘執行一次

[![image](https://user-images.githubusercontent.com/37999690/131639531-bde98c7e-384a-4182-bcf8-dd55a76989bc.png "image")](https://user-images.githubusercontent.com/37999690/131639531-bde98c7e-384a-4182-bcf8-dd55a76989bc.png)
