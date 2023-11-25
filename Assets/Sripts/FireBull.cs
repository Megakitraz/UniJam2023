using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBull : Unit
{
    
    private bool isEnraged = false;
    private Vector3Int facingDir;
    
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
        if (isEnraged)
        {
            // TODO move fireBull
            return;
        }
        CheckPlayerVisibility();
    }

    protected override void ApplyEffectOnNeighbor()
    {
        var neighbors= tileGrid.GetNeighborsFor(tileCoord);
        foreach (var neighbor in neighbors)
        {
            Tile tile = tileGrid.GetTileAt(neighbor);
            tile.ApplyHeat();
        }
    }

    private void CheckPlayerVisibility()
    {
        throw new System.NotImplementedException();
        // TODO implement test to see if player is visible by the bull, and set facing dir accordingly
    }
    
    
}
