using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : Obstacle
{
    private bool isBurning = false;
    [SerializeField]
    private int burnTime;

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
            burnTime--;
            var neighbors = tileGrid.GetNeighborsFor(tileOn.tileCoords);
            foreach (var neighbor in neighbors)
            {
                Tile tile = tileGrid.GetTileAt(neighbor);
                tile.ApplyHeat();
            }

            tileOn.obstacle = null;
            if(burnTime < 0)
                Destroy(gameObject);
        }
    }
}
