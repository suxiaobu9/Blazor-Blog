namespace Blazor.Blog.State;

public class KeywordSearchState1
{
    public string? Keyword { get; private set; }

    public event Action? OnChange;

    public void SetKeyword(string? keyword)
    {
        Keyword = keyword;
        NotifyStateChanged();
    }

    private void NotifyStateChanged() => OnChange?.Invoke();

    public void CleanAction() => OnChange = null;

}
