using Blazor.Blog;
using Blazor.Blog.Service;
using Blazor.Blog.Stage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddScoped<ArticleService>();
builder.Services.AddSingleton<KeywordSearchStage>();

await builder.Build().RunAsync();
