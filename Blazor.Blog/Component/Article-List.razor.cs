using Blazor.Blog.Enum;
using Blazor.Blog.Model;
using Blazor.Blog.State;
using Microsoft.AspNetCore.Components;

namespace Blazor.Blog.Component;


public partial class Article_List : IDisposable
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    [Inject]
    private NavigationManager UriHelper { get; set; }

    [Inject]
    private KeywordSearchState KeywordSearchState { get; set; }

    [Inject]
    private ArticleService ArticleService { get; set; }

#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    [Parameter]
    public string? ArticleType { get; set; }

    [Parameter]
    public int? CurrentPage { get; set; }

    [Parameter]
    public string? Keyword { get; set; }

    private PagingModel PagingModel { get; set; } = new PagingModel();

    private ArticleTypeEnum Type
    {
        get
        {
            if (!string.IsNullOrWhiteSpace(ArticleType))
            {
                ArticleType = ArticleType?.ToLower();
                ArticleType = string.Concat(ArticleType?[0].ToString().ToUpper(), ArticleType.AsSpan(1));
            }

            return ArticleType switch
            {
                nameof(ArticleTypeEnum.Technology) => ArticleTypeEnum.Technology,
                nameof(ArticleTypeEnum.Recipe) => ArticleTypeEnum.Recipe,
                _ => ArticleTypeEnum.Technology,
            };
        }
    }

    public void PageChanged(int page)
    {
        PagingModel.CurrentPage = page;

        var type = Type == ArticleTypeEnum.Technology ? "" : Type.ToString();
        var queryString = GetQueryString();

        UriHelper.NavigateTo($"/{type}{queryString}");
    }

    protected override async Task OnParametersSetAsync()
    {
        PagingModel.CurrentPage = CurrentPage;
        KeywordSearchState.SetKeyword(Keyword);

        await GetArticleListAsync();
        await base.OnParametersSetAsync();
    }

    private async Task GetArticleListAsync()
    {
        var model = await ArticleService.GetArticleIntroduction(Type, CurrentPage ?? 1, Keyword);

        if (model == null)
            return;

        PagingModel = model;
    }

    protected override Task OnInitializedAsync()
    {
        KeywordSearchState.OnChange += SetKeyword;
        return base.OnInitializedAsync();
    }

    private void SetKeyword()
    {
        // 處理換頁
        if (Keyword != KeywordSearchState.Keyword)
        {
            Keyword = KeywordSearchState.Keyword;
            PagingModel.CurrentPage = 1;
        }

        var type = Type == ArticleTypeEnum.Technology ? "" : Type.ToString();

        var queryString = GetQueryString();

        UriHelper.NavigateTo($"/{type}{queryString}");
    }

    /// <summary>
    /// 取得 query string
    /// </summary>
    /// <param name="page"></param>
    /// <param name="keyword"></param>
    /// <returns></returns>
    private string GetQueryString(int? page = null, string? keyword = null)
    {
        var queryStringDic = new Dictionary<string, object>();

        if (PagingModel.CurrentPage != null && PagingModel.CurrentPage != 1)
        {
            queryStringDic.Add(nameof(PagingModel.CurrentPage), PagingModel.CurrentPage ?? 1);
        }

        if (!string.IsNullOrWhiteSpace(Keyword))
        {
            queryStringDic.Add(nameof(Keyword), Keyword);
        }

        var queryString = "";

        if (queryStringDic.Any())
            queryString = "?" + string.Join("&", queryStringDic.Select(x => $"{x.Key}={x.Value}").ToArray() ?? Array.Empty<string>());

        return queryString;
    }

    public void Dispose()
    {
        KeywordSearchState.OnChange -= SetKeyword;
    }
}
