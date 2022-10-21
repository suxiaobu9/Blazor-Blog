using Blazor.Blog.Model;
using Blazor.Blog.Service;
using Markdig;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Blazor.Blog.Pages;
public partial class Article
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    [Inject]
    private HttpClient HttpClient { get; set; }

    [Inject]
    private IJSRuntime JsRuntime { get; set; }

    [Inject]
    private ArticleService ArticleService { get; set; }

    [Inject]
    private NavigationManager UrlHelper { get; set; }

#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    [Parameter]
    public string? Nickname { get; set; }

    private ArticleModel? ArticleModel { get; set; }

    private string? HtmlContent { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        await LoadingArticle();
    }

    /// <summary>
    /// 取得文章
    /// </summary>
    /// <param name="articleList"></param>
    /// <returns></returns>
    private ArticleIntroductionModel? GetArticleByNickname(ArticleIntroductionModel[] articleList)
    {
        var hasNickName = articleList.Any(x => x.NickName == Nickname);

        if (!hasNickName) return null;

        ArticleIntroductionModel? model = articleList.FirstOrDefault(x => x.NickName == Nickname);

        return model;

    }

    /// <summary>
    /// 讀取文章內容
    /// </summary>
    /// <returns></returns>
    private async Task LoadingArticle()
    {
        var checkAry = (await ArticleService.GetArticleList(ArticleTypeEnum)).ToArray();

        var articleIntroduction = GetArticleByNickname(checkAry);

        if (articleIntroduction == null)
        {
            UrlHelper.NavigateTo("/", true);
            return;
        }

        ArticleModel = new ArticleModel(articleIntroduction)
        {
            // 文章內容
            MdContent = await HttpClient.GetStringAsync($"/Markdown/{articleIntroduction.ArticleTypeEnum}/{articleIntroduction.NickName}.md?v={DateTime.UtcNow.Ticks}")
        };

        // 轉 html
        HtmlContent = Markdown.ToHtml(ArticleModel.MdContent ?? "");

        StateHasChanged();

        await JsRuntime.InvokeVoidAsync("OnScrollEvent");
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await JsRuntime.InvokeVoidAsync("Prism.highlightAll");
    }

    protected override async Task OnParametersSetAsync()
    {
        await base.OnParametersSetAsync();
        await LoadingArticle();
    }
}
