using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GraphSearch
{
    public static BFSResult BFSGetRange(TileGrid grid, Vector3Int startPoint, int movementPoints)
     {
        Dictionary<Vector3Int, Vector3Int?> visitedNodes = new Dictionary<Vector3Int, Vector3Int?>();
        Dictionary<Vector3Int, int> costSoFar = new Dictionary<Vector3Int, int>();
        Queue<Vector3Int> nodesToVisit = new Queue<Vector3Int>();
       

        nodesToVisit.Enqueue(startPoint);
        costSoFar.Add(startPoint, 0);
        visitedNodes.Add(startPoint, null);

        while(nodesToVisit.Count > 0)
        {
            Vector3Int currentNode = nodesToVisit.Dequeue();
            foreach (Vector3Int neighborPosition in grid.GetNeighborsFor(currentNode))
            {
                if (grid.GetTileAt(neighborPosition).IsReachable())
                {
                    int nodeCost = grid.GetTileAt(neighborPosition).GetCost();
                    int currentCost = costSoFar[currentNode];
                    int newCost = currentCost + nodeCost;

                    if(newCost <= movementPoints)
                    {
                        if (!visitedNodes.ContainsKey(neighborPosition))
                        {
                            visitedNodes[neighborPosition] = currentNode;
                            costSoFar[neighborPosition] = newCost;
                            nodesToVisit.Enqueue(neighborPosition);
                        }
                        else if(costSoFar[neighborPosition] > newCost)
                        {
                            costSoFar[neighborPosition] = newCost;
                            visitedNodes[neighborPosition] = currentNode;
                        }
                    }
                }
            }

        
        }
        return new BFSResult {visitedNodesDict = visitedNodes, costSoFarDict = costSoFar};
     }


    public static List<Vector3Int> GeneratePathBFS(Vector3Int current, Dictionary<Vector3Int, Vector3Int?> visitedNodesDict)
    {
        List<Vector3Int> path = new List<Vector3Int>();
        path.Add(current);
        while (visitedNodesDict[current] != null)
        {
            path.Add(visitedNodesDict[current].Value);
            current = visitedNodesDict[current].Value;
        }
        path.Reverse();
        return path.Skip(1).ToList();
    }

}

public struct BFSResult
{
    public Dictionary<Vector3Int, Vector3Int?> visitedNodesDict;
    public Dictionary<Vector3Int, int> costSoFarDict;
    private GameManager gManager;
    public List<Vector3Int> GetPathTo(Vector3Int destination)
    {
        gManager = GameManager.Instance;
        if (!visitedNodesDict.ContainsKey(destination))
        {
            return new List<Vector3Int>();
        }
        gManager.movementSystem.pathCost = costSoFarDict[destination];
        return GraphSearch.GeneratePathBFS(destination, visitedNodesDict);
    }

    public bool IsTilePositionInRange(Vector3Int position)
    {
        return visitedNodesDict.ContainsKey(position);
    }

    public List<Vector3Int> GetRangePositions()
    {
        return new List<Vector3Int>(visitedNodesDict.Keys);
    }

}