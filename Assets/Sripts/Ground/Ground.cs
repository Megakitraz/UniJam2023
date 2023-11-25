using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ground : MonoBehaviour
{
    public Tile tileOn;
    public Vector3Int tileCoord;
    protected TileGrid tileGrid;

    private void Awake()
    {
        tileGrid = FindObjectOfType<TileGrid>();
    }

    public abstract void ApplyHeat();
    public abstract void ApplyCold();
    public abstract bool IsReachable();
    public abstract bool IsSlippery();
}
