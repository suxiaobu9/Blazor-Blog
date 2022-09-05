using Blazor.Blog.Server.Service;
using Blazor.Blog.Shared.Article;
using Blazor.Blog.Shared.Enum;
using Microsoft.AspNetCore.Mvc;

namespace Blazor.Blog.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class ApiController : ControllerBase
{
    private readonly ArticleService articleService;

    public ApiController(ArticleService articleService)
    {
        this.articleService = articleService;
    }

    [HttpGet("Article/{type}")]
    public IActionResult Index(ArticleTypeEnum type, int? page)
    {
        page ??= 0;
        var result = articleService.GetArticleIntroduction(type, page.Value);

        if (result == null)
            return NotFound();

        return Ok(result);

    }
}
