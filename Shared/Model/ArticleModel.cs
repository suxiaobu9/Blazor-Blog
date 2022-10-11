using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazor.Blog.Shared.Model;

public class ArticleModel
{
    public string? MarkdownContent { get; set; }

    public string? SEOKeyword { get; set; }

    public string? Description { get; set; }
}
