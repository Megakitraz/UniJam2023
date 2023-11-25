using System.Collections;
using System.Collections.Generic;
using System;
using System.ComponentModel;
using UnityEngine;

[SelectionBase]

public class Tile : MonoBehaviour
{
    [SerializeField]
    private GlowHighlight highlight;

    private TileCoordinates tileCoordinates;

    [SerializeField]
    public int tileCost;

    private TileGrid grid;

    private Groundtype type;

    public Groundtype groundtype
    {
        get => _groundtype;
        set
        {
            _groundtype = value;
            UpdateGroundModel();
        }
    }

    private void UpdateGroundModel()
    {
        throw new NotImplementedException();
    }

    [SerializeField]
    public Unit unit;

    [SerializeField]
    public Obstacle obstacle;

    [SerializeField] private Groundtype _groundtype;

    public Vector3Int tileCoords => tileCoordinates.GetCoords();

    public int GetCost()
    {
        return (tileCost);
    }

    public bool IsReachable()
    {
        if (obstacle != null || unit != null)
            return false;
        
        return GroundManager.IsReachable(groundtype);
    }

    public bool IsPlayerOnTile()
    {
        if (unit != null)
            return false;
        //if (unit.GetType() == typeof(Player))
        //{
            //return true;
        //}

        return false;
    }

    private void Awake()
    {
        tileCoordinates = GetComponent<TileCoordinates>();
        grid = FindObjectsOfType<TileGrid>()[0];
    }

    public void EnableHighlight1()
    {           
        highlight.ToggleGlow2(false);
        highlight.ToggleGlow3(false);
        highlight.ToggleGlow1(true);
    }

    public void DisableHighlight1()
    {
        highlight.ToggleGlow1(false);
    }

    public void EnableHighlight2()
    {
        
        highlight.ToggleGlow1(false);
        highlight.ToggleGlow3(false);
        highlight.ToggleGlow2(true);
    }

    public void DisableHighlight2()
    {
        highlight.ToggleGlow2(false);
    }

    public void EnableHighlight3()
    {
        
        highlight.ToggleGlow1(false);
        highlight.ToggleGlow2(false);
        highlight.ToggleGlow3(true);
    }

    public void DisableHighlight3()
    {
        highlight.ToggleGlow3(false);
    }

    public void DisableHighlights()
    {
        highlight.ToggleGlow1(false);
        highlight.ToggleGlow2(false);
        highlight.ToggleGlow3(false);
    }
    

    public void ApplyHeat()
    {
            groundtype = GroundManager.ApplyHeat(groundtype);
        if(obstacle != null)
            obstacle.ApplyHeat();
    }

    public void ApplyCold()
    {
        groundtype = GroundManager.ApplyCold(groundtype);
        if(obstacle != null)
            obstacle.ApplyCold();
    }
}



