using Blazor.Blog.Shared.Article;
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

        return new PagingModel
        {
            CurrentPage = page,
            TotalPage = (articleList.Count() / pageSize) + 1,
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
    public async Task<string?> GetMdContentAsync(string nickname)
    {
        var targetMd = GetFilePath(nickname);

        if (targetMd == null)
            return null;

        return await File.ReadAllTextAsync(targetMd);
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
