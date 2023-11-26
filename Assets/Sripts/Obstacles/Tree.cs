using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : Obstacle
{
    private bool isBurning = false;
    [SerializeField]
    private int burnTime;

    private List<ParticleSystem> particleSystems;

    public void Start()
    {
        particleSystems = new List<ParticleSystem>(GetComponentsInChildren<ParticleSystem>());
    }

    public override void ApplyHeat()
    {
        isBurning = true;
        AudioManager.Instance.PlaySFX("feu_vegetation");
        foreach (ParticleSystem system in particleSystems)
        {
            system.Play();
        }
    }

    public override void ApplyCold() {}

    public override void ApplyPush(Vector3Int pushingDir) {}

    public override bool IsReachable()
    {
        return false;
    }

    public override void Tick()
    {
        if (isBurning)
        {
            burnTime--;
            var neighbors = tileGrid.GetNeighborsFor(tileOn.tileCoords);
            foreach (var neighbor in neighbors)
            {
                Tile tile = tileGrid.GetTileAt(neighbor);
                tile.ApplyHeat();
            }

            if (burnTime < 0)
            {
                tileOn.obstacle = null;
                Destroy(gameObject);
            }
        }
    }
}
