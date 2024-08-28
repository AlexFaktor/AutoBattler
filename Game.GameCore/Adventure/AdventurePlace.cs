namespace App.GameCore.Adventure;

// Місце або кімата, по яким гравець передвигається
public class AdventurePlace
{
    public string ImageUrl { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public AdventureMap Map { get; set; }
    // Active prop
    private List<AdventureChoice> _choices;

    public AdventurePlace(AdventurePlaceConfiguration configuration, AdventureMap map)
    {
        _choices = GetChoices();
        Map = map;

        List<AdventureChoice> GetChoices()
        {
            return new List<AdventureChoice>();
        }
    }
}

public class AdventurePlaceConfiguration
{

}
