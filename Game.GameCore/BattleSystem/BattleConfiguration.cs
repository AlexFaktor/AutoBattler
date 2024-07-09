using Game.GameCore.BattleSystem;
using Game.GameCore.BattleSystem.Enums;

namespace Game.Manager.BattleSystem;

public struct BattleConfiguration
{
    public int RandomSeed { get; }
    public EDayTime DayTime { get; }
    public ETempetura Tempetura { get; }
    public ETerrain Terrain { get; }
    public EWeather Weather { get; }

    public List<Team> Teams { get; }
}
