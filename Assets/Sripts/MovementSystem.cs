using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementSystem : MonoBehaviour
{
    private BFSResult movementRange = new BFSResult();
    private List<Vector3Int> currentPath = new List<Vector3Int>();
    private List<Vector3> worldPath = new List<Vector3>();
    public int pathCost;
    [SerializeField]
    private GameManager gManager;
    [SerializeField]
    public TileGrid grid;
    [SerializeField]
    public Unit player;

   public void HideRange()
    {
        foreach (Vector3Int tilePosition in movementRange.GetRangePositions())
        {
            grid.GetTileAt(tilePosition).DisableHighlights();
        }
        movementRange = new BFSResult();
    }

    public void ShowRange(Unit unit)
    {
        CalculateRange(unit);
        foreach (Vector3Int tilePosition in movementRange.GetRangePositions())
        {   
            Tile tile = grid.GetTileAt(tilePosition);
            if (unit.tileOn != tile)
            {
                 tile.EnableHighlight1();
            }
        }
    }

    public void CalculateRange(Unit unit)
    {
        movementRange = GraphSearch.BFSGetRange(grid, unit.tileOn.tileCoords, unit.mp);
    }

    public void ShowPath(Vector3Int selectedTilePos)
    {
        if (movementRange.GetRangePositions().Contains(selectedTilePos))
        {
            foreach (Vector3Int tilePos in currentPath)
            {
                grid.GetTileAt(tilePos).EnableHighlight1();
            }
            currentPath = movementRange.GetPathTo(selectedTilePos);
            foreach (Vector3Int tilePos in currentPath)
            {
                grid.GetTileAt(tilePos).EnableHighlight2();
            }
        }
    }

    public void MoveUnit(Player unit)
    {
        unit.tileOn.unit = null;
        Vector3Int endOfPath = currentPath[currentPath.Count -1];
        grid.GetTileAt(endOfPath).unit = unit;
        unit.tileOn = grid.GetTileAt(endOfPath);
        unit.tileCoord= endOfPath;
        ConvertPath(currentPath);
        unit.MoveThroughPath(worldPath);
    }


    public bool IsTileInRange(Vector3Int tilePos)
    {
        return movementRange.IsTilePositionInRange(tilePos);
    }

    private void ConvertPath (List<Vector3Int> tilePath)
    {
        worldPath = new List<Vector3>();
        foreach (Vector3Int tileOn in tilePath)
        {
            worldPath.Add(grid.GetTileAt(tileOn).transform.position);
        }
    }

    private void TryMoveAnObstacle(Obstacle obstacle, Vector3Int destTilePos)
    {
        if (grid.GetTileAt(destTilePos) != null)
        {
            Tile destTile = grid.GetTileAt(destTilePos);
            if (destTile.IsReachable())
            {
                obstacle.tileOn.obstacle = null;
                obstacle.tileOn = destTile;
                destTile.obstacle = obstacle;
                StartCoroutine(obstacle.MovementCoroutine(destTile.transform.position));
            }
        }
    }
}
