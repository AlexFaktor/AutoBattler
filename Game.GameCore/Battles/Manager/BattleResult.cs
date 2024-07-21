﻿namespace App.GameCore.Battles.Manager;

public class BattleResult
{
    public BattleConfiguration Configuration { get; }
    public Guid Id { get; } = Guid.NewGuid();

    public DateTime StartTime { get; set; } = DateTime.UtcNow;
    public DateTime EndTime { get; private set; }

    public BattleStats Stats { get; set; } = new ();
    public BattleLogs Logs { get; set; } = new();

    public BattleResult(BattleConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void EndBattle()
    {
        var logs = Logs;
        logs.IsEnded = true;
        Logs = logs;

        EndTime = DateTime.UtcNow;
    }
}