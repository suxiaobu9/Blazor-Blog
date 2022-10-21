using Blazor.Blog.Component;

namespace Blazor.Blog.Model;

public class ArticleModel
{
    public ArticleModel(ArticleIntroductionModel articleIntroduction)
    {
        Description = $"{articleIntroduction.NickName} - {articleIntroduction.Title}";

        SEOKeyword = string.Join(",", articleIntroduction.SEOKeywords ?? Array.Empty<string>());
        
        Hint = string.Join(",", articleIntroduction.Hints?? Array.Empty<string>());

    }

    public string? SEOKeyword { get; set; }

    public string? Description { get; set; }

    public string? Hint { get; set; }

    public string? MdContent { get; set; }
}

