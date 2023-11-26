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

    public TileCoordinates tileCoordinates;

    [SerializeField]
    public int tileCost;

    private TileGrid grid;

    private Groundtype type;

    private MeshRenderer renderer;
    
    [SerializeField] private List<Material> tileMaterials;

    public Groundtype groundtype
    {
        get => _groundtype;
        set
        {
            if (_groundtype != value)
            {
                _groundtype = value;
                UpdateGroundModel();
            }
        }
    }

    private void UpdateGroundModel()
    {
        switch (_groundtype)
        {
            case Groundtype.Normal:
                renderer.material = tileMaterials[0];
                break;
            case Groundtype.Portal:
                renderer.material = tileMaterials[1];
                break;
            case Groundtype.Soaked:
                renderer.material = tileMaterials[2];
                break;
            case Groundtype.Ice:
                renderer.material = tileMaterials[3];
                break;
            default:
                break;
        }

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

    public bool IsMovableOn(Vector3Int dir)
    {
        if (IsReachable())
        {
           return true;
        }
            
        
        else if (obstacle != null && obstacle.isPushable)
        {
            Tile nextTile = grid.GetTileAt(tileCoordinates.GetCoords() + dir);
            if (nextTile != null && nextTile.IsPushableOn())
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        else
            return false;
    }

    public bool IsPushableOn()
    {
        if (obstacle != null || unit != null)
        {
            return false;
        } 
        return true;
    }

    public bool IsPlayerOnTile()
    {
        if (unit == null)
        {
            return false;
        }   
        if (unit.GetComponent<Player>() != null)
        {
            return true;
        }

        return false;
    }

    public bool IsSlippery()
    {
        return GroundManager.IsSlippery(groundtype);
    }

    private void Awake()
    {
        if (obstacle != null)
            obstacle.tileOn = this;
        if (unit != null)
            unit.tileOn = this;
        tileCoordinates = GetComponent<TileCoordinates>();
        grid = FindObjectsOfType<TileGrid>()[0];
        renderer = GetComponentInChildren<MeshRenderer>(); 
        UpdateGroundModel();
    }

    public void EnableHighlight1()
    {           
        renderer.material.SetColor("_EmissionColor",Color.grey);
        renderer.material.EnableKeyword("_Emission");
    }

    public void DisableHighlight1()
    {
        renderer.material.SetColor("_EmissionColor",Color.black);
    }

    public void EnableHighlight2()
    {
      
    }

    public void DisableHighlight2()
    {
       
    }

    public void EnableHighlight3()
    {
        
    }

    public void DisableHighlight3()
    {
        highlight.ToggleGlow3(false);
    }

    public void DisableHighlights()
    {
        renderer.material.SetColor("_EmissionColor",Color.black);
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



