using App.GameCore.Battles.Enums;
using App.GameCore.Battles.System;
using App.GameCore.BattleSystem.Enums;
using App.Manager.BattleSystem;
using Newtonsoft.Json.Converters;
using System.Text.Json.Serialization;

namespace App.GameCore.Battles.Manager;

public class BattleConfiguration
{
    public int Seed { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public BattleTypes BattleType { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public DayTimes DayTime { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public Tempeturas Tempetura { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public Terrains Terrain { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public Weathers Weather { get; set; }

    public List<TeamConfiguration> TeamConfigurations { get; set; }

    public BattleConfiguration(
        int randomSeed,
        BattleTypes battleType,
        DayTimes dayTime,
        Tempeturas tempetura,
        Terrains terrain,
        Weathers weather,
        List<TeamConfiguration> teams)
    {
        Seed = randomSeed;
        BattleType = battleType;

        DayTime = dayTime;
        Tempetura = tempetura;
        Terrain = terrain;
        Weather = weather;

        TeamConfigurations = teams;
    }

    public BattleConfiguration()
    {

    }
}
