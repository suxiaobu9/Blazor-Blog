using Blazor.Blog.Shared.Model;

namespace Blazor.Blog.Shared.Article;

public static partial class ArticleList
{
    public static Dictionary<string, ArticleIntroductionModel> Technology => new()
    {
        {
            "use-lets-encrypt-to-update-web-site-to-https-free",
            new ArticleIntroductionModel
            {
                Title = "利用 Let's Encrypt 將網站免費升級成 https",
                DisplayDate = new DateTime(2022,09,12),
            }
        },
        {
            "use-quatuz-with-dotnet-core",
            new ArticleIntroductionModel
            {
                Title = "在.net core中使用Quatuz.net",
                DisplayDate = new DateTime(2021,09,01),
            }
        },
        {
            "set-nuget-ref-url",
            new ArticleIntroductionModel
            {
                Title = "重灌後Visual Studio沒辦法還原Nuget套件",
                DisplayDate = new DateTime(2021,09,01),
                Hints = new string[]{"https://api.nuget.org/v3/index.json"},
            }
        },
        {
            "push-multi-repos-in-one-time",
            new ArticleIntroductionModel
            {
                Title = "Git一次性Push至多個Remote Repository",
                DisplayDate = new DateTime(2021,08,06),
            }
        },
        {
            "docker-containers-connunicate-to-each-other",
            new ArticleIntroductionModel
            {
                Title = "Docker Containers 之間相互溝通",
                DisplayDate = new DateTime(2021,08,04),
                Hints = new string[]{"docker network ls", "docker network inspect bridge"},
            }
        },
        {
            "note-with-ef-core-db-first",
            new ArticleIntroductionModel
            {
                Title = "EF Core Db first 筆記",
                DisplayDate = new DateTime(2021,07,23),
            }
        },
        {
            "appsettings-in-mstest",
            new ArticleIntroductionModel
            {
                Title = "Appsettings in mstest",
                DisplayDate = new DateTime(2021,07,21),
            }
        },
        {
            "angular-cheat-list",
            new ArticleIntroductionModel
            {
                Title = "Angular - Cheat List",
                DisplayDate = new DateTime(2021,07,18),
            }
        },
        {
            "remove-special-dir",
            new ArticleIntroductionModel
            {
                Title = "移除特殊名稱的資料夾",
                DisplayDate = new DateTime(2021,07,14),
                Hints = new string[]{"dir /x", "rename {0} {1}"},
            }
        },
        {
            "custom-model-binder",
            new ArticleIntroductionModel
            {
                Title = "Custom Model Binder",
                DisplayDate = new DateTime(2021,07,14),
            }
        },
        {
            "use-ngrok-to-teast-local-website",
            new ArticleIntroductionModel
            {
                Title = "使用Ngrok測試地端網站",
                DisplayDate = new DateTime(2021,04,26),
                Hints = new string[]{"ngrok http {port} --host-header=\"localhost:{port}\""},
            }
        },
        {
            "use-user-secret-to-protect-parameters",
            new ArticleIntroductionModel
            {
                Title = "使用 User Secret 保護程式參數",
                DisplayDate = new DateTime(2021,04,26),
            }
        },
        {
            "vue-2-develop-with-multi-envs",
            new ArticleIntroductionModel
            {
                Title = "Vue 2 多環境開發",
                DisplayDate = new DateTime(2021,04,12),
            }
        },
        {
            "azure-devops-auto-deploy",
            new ArticleIntroductionModel
            {
                Title = "Azure DevOps Auto Deploy",
                DisplayDate = new DateTime(2021,04,01),
            }
        },
        {
            "package-dotnet-core-as-image",
            new ArticleIntroductionModel
            {
              Title = "將.net core 包成 Image",
              DisplayDate = new DateTime(2021,03,30),
            }
        },
        {
            "dotnet-core-3-to-inject-multi-instances-with-same-interface",
            new ArticleIntroductionModel
            {
                Title = ".net core 3 注入相同介面、多個實作",
                DisplayDate = new DateTime(2021,03,15),
            }
        },
        {
            "jquery-fuzzy-matching",
            new ArticleIntroductionModel
            {
                Title = "jQuery checkbox 模糊匹配",
                DisplayDate = new DateTime(2021,02,16),
            }
        },
        {
            "jquery-map-grep-each",
            new ArticleIntroductionModel
            {
                Title = "jQuery陣列操作：$map()、$.grep()、$.each()",
                DisplayDate = new DateTime(2021,01,27),
                Hints = new string[]
                {
                    "$.each([], function (index, item)",
                    "$.grep([], function (item, index)",
                    "$.map([], function (item, index)"
                }
            }
        },
    };
}
