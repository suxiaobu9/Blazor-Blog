using Blazor.Blog.Server.Article;
using Blazor.Blog.Shared.Enum;
using Blazor.Blog.Shared.Model;

namespace Blazor.Blog.Server.Service;

public class ArticleService
{
    private readonly IWebHostEnvironment env;

    public ArticleService(IWebHostEnvironment env)
    {
        this.env = env;
    }

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

        var articleList = KeywordFilter(source, keyword);

        var totalPage = 1;

        if (articleList.Any())
        {
            totalPage = articleList.Count() / pageSize +( articleList.Count() % page == 0 ? 0 : 1);
        }

        return new PagingModel
        {
            CurrentPage = page,
            TotalPage = totalPage,
            ArticleIntroductions = articleList
            .Skip(skipPages).Take(pageSize)
            .ToArray()
        };
    }
    /// <summary>
    /// 取得 Markdown 內容
    /// </summary>
    /// <param name="nickname"></param>
    /// <returns></returns>
    public async Task<ArticleModel?> GetMdContentAsync(string nickname)
    {
        var tech = ArticleList.Technology.ContainsKey(nickname);
        var recipe = ArticleList.Recipe.ContainsKey(nickname);

        if (!tech && !recipe)
            return null;


        var targetDic = tech ?
            ArticleList.Technology.FirstOrDefault(x => x.Key == nickname) :
            ArticleList.Recipe.FirstOrDefault(x => x.Key == nickname);

        var result = new ArticleModel
        {
            Description = $"{targetDic.Key} - {targetDic.Value.Title}",
            SEOKeyword = string.Join(",", targetDic.Value.SEOKeywords ?? Array.Empty<string>())
        };

        var targetMd = GetFilePath(nickname);

        if (targetMd == null)
            return null;

        result.MarkdownContent = await File.ReadAllTextAsync(targetMd);

        return result;
    }

    /// <summary>
    /// 取得 Markdown 內容
    /// </summary>
    /// <param name="nickname"></param>
    /// <returns></returns>
    public string? GetMdContent(string nickname)
    {
        var targetMd = GetFilePath(nickname);

        if (targetMd == null)
            return null;

        return File.ReadAllText(targetMd);
    }

    /// <summary>
    /// 取得檔案路徑
    /// </summary>
    /// <param name="nickname"></param>
    /// <returns></returns>
    private string? GetFilePath(string nickname)
    {
        var markdownDir = Path.Combine(env.ContentRootPath, "Markdown");

        var allFiles = Directory.GetFiles(markdownDir, "*.md", SearchOption.AllDirectories);

        return allFiles.FirstOrDefault(x => Path.GetFileNameWithoutExtension(x) == nickname);
    }

    /// <summary>
    /// 過濾關鍵字
    /// </summary>
    /// <param name="source"></param>
    /// <param name="keyword"></param>
    /// <returns></returns>
    private IEnumerable<ArticleIntroductionModel> KeywordFilter(Dictionary<string, ArticleIntroductionModel> source, string? keyword)
    {
        foreach (var item in source)
        {
            var model = item.Value;

            model.NickName = item.Key;

            if (string.IsNullOrWhiteSpace(keyword))
                yield return model;
            else if (!string.IsNullOrWhiteSpace(model.NickName) && model.NickName.ToLower().Contains(keyword))
                yield return model;
            else if (!string.IsNullOrWhiteSpace(model.Title) && model.Title.ToLower().Contains(keyword))
                yield return model;
            else
            {
                var content = GetMdContent(item.Key);
                if (!string.IsNullOrWhiteSpace(content) && content.ToLower().Contains(keyword))
                    yield return model;
            }

        }
    }
}
