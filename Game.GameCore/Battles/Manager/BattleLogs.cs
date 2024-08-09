namespace App.GameCore.Battles.Manager;

public class BattleLogs
{
    public string Text { get; set; } = string.Empty;
    public string ActualDuration { get; set; } = string.Empty;
    public string ExceptionText { get; set; } = string.Empty;
    public bool IsEnded { get; set; } = false;
}
