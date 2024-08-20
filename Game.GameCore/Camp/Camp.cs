using App.GameCore.Battles.System;

namespace App.GameCore.Camp;

public class Camp
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Player Player { get; set; }
}
