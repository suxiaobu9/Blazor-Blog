using Blazor.Blog.Enum;
using Blazor.Blog.Model;
using Blazor.Blog.State;
using Microsoft.AspNetCore.Components;

namespace Blazor.Blog.Component;


public partial class Article_List
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    [Inject]
    private NavigationManager UriHelper { get; set; }

    [Inject]
    private ArticleService ArticleService { get; set; }

#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
   
    [Parameter]
    public int? CurrentPage { get; set; }

    private PagingModel PagingModel { get; set; } = new PagingModel();

    /// <summary>
    /// 換頁
    /// </summary>
    /// <param name="page"></param>
    public void PageChanged(int page)
    {
        PagingModel.CurrentPage = page;

        var type = ArticleTypeEnum == ArticleTypeEnum.Technology ? "" : ArticleTypeEnum.ToString();
        var queryString = GetQueryString();

        UriHelper.NavigateTo($"/{type}{queryString}");
    }

    protected override async Task OnParametersSetAsync()
    {
        PagingModel.CurrentPage = CurrentPage;

        await GetArticleListAsync();
        await base.OnParametersSetAsync();
    }

    private async Task GetArticleListAsync()
    {
        var model = await ArticleService.GetArticleIntroduction(ArticleTypeEnum, CurrentPage ?? 1);

        if (model == null)
            return;

        PagingModel = model;
    }

    protected override Task OnInitializedAsync()
    {
        return base.OnInitializedAsync();
    }

    /// <summary>
    /// 取得 query string
    /// </summary>
    /// <param name="page"></param>
    /// <param name="keyword"></param>
    /// <returns></returns>
    private string GetQueryString()
    {
        var queryStringDic = new Dictionary<string, object>();

        if (PagingModel.CurrentPage != null && PagingModel.CurrentPage != 1)
        {
            queryStringDic.Add(nameof(PagingModel.CurrentPage), PagingModel.CurrentPage ?? 1);
        }

        var queryString = "";

        if (queryStringDic.Any())
            queryString = "?" + string.Join("&", queryStringDic.Select(x => $"{x.Key}={x.Value}").ToArray() ?? Array.Empty<string>());

        return queryString;
    }
}
