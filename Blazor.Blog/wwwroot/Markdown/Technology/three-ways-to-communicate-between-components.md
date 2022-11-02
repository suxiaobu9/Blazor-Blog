# Blazor 的三種 Components 之間溝通的方式

## EventCallbacks

- 使用 `EventCallbacks` 時並不用像使用 `Action` 或 `Func` 時需要另外呼叫 `StateHasChanged` ， `EventCallbacks` 適合在父子 Components 之間應用

> index.razor

```razor
@page "/"

<PageTitle>Index</PageTitle>

<EventCallBacks EventOnClick="@ClickEvent"></EventCallBacks>

@Counter

@code {
    private int Counter { get; set; }

    private void ClickEvent(int counter)
    {
        this.Counter = counter;
    }
}
```

> EventCallBacks.razor

```razor
<h3>EventCallBacks</h3>

<button @onclick="@BtnClick"> AddCounter </button>

@code {

    [Parameter]
    public EventCallback<int> EventOnClick { get; set; }

    private int Counter { get; set; }

    private void BtnClick()
    {
        EventOnClick.InvokeAsync(++Counter);
    }

}

```

## Cascading Values

- 包在 `<CascadingValue Value="..."> ... </CascadingValue>` 中的 `Component` 都可以取得 Value 中傳入的參數

> index.razor

```razor
@page "/"

<PageTitle>Index</PageTitle>

<CascadingContainer>
    <FirstPage></FirstPage>
</CascadingContainer>

```

> CascadingContainer.razor

```razor
<h3>CascadingContainer</h3>

<div>
    Container User Name : @UserName
</div>
<hr />

<CascadingValue Value="this">
    @ChildContent
</CascadingValue>


@code {

    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    public string UserName { get; set; } = "Ari";

    public void AddTimeTickToUserName()
    {
        UserName = "Ari " + DateTime.UtcNow.Ticks;

        StateHasChanged();
    }
}

```

> FirstPage.razor

```razor
<h3>1st Page</h3>

1st Page User Name : @(CascadingContainer?.UserName)

<div>
    <button @onclick="@(()=>CascadingContainer?.AddTimeTickToUserName())">1st Page Button</button>
</div>
<hr />

<SecondPage></SecondPage>

@code {

    [CascadingParameter]
    public CascadingContainer? CascadingContainer { get; set; }

}

```

> SecondPage.razor

```csharp
<h3>2nd Page</h3>

2nd Page User Name : @(CascadingContainer?.UserName)
<hr />

@code {
    [CascadingParameter]
    public CascadingContainer? CascadingContainer { get; set; }
}

```

- 畫面結果
  ![image](https://user-images.githubusercontent.com/37999690/199503406-71f14ea8-3d39-4bf5-9605-24c1268d4092.png)

  ![image](https://user-images.githubusercontent.com/37999690/199503493-c551f651-09f3-43b9-aad7-11a1620fc032.png)

## State Container

- 跨多個元件間的蟲洞傳輸資料

```csharp
...
// 需要是 AddScoped 或 AddSingleton
builder.Services.AddScoped<UserStage>();
...

```

> UserStage.cs

```csharp
public class UserStage
{
    public string UserName { get; private set; } = "Ari";

    public event Action? OnChange;

    public void AddSomethingToUserName(string something)
    {
        UserName += something;

        NotifyStateChanged();
    }

    private void NotifyStateChanged()
    {
        OnChange?.Invoke();
    }

}

```

> index.razor

```razor
@page "/"

@inject UserStage UserStage
@implements IDisposable

<PageTitle>Index</PageTitle>

<h3>Index</h3>

Index User Name : @(UserStage.UserName)
<hr />

<UserStageComponent></UserStageComponent>

@code {
    protected override void OnInitialized()
    {
        UserStage.OnChange += StateHasChanged;
    }

    public void Dispose()
    {
        UserStage.OnChange -= StateHasChanged;
    }
}
```

> UserStageComponent.razor

```razor
@inject UserStage UserStage
<h3>UserStageComponent</h3>

UserStageComponent User Name : @(UserStage.UserName)

<div>
    <button @onclick="@btnClick">Component Btn</button>
</div>


@code{
    private void btnClick(){
        UserStage.AddSomethingToUserName(DateTime.UtcNow.Ticks.ToString());
    }
}

```
