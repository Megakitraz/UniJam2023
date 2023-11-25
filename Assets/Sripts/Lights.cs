using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lights : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator RandomInstensity(float moveSpeed)
    {
        while (true)
        {
            float randomTarget = Random.Range(3f, 5f);
            Vector3 direction = (randomTarget - transform.position).normalized;

            while (Vector3.Distance(transform.position, randomTarget) > .1)
            {
                transform.position += direction * Time.fixedDeltaTime * moveSpeed;
                yield return new WaitForFixedUpdate();
            }
        }
    }
}
