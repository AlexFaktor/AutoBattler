using Game.GameCore.Battles.System;
using Game.GameCore.BattleSystem.Enums;
using Game.Manager.BattleSystem;
using Newtonsoft.Json.Converters;
using System.Text.Json.Serialization;

namespace Game.GameCore.Battles.Manager;

public class BattleConfiguration
{
    public BattleConfiguration(int randomSeed, EDayTime dayTime, ETempetura tempetura, ETerrain terrain, EWeather weather, List<Team> teams)
    {
        RandomSeed = randomSeed;
        DayTime = dayTime;
        Tempetura = tempetura;
        Terrain = terrain;
        Weather = weather;
        Teams = teams;
    }

    public int RandomSeed { get; set; }

    [JsonConverter(typeof(StringEnumConverter))]
    public EDayTime DayTime { get; set; }
    [JsonConverter(typeof(StringEnumConverter))]
    public ETempetura Tempetura { get; set; }
    [JsonConverter(typeof(StringEnumConverter))]
    public ETerrain Terrain { get; set; }
    [JsonConverter(typeof(StringEnumConverter))]
    public EWeather Weather { get; set; }

    public List<Team> Teams { get; set; }
}
