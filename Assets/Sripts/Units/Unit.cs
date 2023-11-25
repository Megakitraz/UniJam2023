using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;



[SelectionBase]

public abstract class Unit : MonoBehaviour
{
    public int mp;  //moving points

    public int maxMP; 
    
    public Tile tileOn;
    protected TileGrid tileGrid;

    protected MovementSystem movementSystem;

    [SerializeField] protected Animator _animatorFireBull;

    public float movSpeed;
    public float rotSpeed;
    
    private Queue<Vector3> pathPositions = new Queue<Vector3>();

    public event Action<Unit> MovementFinished;

    private void Awake()
    {
        if (tileOn != null)
            tileOn.unit = this;
        movementSystem = FindObjectOfType<MovementSystem>();
        tileGrid = FindObjectOfType<TileGrid>();
    }

    private void Update()
    {
    }

    public abstract void Tick();
    public abstract void ApplyEffectOnNeighbor();
    
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

    protected IEnumerator RotationCoroutine(Vector3Int direction)
    {
        Quaternion startRotation = transform.rotation;
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
    }

    public IEnumerator MovementCoroutine(Vector3 endPosition)
    {
        if (_animatorFireBull != null)
        {
            Debug.Log("Animation");
            _animatorFireBull.SetBool("Run", true);
            AudioManager.Instance.PlaySFX("course_taureau");
        }
        Vector3 startPosition = transform.position;
        endPosition.y = startPosition.y;
        float timeElapsed = 0;
        float movementDuration = (endPosition - startPosition).magnitude * 1f / movSpeed;
        while (timeElapsed < movementDuration)
        {
            timeElapsed += Time.deltaTime;
            float step = timeElapsed / movementDuration;
            transform.position = Vector3.Lerp(startPosition, endPosition, step);
            yield return null;
        }
        transform.position = endPosition;

        if (_animatorFireBull != null)
        {
            Debug.Log("Animation");
            _animatorFireBull.SetBool("Run", false);
            AudioManager.Instance.StopSFX();
        }

        MovementFinished?.Invoke(this);
        
    }
}


