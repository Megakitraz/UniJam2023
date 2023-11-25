using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Obstacle : MonoBehaviour
{
    public Tile tileOn;
    public Vector3Int tileCoord;
    protected TileGrid tileGrid;
    
    private Queue<Vector3> pathPositions = new Queue<Vector3>();
    public float movSpeed;
    public event Action<Obstacle> MovementFinished;


    private void Awake()
    {
        tileGrid = FindObjectOfType<TileGrid>();
    }

    public abstract void ApplyHeat();
    public abstract void ApplyCold();
    public abstract void ApplyPush(Vector3Int pushingDir);
    public abstract bool IsReachable();
    public abstract void Tick();
    
    internal void MoveThroughPath(List<Vector3> currentPath)
    {
        pathPositions = new Queue<Vector3>(currentPath);
        foreach(Vector3 pos in pathPositions){
            Debug.Log(pos);
        }
        Vector3 firstTarget = pathPositions.Dequeue();
        StartCoroutine(MovementCoroutine(firstTarget));
    }
    
    public IEnumerator MovementCoroutine(Vector3 endPosition)
    {
        Vector3 startPosition = transform.position;
        endPosition.y = startPosition.y;
        float timeElapsed = 0;
        float movementDuration = 1f / movSpeed;
        while (timeElapsed < movementDuration)
        {
            timeElapsed += Time.deltaTime;
            float step = timeElapsed / movementDuration;
            transform.position = Vector3.Lerp(startPosition, endPosition, step);
            yield return null;
        }
        transform.position = endPosition;
        MovementFinished?.Invoke(this);
        
    }
}
