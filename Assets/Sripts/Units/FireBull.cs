using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBull : Unit
{
    
    private bool isEnraged = false;
    [SerializeField] private Direction direction;
    
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
            //TODO tile.ApplyHeat();
        }
    }

    public void CheckPlayerVisibility()
    {
        Debug.Log(IsPlayerVisible());
    }

    private bool IsPlayerVisible()
    {
        Vector3Int coords = tileOn.tileCoordinates.GetCoords();
        bool solved = false;
        while(!solved)
        {
            switch(direction)
            {
            case Direction.up:
                coords += new Vector3Int(0,0,1);
                break;

            case Direction.right:
                coords += new Vector3Int(1,0,0);
                break;

            case Direction.down:
                coords += new Vector3Int(0,0,-1);
                break;

            case Direction.left:
                coords += new Vector3Int(-1,0,0);
                break;

            default:
                Debug.Log(":(");
            break;
            }
            Debug.Log(coords);
            Tile tile = TileGrid.Instance.GetTileAt(coords);
            if (tile != null)
            {
                if (tile.IsPlayerOnTile())
                {
                    return true;
                }
                if (!tile.IsReachable())
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        return false;
    }


    public enum Direction
    {
        up,
        right,
        down,
        left
    }
    
    
}
