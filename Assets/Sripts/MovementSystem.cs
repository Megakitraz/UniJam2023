using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

            Debug.Log(tile.tileCoords + "is Reachable");
            if (unit.tileOn != tile)
            {
                if(tile.IsReachable())
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
        Vector3Int endOfPath = currentPath[currentPath.Count - 1];
        Vector3Int dir = endOfPath - unit.tileOn.tileCoords;
        grid.GetTileAt(endOfPath).unit = unit;
        unit.tileOn = grid.GetTileAt(endOfPath);
        StartCoroutine(unit.MovementCoroutine(unit.tileOn.transform.position));
        grid.Tick();
        while (unit.tileOn.IsSlippery())
        { 
            Tile target = grid.GetTileAt(unit.tileOn.tileCoords + dir);
            if (target == null) break;
            if (target.IsReachable())
            {
                unit.tileOn.unit = null;
                unit.tileOn = target;
                unit.tileOn.unit = unit;
                StartCoroutine(unit.MovementCoroutine(unit.tileOn.transform.position));
                grid.Tick();
            }
        }
        GameManager.Instance.StartTurn();
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

    public void MoveEntity(Unit unit, Vector3Int destTilePos)
    {
        Vector3Int dir = destTilePos - unit.tileOn.tileCoords;
        if (grid.GetTileAt(destTilePos) != null)
        {
            Tile destTile = grid.GetTileAt(destTilePos);
            unit.tileOn.unit = null;
            unit.tileOn = destTile;
            destTile.unit = unit;
            StartCoroutine(unit.MovementCoroutine(destTile.transform.position));
            if (unit.GetComponent<FireBull>() != null)
            {
                var target = grid.GetTileAt(unit.tileOn.tileCoords + dir);
                if (target == null) return;
                while (target.IsReachable())
                {   
                    unit.tileOn.unit = null;
                    unit.tileOn = target;
                    target.unit = unit;
                    StartCoroutine(unit.MovementCoroutine(target.transform.position));
                    target = grid.GetTileAt(unit.tileOn.tileCoords + dir);
                    if (target == null) return;
                }
            }
        }
    }
    
    public void TryMoveAnObstacle(Obstacle obstacle, Vector3Int destTilePos)
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

    public bool IsObstaclePushable(Obstacle obstacle, Vector3Int dir)
    {
        if (grid.GetTileAt(obstacle.tileOn.tileCoords + dir) != null && grid.GetTileAt(obstacle.tileOn.tileCoords + dir).IsPushableOn())
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
