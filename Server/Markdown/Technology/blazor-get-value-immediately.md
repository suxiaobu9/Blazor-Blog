# Blazor 立刻取得值的方法

## 一般的 bind data 方法

在離開 input 框框時值才會有變化

```csharp
@(nameof(CurrentValue)) : @CurrentValue

<br />

<input type="text" @bind="@CurrentValue" />


@code {
    private string? CurrentValue { get; set; }
}
```

![image](https://user-images.githubusercontent.com/37999690/193398149-6586edbb-a0ac-4a92-b7f8-d7e75e95ba2d.png)

## 利用 oninput 取得輸入的值

輸入任何東西值都會即時變化

```csharp
@(nameof(CurrentValue)) : @CurrentValue

<br />

<input type="text" @oninput="@valueOnInput" />


@code {
    private string? CurrentValue { get; set; }

    private void valueOnInput(ChangeEventArgs e)
    {
        CurrentValue = e.Value?.ToString();
    }
}
```

或

```csharp
@(nameof(CurrentValue)) : @CurrentValue

<br />

<input type="text" @oninput="@((e)=>{CurrentValue = e.Value?.ToString();})" />


@code {
    private string? CurrentValue { get; set; }
}
```

![image](https://user-images.githubusercontent.com/37999690/193398641-acb896fb-7995-4716-aa7f-d3e7e58a66ad.png)
