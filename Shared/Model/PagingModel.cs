namespace Blazor.Blog.Shared.Model;

public class PagingModel
{
    public int? Total { get; set; }

    public int? Page { get; set; }

    public ArticleIntroductionModel[]? ArticleIntroductions { get; set; }
}

