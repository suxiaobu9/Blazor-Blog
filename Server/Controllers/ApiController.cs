﻿using Blazor.Blog.Server.Service;
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

    public ApiController(ArticleService articleService)
    {
        this.articleService = articleService;
    }

    /// <summary>
    /// 取得文章列表
    /// </summary>
    /// <param name="type"></param>
    /// <param name="page"></param>
    /// <param name="keyword"></param>
    /// <returns></returns>
    [HttpGet("ArticleIntroductionList/{type}")]
    public IActionResult ArticleIntroductionList(ArticleTypeEnum type, int? page, string? keyword)
    {
        var result = articleService.GetArticleIntroduction(type, page ?? 1, keyword);

        if (result == null)
            return NotFound();

        return Ok(result);
    }

    /// <summary>
    /// 取得文章內容
    /// </summary>
    /// <param name="nickname"></param>
    /// <returns></returns>
    [HttpGet("Article/{nickname}")]
    public async Task<IActionResult> ArticleAsync(string nickname)
    {
        var content = await articleService.GetMdContentAsync(nickname);

        if (content == null)
            return NotFound();

        return Ok(content);
    }
}
