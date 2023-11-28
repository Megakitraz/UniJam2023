using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Unit
{
    
    public event Action<Player> MovementFinished;
    public Animator _animator;
    [SerializeField] private GameObject _portalParticules;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    

    public override void Tick() {}
    public override void ApplyEffectOnNeighbor() {}

    public IEnumerator RotationCoroutine(Vector3 endPosition)
    {
        Quaternion startRotation = transform.rotation;
        endPosition.y = transform.position.y;
        Vector3 direction = endPosition - transform.position;
        Quaternion endRotation = Quaternion.LookRotation(direction, Vector3.up);

        if (Mathf.Approximately(Math.Abs(Quaternion.Dot(startRotation, endRotation)), 1.0f) == false)
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
    public IEnumerator MovementCoroutine(Vector3 endPosition)
    {
        if (_animator != null)
        {
            _animator.speed = 2;
            _animator.SetBool("Walk", true);
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

        if (_animator != null)
        {
            _animator.SetBool("Walk", false);
        }

        transform.position = endPosition;
        MovementFinished?.Invoke(this);
        //GameManager.Instance.StartTurn();
    }

    public void ActivateParticules()
    {
        _portalParticules.SetActive(true);
    }
}
