﻿namespace Game.GameCore.Units.Types;

public class TUnitPercentage
{
    public float Default { get; }
    public float Max { get; set; }
    public float Now { get; set; }

    public TUnitPercentage(float value)
    {
        Default = value;
        Max = value;
        Now = value;
    }

}


