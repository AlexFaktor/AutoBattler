namespace GameLogic.Units.Enums;

public enum TacticalTypes
{
    Default = 0,
    Attacker,
    Defender,
    Universal,
}

public static class ETacticalTypeParse
{
    public static TacticalTypes Parse(string data)
    {
        return data switch
        {
            "Attacker" => TacticalTypes.Attacker,
            "Defender" => TacticalTypes.Defender,
            "Universal" => TacticalTypes.Universal,
            _ => TacticalTypes.Defender,
        };
    }

    public static string Parse(TacticalTypes data)
    {
        return data switch
        {
            TacticalTypes.Attacker => "Attacker",
            TacticalTypes.Defender => "Defender",
            TacticalTypes.Universal => "Universal",
            _ => "Defender",
        };
    }
}
