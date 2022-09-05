using Blazor.Blog.Shared.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazor.Blog.Shared.Article;

public static partial class ArticleList
{
    public static Dictionary<string, ArticleIntroductionModel> Recipe => new()
    {
        {
            "酸梅蜜汁雞排",
            new ArticleIntroductionModel
            {
                Title = "酸梅蜜汁雞排",
                DisplayDate = new DateTime(2021, 04, 26),
            }
        },
        {
            "滷雞翅",
            new ArticleIntroductionModel
            {
                Title = "滷雞翅",
                DisplayDate = new DateTime(2021, 03, 16),
            }
        },
        {
            "雞蛋糕、鬆餅麵糊",
            new ArticleIntroductionModel
            {
                Title = "雞蛋糕、鬆餅麵糊",
                DisplayDate = new DateTime(2021, 02, 17),
            }
        },
        {
            "滷肉",
            new ArticleIntroductionModel
            {
                Title = "滷肉",
                DisplayDate = new DateTime(2021, 02, 17),
            }
        },
        {
            "煎【蚵仔煎或蝦仁煎的基底】",
            new ArticleIntroductionModel
            {
                Title = "煎【蚵仔煎或蝦仁煎的基底】",
                DisplayDate = new DateTime(2021, 02, 17),
            }
        },
        {
            "微甜豆漿",
            new ArticleIntroductionModel
            {
                Title = "微甜豆漿",
                DisplayDate = new DateTime(2021, 02, 17),
            }
        },
        {
            "奶酪 (微甜配方)",
            new ArticleIntroductionModel
            {
                Title = "奶酪 (微甜配方)",
                DisplayDate = new DateTime(2021, 02, 17),
            }
        },
        {
            "不專業椒麻雞 【需醃漬30分鐘或過夜】",
            new ArticleIntroductionModel
            {
                Title = "不專業椒麻雞 【需醃漬30分鐘或過夜】",
                DisplayDate = new DateTime(2021, 02, 17),
            }
        }
    };
}