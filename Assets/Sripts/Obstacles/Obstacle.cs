using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



abstract public class Obstacle : MonoBehaviour {
    public Tile tileOn;
    protected TileGrid tileGrid;
    protected MovementSystem movementSystem;

    public bool isPushable;

    private Queue<Vector3> pathPositions = new Queue<Vector3>();
    public float movSpeed;
    public event Action<Obstacle> MovementFinished;


    private void Awake()
    {
        if (tileOn != null)
            tileOn.obstacle = this;
        movementSystem = FindObjectOfType<MovementSystem>();
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
        if(gameObject.TryGetComponent<Box>(out Box box))
        {
            AudioManager.Instance.PlayLoopSFX("push_crate");
        }
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

        if (gameObject.TryGetComponent<Box>(out box))
        {
            AudioManager.Instance.StopSFXLoop();
        }

    }

}
