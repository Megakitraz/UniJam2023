using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lights : MonoBehaviour
{
    [SerializeField] Material material;
    [SerializeField] float changeSpeed;
    Color emissionColorValue;
    // Start is called before the first frame update
    void Awake()
    {
        emissionColorValue = new Color(1f, 0.9953098f, 0.6735849f);
        StartCoroutine(RandomInstensity());
    }


    private IEnumerator RandomInstensity()
    {
        float intensity = 0.8f;
        while (true)
        {
            float randomTarget = Random.Range(0.6f, 1f);

            while (Mathf.Abs(intensity - randomTarget) > .05)
            {
                intensity += (randomTarget-intensity) * Time.fixedDeltaTime;
                material.SetVector("_EmissionColor", emissionColorValue * intensity);
                yield return new WaitForFixedUpdate();
            }
        }
    }
}
