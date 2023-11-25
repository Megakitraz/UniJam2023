using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : Obstacle
{
    private bool isBurning = false;

    public override void ApplyHeat()
    {
        isBurning = true;
    }

    public override void ApplyCold() {}

    public override void ApplyPush(Vector3Int pushingDir) {}

    public override bool IsReachable()
    {
        return false;
    }

    public override void Tick()
    {
        if (isBurning)
        {
            var neighbors = tileGrid.GetNeighborsFor(tileCoord);
            foreach (var neighbor in neighbors)
            {
                Tile tile = tileGrid.GetTileAt(neighbor);
                //tile.ApplyHeat();
            }
            // TODO remove tree
        }
    }
}
