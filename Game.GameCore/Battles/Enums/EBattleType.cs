namespace App.GameCore.Battles.Enums;

public enum EBattleType
{
    Unknown = 0,
    Pvp,
    Pve,
    Custom,
    Test
}

public class EBattleTypeParser
{
    public static string Parse(EBattleType type)
    {
        return type switch
        {
            EBattleType.Unknown => "Unknown",
            EBattleType.Pvp => "Pvp",
            EBattleType.Pve => "Pve",
            EBattleType.Custom => "Custom",
            EBattleType.Test => "Test",
            _ => "Unknown",
        };
    }

    public static EBattleType Parse(string type)
    {
        return type switch
        {
            "Unknown" => EBattleType.Unknown,
            "Pvp" => EBattleType.Pvp,
            "Pve" => EBattleType.Pve,
            "Test" => EBattleType.Test,
            "Custom" => EBattleType.Custom,
            _ => EBattleType.Unknown,
        };
    }
}
