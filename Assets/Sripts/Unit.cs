using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;



[SelectionBase]

public abstract class Unit : MonoBehaviour
{
    public Tile tileOn;
    public Vector3Int tileCoord;
    protected TileGrid tileGrid;
    
    public float movSpeed;
    public float rotSpeed;
    
    [SerializeField] private GlowHighlight glowHighlight;
    public event Action<Unit> MovementFinished;

    private void Awake()
    {
        glowHighlight = GetComponent<GlowHighlight>();
        tileGrid = FindObjectOfType<TileGrid>();
    }

    private void Update()
    {
    }

    public abstract void Tick();
    protected abstract void ApplyEffectOnNeighbor();

    public void Select()
    {
        glowHighlight.ToggleGlow1(true);
    }

    public void Deselect()
    {
        glowHighlight.ToggleGlow1(false);
    }

    internal void Move(List<Vector3Int> currentPath)
    {
        foreach(Vector3Int pos in currentPath)
        {
            Tile tile = tileGrid.GetTileAt(pos);
            if (!tile.IsReachable())
            {
                
                break;
            }
            tileCoord = pos;
            ApplyEffectOnNeighbor();
        }
        StartCoroutine(RotationCoroutine(tileCoord));
    }

    private IEnumerator RotationCoroutine(Vector3 endPosition)
    {
        Quaternion startRotation = transform.rotation;
        endPosition.y = transform.position.y;
        Vector3 direction = endPosition - transform.position;
        Quaternion endRotation = Quaternion.LookRotation(direction, Vector3.up);

        if(Mathf.Approximately(Math.Abs(Quaternion.Dot(startRotation, endRotation)), 1.0f) == false)
        {
            float timeElapsed = 0;
            float rotationDuration = 1f / rotSpeed;
            while (timeElapsed < rotationDuration)
            {
                timeElapsed += Time.deltaTime;
                float step = timeElapsed / rotationDuration;
                transform.rotation = Quaternion.Lerp(startRotation, endRotation, step);
                yield return null;
            }
            transform.rotation = endRotation;
        }
        StartCoroutine(MovementCoroutine(endPosition));
    }

    private IEnumerator MovementCoroutine(Vector3 endPosition)
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


