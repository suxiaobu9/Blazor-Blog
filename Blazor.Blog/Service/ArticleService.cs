using Blazor.Blog.Component;
using Blazor.Blog.Enum;
using Blazor.Blog.Model;
using System.Net.Http.Json;

namespace Blazor.Blog.Service;

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

        var totalPage = 1;

        if (source.Any())
        {
            totalPage = source.Length / pageSize + (source.Length % pageSize == 0 ? 0 : 1);
        }

        return new PagingModel
        {
            CurrentPage = page,
            TotalPage = totalPage,
            ArticleIntroductions = source.Skip(skipPages).Take(pageSize).ToArray()
        };
    }

    /// <summary>
    /// 取得文章列表
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public async Task<IEnumerable<ArticleIntroductionModel>> GetArticleList(ArticleTypeEnum? type)
    {
        var jsonNameAry = new List<ArticleTypeEnum>();

        if (type == ArticleTypeEnum.Recipe || type == null)
            jsonNameAry.Add(ArticleTypeEnum.Recipe);

        if (type == ArticleTypeEnum.Technology || type == null)
            jsonNameAry.Add(ArticleTypeEnum.Technology);

        var tasks = jsonNameAry.Select(x => (x, httpClient.GetFromJsonAsync<ArticleIntroductionModel[]>($"article-list/{x}.json")));

        var result = new List<ArticleIntroductionModel>();

        foreach ((ArticleTypeEnum articletype, Task<ArticleIntroductionModel[]?> task) item in tasks)
        {
            var tmp = (await item.task)?.Select(x =>
            {
                x.ArticleTypeEnum = item.articletype;
                return x;
            }).ToArray();

            result.AddRange(tmp ?? Array.Empty<ArticleIntroductionModel>());
        }

        return result;
    }

    public async Task<ArticleIntroductionModel[]> GetArticleSearchResult(string[]? keywords)
    {
        if (keywords == null)
            return Array.Empty<ArticleIntroductionModel>();

        keywords = keywords.Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();

        if (keywords.Length == 0)
            return Array.Empty<ArticleIntroductionModel>();

        ArticleIntroductionModel[] allArticleIntroductions = (await GetArticleList(null)).ToArray();

        Task<string>[] getMdContentTasks = allArticleIntroductions
            .Select(x => httpClient.GetStringAsync($"/Markdown/{x.ArticleTypeEnum}/{x.NickName}.md"))
            .ToArray();

        var articleWithMdContent = new (string mdContent, ArticleIntroductionModel articleModel)[getMdContentTasks.Length];

        for (var i = 0; i < getMdContentTasks.Length; i++)
            articleWithMdContent[i] = (await getMdContentTasks[i], allArticleIntroductions[i]);

        keywords = keywords.Select(x => x.ToLower()).ToArray();

        foreach (var keyword in keywords)
        {
            articleWithMdContent = articleWithMdContent.Where(x =>
            {
                var tmp = new List<string>();

                if (!string.IsNullOrWhiteSpace(x.mdContent))
                    tmp.Add(x.mdContent);

                if (x.articleModel.Hints != null)
                    tmp.AddRange(x.articleModel.Hints);

                if (x.articleModel.SEOKeywords != null)
                    tmp.AddRange(x.articleModel.SEOKeywords);

                if (!string.IsNullOrWhiteSpace(x.articleModel.NickName))
                    tmp.Add(x.articleModel.NickName);

                if (!string.IsNullOrWhiteSpace(x.articleModel.Title))
                    tmp.Add(x.articleModel.Title);

                tmp = tmp.Select(x => x.ToLower()).ToList();

                return tmp.Any(x => x.Contains(keyword));

            }).ToArray();
        }

        return articleWithMdContent.Select(x => x.articleModel).ToArray();
    }

}
