using Game.GameCore.Battles.System;
using Game.GameCore.BattleSystem.Enums;
using Game.Manager.BattleSystem;

namespace Game.GameCore.Battles.Manager;

public struct BattleConfiguration
{
    public int RandomSeed { get; }
    public EDayTime DayTime { get; }
    public ETempetura Tempetura { get; }
    public ETerrain Terrain { get; }
    public EWeather Weather { get; }

    public List<Team> Teams { get; }
}
