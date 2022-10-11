namespace Blazor.Blog.Shared.Model;

public class PagingModel
{
    public int? TotalPage { get; set; }

    public int? CurrentPage { get; set; }

    public ArticleIntroductionModel[]? ArticleIntroductions { get; set; }
}

