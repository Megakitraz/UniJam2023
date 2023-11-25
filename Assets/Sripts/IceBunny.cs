using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;

public class IceBunny : Unit
{
    private bool isEffrayed = false;
    private Vector3Int fleeingDir;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Tick()
    {
        if (isEffrayed)
        {
            // TODO move the ice bunny
            return;
        }
        CheckPlayerVisibility();
    }

    private void CheckPlayerVisibility()
    {
        throw new System.NotImplementedException();
    }

    public override void ApplyEffectOnNeighbor()
    {
        var neighbors = tileGrid.GetNeighborsFor(tileCoord);
        foreach (var neighbor in neighbors)
        {
            Tile tile = tileGrid.GetTileAt(neighbor);
            tile.ApplyCold();
        }
    }
}
