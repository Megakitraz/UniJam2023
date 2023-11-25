using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;



[SelectionBase]

public class Unit : MonoBehaviour
{
    public int mp;  //moving points

    public int maxMP; 

    public float movSpeed;
    public float rotSpeed;

    public Tile tileOn;
    public Vector3Int tileCoord;
    
    protected Queue<Vector3> pathPositions = new Queue<Vector3>();

    public event Action<Unit> MovementFinished;

    

    internal void MoveThroughPath(List<Vector3> currentPath)
    {
        pathPositions = new Queue<Vector3>(currentPath);
        foreach(Vector3 pos in pathPositions){
            Debug.Log(pos);
        }
        Vector3 firstTarget = pathPositions.Dequeue();
        StartCoroutine(RotationCoroutine(firstTarget));
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

        if (pathPositions.Count > 0)
        {
            StartCoroutine(RotationCoroutine(pathPositions.Dequeue()));
        }
        else
        {
            MovementFinished?.Invoke(this);
            RefillMP(); //A BOUGER DANS L'EVENT
        }
    }

    private void RefillMP()
    {
        mp = maxMP;
    }
}


