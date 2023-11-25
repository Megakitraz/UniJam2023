using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : Obstacle
{
    public override void ApplyHeat() {}

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

