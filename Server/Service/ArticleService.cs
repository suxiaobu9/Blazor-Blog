using Blazor.Blog.Shared.Article;
using Blazor.Blog.Shared.Enum;
using Blazor.Blog.Shared.Model;

namespace Blazor.Blog.Server.Service;

public class ArticleService
{
    public PagingModel? GetArticleIntroduction(ArticleTypeEnum type, int page)
    {
        int pageSize = 12,
            skipPages = (page - 1) * pageSize;

        var source = type switch
        {
            ArticleTypeEnum.Recipe => ArticleList.Recipe,
            ArticleTypeEnum.Technology => ArticleList.Technology,
            _ => null
        };

        if (source == null)
            return null;

        return new PagingModel
        {
            Page = page,
            Total = (source.Count / pageSize) + 1,
            ArticleIntroductions = 
            source.Skip(skipPages).Take(pageSize)
            .Select(x => new ArticleIntroductionModel
            {
                Title = x.Value.Title,
                DisplayDate = x.Value.DisplayDate,
                Hints = x.Value.Hints,
                NickName = x.Key
            }).ToArray()
        };
    }
}
