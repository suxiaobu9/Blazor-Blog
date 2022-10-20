using Blazor.Blog.Enum;
using Microsoft.AspNetCore.Components;

namespace Blazor.Blog.Component;

public partial class ArticleTypeBase
{
    /// <summary>
    /// 文章分類的判斷字串
    /// </summary>
    [Parameter]
    public string? ArticleType { get; set; }

    /// <summary>
    /// 文章分類
    /// </summary>
    protected ArticleTypeEnum ArticleTypeEnum
    {
        get
        {
            if (!string.IsNullOrWhiteSpace(ArticleType))
            {
                ArticleType = ArticleType?.ToLower();
                ArticleType = string.Concat(ArticleType?[0].ToString().ToUpper(), ArticleType.AsSpan(1));
            }

            return ArticleType switch
            {
                nameof(ArticleTypeEnum.Technology) => ArticleTypeEnum.Technology,
                nameof(ArticleTypeEnum.Recipe) => ArticleTypeEnum.Recipe,
                _ => ArticleTypeEnum.Technology,
            };
        }
    }
}
