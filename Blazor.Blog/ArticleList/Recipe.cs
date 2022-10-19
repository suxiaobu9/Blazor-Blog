using Blazor.Blog.Model;

namespace Blazor.Blog;

public static partial class ArticleList
{
    public static Dictionary<string, ArticleIntroductionModel> Recipe => new()
    {
        {
            "麻油雞飯",
            new ArticleIntroductionModel
            {
                Title = "麻油雞飯",
                DisplayDate = new DateTime(2022, 10, 18),
                Hints = new string[]
                {
                    "麻油",
                    "雞腿肉",
                    "薑"
                }
            }
        },
        {
            "雞腿悶飯",
            new ArticleIntroductionModel
            {
                Title = "雞腿悶飯",
                DisplayDate = new DateTime(2022, 09, 11),
                Hints = new string[]
                {
                    "雞腿肉",
                    "飯",
                }
            }
        },
        {
            "酸梅蜜汁雞排",
            new ArticleIntroductionModel
            {
                Title = "酸梅蜜汁雞排",
                DisplayDate = new DateTime(2021, 04, 26),
                Hints = new string[]
                {
                    "雞腿肉",
                    "酸梅",
                }
            }
        },
        {
            "滷雞翅",
            new ArticleIntroductionModel
            {
                Title = "滷雞翅",
                DisplayDate = new DateTime(2021, 03, 16),
                Hints = new string[]
                {
                    "雞翅",
                }
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
            "微甜奶酪",
            new ArticleIntroductionModel
            {
                Title = "微甜奶酪",
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
