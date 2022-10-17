namespace Blazor.Blog.Model;

public class ArticleIntroductionModel
{
    public string? Title { get; set; }

    public string? NickName { get; set; }

    public DateTime? DisplayDate { get; set; }

    public string[]? Hints { get; set; }

    public string[]? SEOKeywords { get; set; }
}
