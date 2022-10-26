# Blazor 取得 URL 參數與 Query Strings 的方式

## Page Route parameters

### [路由限制 - Route constraints](https://learn.microsoft.com/en-us/aspnet/core/blazor/fundamentals/routing?view=aspnetcore-5.0#route-constraints-1)

新增 Route Template 並且修改一下參數的 Attribute

```C#
@page "/counter/{currentCount:int?}"

...

@code {
    [Parameter]
    public int? currentCount { get; set; }

    protected override void OnInitialized()
    {
        currentCount = currentCount ?? 0;
    }
}

```

![image](https://user-images.githubusercontent.com/37999690/197954798-75950616-9aa6-481a-93d4-a883e1d893c1.png)

![image](https://user-images.githubusercontent.com/37999690/197955994-4562dedd-dabe-4a1f-8991-11850560787a.png)

## Query Strings - Blazor .Net 6

### `[SupplyParameterFromQuery]` 僅限於有設定 `@page` 的 `Component`

`/demo?param1=aaa&param2=bbb&Param3=ccc&paramAry=ddd&paramAry=eee`

```C#
@page "/Demo"

<PageTitle>Demo</PageTitle>

@nameof(Param1) : @Param1
<br />
@nameof(param2) : @param2
<br />
@nameof(paramThree) : @paramThree
<br />
@nameof(paramAry) : @(paramAry == null ? "" : string.Join("、", paramAry))

@code {
    [Parameter]
    [SupplyParameterFromQuery]
    public string? Param1 { get; set; }

    [Parameter]
    [SupplyParameterFromQuery]
    public string? param2 { get; set; }

    [Parameter]
    [SupplyParameterFromQuery(Name = "param3")]
    public string? paramThree { get; set; }

    [Parameter]
    [SupplyParameterFromQuery]
    public string[]? paramAry { get; set; }
}
```

![image](https://user-images.githubusercontent.com/37999690/197961441-702efff4-0344-4e80-b27a-f3c964160c94.png)

## Query Strings - Blazor .Net 5 或是沒有 Route Template 的 Component

需要先安裝套件 [Microsoft.AspNetCore.WebUtilities](https://www.nuget.org/packages/Microsoft.AspNetCore.WebUtilities/2.2.0)

`/demo?param1=aaa&paramAry=ddd&paramAry=eee`

```C#
@page "/Demo"
@using Microsoft.AspNetCore.WebUtilities
@inject NavigationManager NavigationManager

<PageTitle>Demo</PageTitle>

@nameof(Param1) : @Param1
<br />
@nameof(paramAry) : @(paramAry == null ? "" : string.Join("、", paramAry))

@code {
    public string? Param1 { get; set; }

    public string[]? paramAry { get; set; }

    protected override void OnInitialized()
    {
        var url = NavigationManager.ToAbsoluteUri(NavigationManager.Uri);

        QueryHelpers.ParseQuery(url.Query).TryGetValue("param1", out var p1);

        Param1 = p1;

        QueryHelpers.ParseQuery(url.Query).TryGetValue("paramAry", out var pAry);

        paramAry = pAry.ToArray();

        StateHasChanged();
    }
}

```

![image](https://user-images.githubusercontent.com/37999690/197974442-2715705b-a7d4-48e4-9228-e4a4d922af03.png)
