using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrostedLake : Ground
{
    public override void ApplyHeat()
    {
        // Todo melt lake
        throw new System.NotImplementedException();
    }

    public override void ApplyCold() {}

    public override bool IsReachable()
    {
        return true;
    }

    public override bool IsSlippery()
    {
        return true;
    }
}
