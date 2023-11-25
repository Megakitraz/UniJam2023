using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Normal : Ground
{
    public override void ApplyHeat() {}

    public override void ApplyCold() {}

    public override bool IsReachable()
    {
        return true;
    }

    public override bool IsSlippery()
    {
        return false;
    }
}
