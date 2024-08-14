using App.GameCore.Battles.Enums;
using App.GameCore.Battles.System;
using App.GameCore.BattleSystem.Enums;
using App.Manager.BattleSystem;
using Newtonsoft.Json;

namespace App.GameCore.Battles.Dto;

public class StartBattleDto
{
    public int RandomSeed { get; set; }
    public BattleTypes BattleType { get; set; }
    public DayTimes DayTime { get; set; }
    public Tempeturas Tempetura { get; set; }
    public Terrains Terrain { get; set; }
    public Weathers Weather { get; set; }
    public Dictionary<IPlayer, TeamConfiguration> Team { get; set; }

    public StartBattleDto() { }

    public StartBattleDto(string json)
    {
        var dtoWithStringKey = JsonConvert.DeserializeObject<StartBattleDtoWithStringKey>(json);

        RandomSeed = dtoWithStringKey.RandomSeed;
        BattleType = dtoWithStringKey.BattleType;
        DayTime = dtoWithStringKey.DayTime;
        Tempetura = dtoWithStringKey.Tempetura;
        Terrain = dtoWithStringKey.Terrain;
        Weather = dtoWithStringKey.Weather;
        Team = dtoWithStringKey.Team.ToDictionary(kvp => (IPlayer)new Player { Id = Guid.Parse(kvp.Key) }, kvp => kvp.Value);
    }

    public string GetJSON()
    {
        var dtoWithStringKey = new StartBattleDtoWithStringKey
        {
            RandomSeed = this.RandomSeed,
            BattleType = this.BattleType,
            DayTime = this.DayTime,
            Tempetura = this.Tempetura,
            Terrain = this.Terrain,
            Weather = this.Weather,
            Team = this.Team.ToDictionary(kvp => kvp.Key.Id.ToString(), kvp => kvp.Value)
        };

        return JsonConvert.SerializeObject(dtoWithStringKey, Formatting.Indented);
    }
}

public class StartBattleDtoWithStringKey
{
    public int RandomSeed { get; set; }
    public BattleTypes BattleType { get; set; }
    public DayTimes DayTime { get; set; }
    public Tempeturas Tempetura { get; set; }
    public Terrains Terrain { get; set; }
    public Weathers Weather { get; set; }
    public Dictionary<string, TeamConfiguration> Team { get; set; }
}
