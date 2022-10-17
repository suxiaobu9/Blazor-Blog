using Blazor.Blog.Enum;
using Blazor.Blog.Model;
using System.Reflection;

namespace Blazor.Blog;

public class ArticleService
{
    /// <summary>
    /// 取得文章列表
    /// </summary>
    /// <param name="type"></param>
    /// <param name="page"></param>
    /// <param name="keyword"></param>
    /// <returns></returns>
    public PagingModel? GetArticleIntroduction(ArticleTypeEnum type, int page, string? keyword)
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

        var articleList = KeywordFilter(source, keyword).ToArray();

        var totalPage = 1;

        if (articleList.Any())
        {
            totalPage = articleList.Length / pageSize + (articleList.Length % pageSize == 0 ? 0 : 1);
        }

        return new PagingModel
        {
            CurrentPage = page,
            TotalPage = totalPage,
            ArticleIntroductions = articleList.Skip(skipPages).Take(pageSize).ToArray()
        };
    }

    /// <summary>
    /// 過濾關鍵字
    /// </summary>
    /// <param name="source"></param>
    /// <param name="keyword"></param>
    /// <returns></returns>
    private IEnumerable<ArticleIntroductionModel> KeywordFilter(Dictionary<string, ArticleIntroductionModel> source, string? keyword)
    {
        return source.Where(x =>
        {
            var model = x.Value;

            if (string.IsNullOrWhiteSpace(keyword))
                return true;
            else if (!string.IsNullOrWhiteSpace(x.Key) && x.Key.ToLower().Contains(keyword))
                return true;
            else if (!string.IsNullOrWhiteSpace(model.Title) && model.Title.ToLower().Contains(keyword))
                return true;

            return false;
        }).Select(x =>
        {
            var model = x.Value;

            model.NickName = x.Key;
            return model;
        });
    }

}
