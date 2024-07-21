namespace App.GameCore.Units.Enums;

public enum ETacticalType
{
    Default = 0,
    Attacker,
    Defender,
    Universal,
}

public static class ETacticalTypeParse
{
    public static ETacticalType Parse(string data)
    {
        return data switch
        {
            "Attacker" => ETacticalType.Attacker,
            "Defender" => ETacticalType.Defender,
            "Universal" => ETacticalType.Universal,
            _ => ETacticalType.Defender,
        };
    }

    public static string Parse(ETacticalType data)
    {
        return data switch
        {
            ETacticalType.Attacker => "Attacker",
            ETacticalType.Defender => "Defender",
            ETacticalType.Universal => "Universal",
            _ => "Defender",
        };
    }
}
