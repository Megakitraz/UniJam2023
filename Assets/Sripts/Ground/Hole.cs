using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole : Ground
{
    public override void ApplyHeat() {}

    public override void ApplyCold() {}

    public override bool IsReachable()
    {
        return false;
    }

    public override bool IsSlippery()
    {
        return false;
    }
}
