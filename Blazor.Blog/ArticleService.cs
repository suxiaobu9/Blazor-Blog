using Blazor.Blog.Enum;
using Blazor.Blog.Model;
using System.Net.Http.Json;
using System.Reflection;
using System.Text.Json.Nodes;

namespace Blazor.Blog;

public class ArticleService
{
    private readonly HttpClient httpClient;

    public ArticleService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    /// <summary>
    /// 取得文章列表
    /// </summary>
    /// <param name="type"></param>
    /// <param name="page"></param>
    /// <param name="keyword"></param>
    /// <returns></returns>
    public async Task<PagingModel?> GetArticleIntroduction(ArticleTypeEnum type, int page)
    {
        int pageSize = 12,
           skipPages = (page - 1) * pageSize;

        var source = (await GetArticleList(type)).ToArray();

        if (source == null)
            return null;

        var articleList = KeywordFilter(source).ToArray();

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
    private IEnumerable<ArticleIntroductionModel> KeywordFilter(ArticleIntroductionModel[] source)
    {
        return source;
    }

    public async Task<IEnumerable<ArticleIntroductionModel>> GetArticleList(ArticleTypeEnum? type)
    {
        var jsonNameAry = new List<ArticleTypeEnum>();

        if (type == ArticleTypeEnum.Recipe || type == null)
            jsonNameAry.Add(ArticleTypeEnum.Recipe);

        if (type == ArticleTypeEnum.Technology || type == null)
            jsonNameAry.Add(ArticleTypeEnum.Technology);

        var tasks = jsonNameAry.Select(x => (x, httpClient.GetFromJsonAsync<ArticleIntroductionModel[]>($"article-list/{x}.json?v={DateTime.UtcNow.Ticks}")));

        var result = new List<ArticleIntroductionModel>();

        foreach ((ArticleTypeEnum articletype, Task<ArticleIntroductionModel[]?> task) item in tasks)
        {
            var tmp = await item.task;
            tmp = tmp?.Select(x =>
                    {
                        x.ArticleTypeEnum = item.articletype;
                        return x;
                    }).ToArray();

            result.AddRange(tmp ?? Array.Empty<ArticleIntroductionModel>());
        }

        return result;
    }

}
