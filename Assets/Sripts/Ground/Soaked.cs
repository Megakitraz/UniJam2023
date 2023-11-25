using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soaked : Ground
{
    public override void ApplyHeat() {}

    public override void ApplyCold()
    {
        // TODO soaked -> Ice
        throw new System.NotImplementedException();
    }
    
    public override bool IsReachable()
    {
        return true;
    }

    public override bool IsSlippery()
    {
        return true;
    }
}
