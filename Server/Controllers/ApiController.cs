using Blazor.Blog.Server.Service;
using Blazor.Blog.Shared.Article;
using Blazor.Blog.Shared.Enum;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;

namespace Blazor.Blog.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class ApiController : ControllerBase
{
    private readonly ArticleService articleService;
    private readonly IWebHostEnvironment env;

    public ApiController(ArticleService articleService,
        IWebHostEnvironment env)
    {
        this.articleService = articleService;
        this.env = env;
    }

    [HttpGet("ArticleIntroductionList/{type}")]
    public IActionResult ArticleIntroductionList(ArticleTypeEnum type, int? page)
    {
        page ??= 0;
        var result = articleService.GetArticleIntroduction(type, page.Value);

        if (result == null)
            return NotFound();

        return Ok(result);
    }

    [HttpGet("Article/{nickname}")]
    public async Task<IActionResult> ArticleAsync(string nickname)
    {
        var markdownDir = Path.Combine(env.ContentRootPath, "Markdown");
        var allFiles = Directory.GetFiles(markdownDir, "*.md", SearchOption.AllDirectories);

        var targetMd = allFiles.FirstOrDefault(x => Path.GetFileNameWithoutExtension(x) == nickname);

        if (targetMd == null)
            return NotFound();

        return Ok(await System.IO.File.ReadAllTextAsync(targetMd));
    }




}
