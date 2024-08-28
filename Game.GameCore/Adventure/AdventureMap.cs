using App.GameCore.Adventure.Enums;

namespace App.GameCore.Adventure;

// Впливає на генерацію Place
public class AdventureMap
{
    // Main
    public AdventureMapConfiguration Configuration { get; set; }
    public AdventureMapResult Result { get; set; }

    // General stats
    public int Distance { get; set; }
    public AdventureLocation Location { get; set; }

    // Links 
    public AdventurePlayer Player { get; set; }

    public AdventureMap() { }

}

public class AdventureMapConfiguration
{

}

public class AdventureMapResult
{

}
