namespace Blazor.Blog.Stage;

public class KeywordSearchStage
{
    public event Action? KeyBoardEscPressEvent;

    public void NotifyStateChange() => KeyBoardEscPressEvent?.Invoke();
}
