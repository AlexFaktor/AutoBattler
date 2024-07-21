﻿namespace App.GameCore.Battles.Manager;

public class BattleLogs
{
    public string Text { get; set; } = string.Empty;
    public string ActualDuration { get; set; } = string.Empty;
    public Exception? Exception { get; set; }
    public bool IsEnded { get; set; } = false;
}