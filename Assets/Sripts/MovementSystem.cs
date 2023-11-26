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
            Debug.Log(tilePosition);
            if (unit.tileOn != tile)
            {
                if(tile.IsMovableOn(tile.tileCoords - unit.tileOn.tileCoords))
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

    public IEnumerator MoveUnit(Player unit)
    {
        bool checkSlippery = true;
        Vector3Int baseCoord = unit.tileOn.tileCoords;
        Vector3Int endOfPath = currentPath[currentPath.Count - 1];
        Vector3Int dir = endOfPath - unit.tileOn.tileCoords;
        Tile target = grid.GetTileAt(endOfPath);
        float t = 1.0f/unit.movSpeed;
        if (target.IsMovableOn(target.tileCoords - unit.tileOn.tileCoords))
        {
            baseCoord = unit.tileOn.tileCoords;
            unit.tileOn.unit = null;
            unit.tileOn = target;
            unit.tileOn.unit = unit;
            StartCoroutine(unit.RotationCoroutine(unit.tileOn.transform.position));
            if (target.obstacle != null)
            {
                TryMoveAnObstacle(target.obstacle, target.tileCoords + target.tileCoords - baseCoord);
                checkSlippery = false;

            }
            yield return new WaitForSeconds(t);
            grid.Tick();
        }

        while (unit.tileOn.IsSlippery() && checkSlippery)
        { 
            target = grid.GetTileAt(unit.tileOn.tileCoords + dir);
            if (target == null) break;
            unit.tileOn.unit = null;
            unit.tileOn = target;
            unit.tileOn.unit = unit;
            StartCoroutine(unit.MovementCoroutine(unit.tileOn.transform.position));

            
        } 
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

    public IEnumerator MoveEntity(Unit unit, Vector3Int destTilePos)
    {
        float t = 1.0f/unit.movSpeed;

        Vector3Int dir = destTilePos - unit.tileOn.tileCoords;
        if (grid.GetTileAt(destTilePos) != null)
        {
            unit.PlayStopBullSounds(true);

            Tile destTile = grid.GetTileAt(destTilePos);
            unit.tileOn.unit = null;
            unit.tileOn = destTile;
            destTile.unit = unit;
            StartCoroutine(unit.MovementCoroutine(destTile.transform.position));
            yield return new WaitForSeconds(2*t);
            unit.ApplyEffectOnNeighbor();
            if (unit.GetComponent<FireBull>() != null)
            {
                var target = grid.GetTileAt(unit.tileOn.tileCoords + dir);
                if (target == null)
                {
                    unit.PlayStopBullSounds(false);
                    yield return null;
                }
                while (target.IsReachable())
                {   
                    unit.tileOn.unit = null;
                    unit.tileOn = target;
                    target.unit = unit;
                    StartCoroutine(unit.MovementCoroutine(target.transform.position));
                    yield return new WaitForSeconds(2*t);
                    unit.ApplyEffectOnNeighbor();
                    target = grid.GetTileAt(unit.tileOn.tileCoords + dir);
                    if (target == null) break;
                }

                unit.PlayStopBullSounds(false);
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
                Debug.Log("yyyyyy");
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
