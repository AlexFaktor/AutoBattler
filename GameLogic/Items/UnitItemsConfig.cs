namespace GameLogic.Items;

/// <summary>
/// Number of items that can be applied to a unit 
/// </summary>
public class UnitItemsConfig
{
    public int Id { get; set; } = 0;
    public Dictionary<EquipmnetTypes, int> ItemsConfig { get; set; } = new();
}

public enum EquipmnetTypes
{
    None,

    Weapon,
    Head,
    Chess,
    Legs
}