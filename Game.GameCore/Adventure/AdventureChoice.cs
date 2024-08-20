namespace App.GameCore.Adventure;

// Дія, вибір який може зробити гравець та отримати фідбек
public class AdventureChoice
{
    public string Sign { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    // Links
    public AdventureMap MapNow { get; set; }
    public AdventurePlace PlaceNow { get; set; }
}
