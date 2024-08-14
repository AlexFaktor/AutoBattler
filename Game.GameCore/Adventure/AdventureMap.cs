namespace App.GameCore.Adventure;

// Впливає на генерацію Place
public class AdventureMap
{
    // Main
    public AdventureMapConfiguration Configuration { get; set; }
    public AdventureResult Result { get; set; }

    // General stats
    public int Distance { get; set; }
    public AdventureLocation Location { get; set; }

    // Links 
    public AdventurePlayer Player { get; set; }

    public AdventureMap() { }

}

// Поточна локація
public class AdventureLocation
{

}

// Місце або кімата, по яким гравець передвигається
public class AdventurePlace
{
    public string ImageUrl { get; set; }
    public string Name { get; set; }
}

public class AdventurePlaceConfiguration
{

}

// Дія, вибір який може зробити гравець та отримати фідбек
public class AdventureChoice
{
    public string Sign { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    // Links
    public AdventurePlace PlaceNow { get; set; }
}

public class AdventureMapConfiguration
{
    
}

public class AdventureResult
{

}

public class AdventurePlayer
{

}

public class AdventureLogger
{

}

public enum AdventureTypes
{ 
    OpenWorld,
    Quest,
    Event,
    Raid,
    Campania
}

public enum AdventureLocations
{
    None = 0,
}
