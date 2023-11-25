using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBlock : Obstacle
{
    public override void ApplyHeat()
    {
        if (tileOn.groundtype == Groundtype.Normal)
            tileOn.groundtype = Groundtype.Soaked;
        tileOn.obstacle = null;
        Destroy(this);
    }

    public override void ApplyCold() {}

    public override void ApplyPush(Vector3Int pushingDir)
    {
        movementSystem.TryMoveAnObstacle(this, tileOn.tileCoords + pushingDir);
    }

    public override bool IsReachable()
    {
        return false;
    }

    public override void Tick() {}
}
