namespace Blazor.Blog.Shared.Model;

public record class ArticleIntroductionModel
{
    public string? Title { get; set; }

    public string? NickName { get; set; }

    public DateTime? DisplayDate { get; set; }

    public string[]? Hints { get; set; }

    public string[]? SEOKeywords { get; set; } 
}
