using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBull : Unit
{
    
    private bool isEnraged = false;
    [SerializeField] private Direction direction;
    private GameObject exclamationMark;
    
    // Start is called before the first frame update
    void Start()
    {
        HideIndicator();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ShowIndicator()
    {
        //exclamationMark.SetActive(true);
    }

    void HideIndicator()
    {
        //exclamationMark.SetActive(false);
    }

    public override void Tick()
    {
        if (isEnraged)
        {
            Vector3Int coords = tileOn.tileCoordinates.GetCoords();

            switch (direction)
            {
                case Direction.up:
                    coords += new Vector3Int(0, 0, 1);
                    break;

                case Direction.right:
                    coords += new Vector3Int(1, 0, 0);
                    break;

                case Direction.down:
                    coords += new Vector3Int(0, 0, -1);
                    break;

                case Direction.left:
                    coords += new Vector3Int(-1, 0, 0);
                    break;
            }

            movementSystem.MoveEntity(this,coords);
            return;
        }
        ApplyEffectOnNeighbor();
        CheckPlayerVisibility();
    }

    public override void ApplyEffectOnNeighbor()
    {
        var neighbors= tileGrid.GetNeighborsFor(tileCoord);
        foreach (var neighbor in neighbors)
        {
            Tile tile = tileGrid.GetTileAt(neighbor);
            if(tile != null)
                tile.ApplyHeat();
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
