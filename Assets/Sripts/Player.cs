using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Unit
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MovePlayer(Vector3Int pos)
    {
        var path = new List<Vector3Int> { pos };
        Move(path);
    }

    public override void Tick() {}
    protected override void ApplyEffectOnNeighbor() {}
}
