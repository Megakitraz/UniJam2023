using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.Math ;

public class TileGrid : MonoBehaviour
{
    private static TileGrid instance = null;
    public static TileGrid Instance => instance;

    Dictionary<Vector3Int, Tile> tileDict = new Dictionary<Vector3Int, Tile>();
    Dictionary<Vector3Int, List<Vector3Int>> tileNeighborsDict = new Dictionary<Vector3Int, List<Vector3Int>>();
    List<List<Vector3Int>> tileRow = new List<List<Vector3Int>>(); 
    List<List<Vector3Int>> tileColumn = new List<List<Vector3Int>>(); 


    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        instance = this;
        DontDestroyOnLoad(this);
    }

    public void InitGrid()
    {
        SetupTile();
        SetupNeighbors();
        SetupRow();
        SetupColumn();
    }
    private void Start()
    {
       
    }

    public Tile GetTileAt(Vector3Int tileCoordinates)
    {
        Tile result = null;
        tileDict.TryGetValue(tileCoordinates, out result);
        return result;
    }

    public List<Vector3Int> GetNeighborsFor (Vector3Int tileCoordinates)
    {
        if (tileDict.ContainsKey(tileCoordinates) == false)
        {
            return new List<Vector3Int>();
        }
        else
        {
            return tileNeighborsDict[tileCoordinates];
        }
    }

    public List<Vector3Int> GetAtRangeFor (Vector3Int tileCoordinates, int range)  //range 0 ca marche pas
    {
        if (tileDict.ContainsKey(tileCoordinates) == false)
        {
            return new List<Vector3Int>();
        }

        List<Vector3Int> result = new List<Vector3Int>();
        List<List<Vector3Int>> tileInRange = new List<List<Vector3Int>>();

        tileInRange.Add(new List<Vector3Int>());
        tileInRange[0].Add(tileCoordinates);

        for (int i = 1; i <= range ; i++)
        {
            tileInRange.Add(new List<Vector3Int>());
            foreach (Vector3Int tileCoord in tileInRange[i - 1])
            {
                List<Vector3Int> neighbors = GetNeighborsFor(tileCoord);
                foreach (Vector3Int neighbor in neighbors)
                {
                    if((result.Contains(neighbor) == false) && GetTileAt(neighbor).IsReachable() )       //ici pour rajouter la contrainte "n'est pas un obstacle"
                    {
                        result.Add(neighbor);
                        tileInRange[i].Add(neighbor);
                    }
                }
            }
        }
        return result;
    }


    public List<Vector3Int> GetOnSameLineFor (Vector3Int tileCoordinates)    //Soucis : les obstacles ne bloquent pas la ligne de vue
    {
        if (tileDict.ContainsKey(tileCoordinates) == false)
        {
            return new List<Vector3Int>();
        }
        List<Vector3Int> result = new List<Vector3Int>();
        foreach( List<Vector3Int> line in tileRow)
        {
            if (line.Contains(tileCoordinates))
            {
                foreach(Vector3Int tileCoords in line)
                {
                    if((result.Contains(tileCoords) == false) && GetTileAt(tileCoords).IsReachable() )       
                    {
                        result.Add(tileCoords);
                    }
                }
            }

        }
        foreach( List<Vector3Int> line in tileColumn)
        {
            if (line.Contains(tileCoordinates))
            {
                foreach(Vector3Int tileCoords in line)
                {
                    if((result.Contains(tileCoords) == false) && GetTileAt(tileCoords).IsReachable() )       
                    {
                        result.Add(tileCoords);
                    }
                }
            }

        }
    result.Remove(tileCoordinates);
    return result;

    }
    public void SetupTile()
    {
        foreach(Tile tile in FindObjectsOfType<Tile>())
        {
            tileDict[tile.tileCoords] = tile;
        }   
    }

    public void SetupNeighbors ()
    {
        foreach(Tile tile in FindObjectsOfType<Tile>())
        {
            if (tileNeighborsDict.ContainsKey(tile.tileCoords) == false)
            {
                tileNeighborsDict.Add(tile.tileCoords, new List<Vector3Int>());
                foreach (var direction in Direction.directionsOffset)
                {
                    if (tileDict.ContainsKey(tile.tileCoords + direction))
                    {
                        tileNeighborsDict[tile.tileCoords].Add(tile.tileCoords + direction);
                    }
                }   
            }
        }
    }

    public void SetupRow ()
    {
        int exist = 0;
        foreach(Tile tile1 in FindObjectsOfType<Tile>())
        {
            exist = 0;
            foreach (List<Vector3Int> line in tileRow)
            {
                if (line.Contains(tile1.tileCoords) == true)
                {
                    exist = 1;
                }
            }
            if (exist == 0)
            {
                tileRow.Add( new List<Vector3Int>());
                foreach(Tile tile2 in FindObjectsOfType<Tile>())
                {
                    if(tile1.tileCoords.z == tile2.tileCoords.z)
                    {
                        tileRow[tileRow.Count - 1].Add(tile2.tileCoords);
                    }
                }
            }
        }
    }

    public void Tick()
    {
        Debug.Log("Tick");
        foreach (var unit in FindObjectsOfType<Unit>())
        {
            unit.Tick();
        }

        foreach (var obstacle in FindObjectsOfType<Obstacle>())
        {
            obstacle.Tick();
        }
    }
    public void SetupColumn ()
    {
        int exist = 0;
        foreach(Tile tile1 in FindObjectsOfType<Tile>())
        {
            exist = 0;
            foreach (List<Vector3Int> line in tileColumn)
            {
                if (line.Contains(tile1.tileCoords) == true)
                {
                    exist = 1;
                }
            }
            if (exist == 0)
            {
                tileColumn.Add( new List<Vector3Int>());
                foreach(Tile tile2 in FindObjectsOfType<Tile>())
                {
                    if(tile1.tileCoords.x == tile2.tileCoords.x)
                    {
                        tileColumn[tileColumn.Count - 1].Add(tile2.tileCoords);
                    }
                }
            }
        }
    } 
}


public static class Direction
{
    public static List<Vector3Int> directionsOffset = new List<Vector3Int>
    {
        new Vector3Int(0,0,1),  //up 
        new Vector3Int(1,0,0),  //right
        new Vector3Int(0,0,-1),  //down 
        new Vector3Int(-1,0,0), //left
    };
}
