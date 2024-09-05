namespace GameLogic.Battles.Enums;

public enum BattleTypes
{
    Unknown = 0,
    Pvp,
    Pve,
    Custom,
    Test
}

public class BattleTypesParser
{
    public static string Parse(BattleTypes type)
    {
        return type switch
        {
            BattleTypes.Unknown => "Unknown",
            BattleTypes.Pvp => "Pvp",
            BattleTypes.Pve => "Pve",
            BattleTypes.Custom => "Custom",
            BattleTypes.Test => "Test",
            _ => "Unknown",
        };
    }

    public static BattleTypes Parse(string type)
    {
        return type switch
        {
            "Unknown" => BattleTypes.Unknown,
            "Pvp" => BattleTypes.Pvp,
            "Pve" => BattleTypes.Pve,
            "Test" => BattleTypes.Test,
            "Custom" => BattleTypes.Custom,
            _ => BattleTypes.Unknown,
        };
    }
}
