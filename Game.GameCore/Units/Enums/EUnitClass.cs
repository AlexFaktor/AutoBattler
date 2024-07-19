namespace Game.GameCore.Units.Enums;

public enum EUnitClass
{
    None = 0,
    Assassin,
    Buffer,
    Controller,
    Healer,
    Mage,
    Marksman,
    Support,
    Tank,
    Warrior,
    Universal,
}

public static class EUnitClassParse
{
    public static EUnitClass Parse(string data)
    {
        return data switch
        {
            "Assassin" => EUnitClass.Assassin,
            "Buffer" => EUnitClass.Buffer,
            "Controller" => EUnitClass.Controller,
            "Healer" => EUnitClass.Healer,
            "Mage" => EUnitClass.Mage,
            "Marksman" => EUnitClass.Marksman,
            "Support" => EUnitClass.Support,
            "Tank" => EUnitClass.Tank,
            "Warrior" => EUnitClass.Warrior,
            "Universal" => EUnitClass.Universal,
            _ => EUnitClass.None,
        };
    }

    public static string Parse(EUnitClass data)
    {
        return data switch
        {
            EUnitClass.Assassin => "Assassin",
            EUnitClass.Buffer => "Buffer",
            EUnitClass.Controller => "Controller",
            EUnitClass.Healer => "Healer",
            EUnitClass.Mage => "Mage",
            EUnitClass.Marksman => "Marksman",
            EUnitClass.Support => "Support",
            EUnitClass.Tank => "Tank",
            EUnitClass.Warrior => "Warrior",
            EUnitClass.Universal => "Universal",
            _ => "None",
        };
    }
}