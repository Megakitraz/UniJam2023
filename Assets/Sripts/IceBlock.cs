using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBlock : Obstacle
{
    public override void ApplyHeat()
    {
        // TODO melt ice block
    }

    public override void ApplyCold() {}

    public override void ApplyPush(Vector3Int pushingDir)
    {
        // TODO apply push
    }

    public override bool IsReachable()
    {
        return false;
    }

    public override void Tick() {}
}
