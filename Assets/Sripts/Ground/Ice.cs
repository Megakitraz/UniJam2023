using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ice : Ground
{
    public override void ApplyHeat()
    {
        // TODO melt ice
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
