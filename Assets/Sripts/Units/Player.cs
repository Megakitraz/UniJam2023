using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Unit
{
    
    public event Action<Player> MovementFinished;
    public Animator _animator;
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
    
    public IEnumerator MovementCoroutine(Vector3 endPosition)
    {
        _animator.speed = 2;
        _animator.SetBool("Walk", true);
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
        _animator.SetBool("Walk", false);
        transform.position = endPosition;
        MovementFinished?.Invoke(this);
        GameManager.Instance.StartTurn();
    }
}
