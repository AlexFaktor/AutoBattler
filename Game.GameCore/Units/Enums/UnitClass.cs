namespace GameLogic.Units.Enums;

public enum UnitClass
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
    public static UnitClass Parse(string data)
    {
        return data switch
        {
            "Assassin" => UnitClass.Assassin,
            "Buffer" => UnitClass.Buffer,
            "Controller" => UnitClass.Controller,
            "Healer" => UnitClass.Healer,
            "Mage" => UnitClass.Mage,
            "Marksman" => UnitClass.Marksman,
            "Support" => UnitClass.Support,
            "Tank" => UnitClass.Tank,
            "Warrior" => UnitClass.Warrior,
            "Universal" => UnitClass.Universal,
            _ => UnitClass.None,
        };
    }

    public static string Parse(UnitClass data)
    {
        return data switch
        {
            UnitClass.Assassin => "Assassin",
            UnitClass.Buffer => "Buffer",
            UnitClass.Controller => "Controller",
            UnitClass.Healer => "Healer",
            UnitClass.Mage => "Mage",
            UnitClass.Marksman => "Marksman",
            UnitClass.Support => "Support",
            UnitClass.Tank => "Tank",
            UnitClass.Warrior => "Warrior",
            UnitClass.Universal => "Universal",
            _ => "None",
        };
    }
}