﻿using App.GameCore.Battles.Enums;
using App.GameCore.Battles.System;
using App.GameCore.BattleSystem.Enums;
using App.Manager.BattleSystem;
using Newtonsoft.Json.Converters;
using System.Text.Json.Serialization;

namespace App.GameCore.Battles.Manager;

public class BattleConfiguration
{
    public BattleConfiguration(int randomSeed, EBattleType battleType, EDayTime dayTime, ETempetura tempetura, ETerrain terrain, EWeather weather, List<Team> teams)
    {
        RandomSeed = randomSeed;
        BattleType = battleType;

        DayTime = dayTime;
        Tempetura = tempetura;
        Terrain = terrain;
        Weather = weather;
        Teams = teams;
    }

    public int RandomSeed { get; set; }

    [JsonConverter(typeof(StringEnumConverter))]
    public EBattleType BattleType { get; set; }
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
