using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionManager : MonoBehaviour
{
    public TileGrid tileGrid;
    [SerializeField] private GameManager gManager;
    [SerializeField] private MovementSystem movementSystem;
    [SerializeField] public Player player;
    public Player selectedUnit;
    private Tile previouslySelectedTile;


    public void HandleUnitSelected(GameObject unit)
    {
        if (!GameManager.playerCanPlay)
        {
            return;
        }
        Player unitReference = unit.GetComponent<Player>();
        PrepareUnitForMovement(unitReference);
    }

    private bool CheckIfTheSameUnitSelected(Unit unitReference)
    {
        if (selectedUnit == unitReference)
        {
            //ClearOldSelection();
            return true;
        }
        return false;
    }

    
    public void HandleTileSelected(GameObject tileGO)
    {

        if(selectedUnit == null || !GameManager.playerCanPlay)
        {
            return;
        }

        Tile selectedTile = tileGO.GetComponent<Tile>();
        
        if (HandleTileOutOfRange(selectedTile.tileCoords) || HandleSelectedTileIsUnitTile(selectedTile))
        {
            //ClearOldSelection();
            return;
        }
        //HandleTargetTileSelected(selectedTile);
        previouslySelectedTile = selectedTile;
        movementSystem.ShowPath(selectedTile.tileCoords);
        StartCoroutine(movementSystem.MoveUnit(selectedUnit));
        ClearOldSelection();
        AudioManager.Instance.PlaySFX("clicktiles");

    }

    public void PrepareUnitForMovement(Player unitReference)
    {
        if (selectedUnit != null)
        {
            ClearOldSelection();
        }
        selectedUnit = unitReference;
        movementSystem.ShowRange(selectedUnit);
    }

    //
    private void HandleTargetTileSelected(Tile selectedTile)
    {
        if (previouslySelectedTile == null ||previouslySelectedTile != selectedTile)
        {
            previouslySelectedTile = selectedTile;
            movementSystem.ShowPath(selectedTile.tileCoords);
        }
        else
        {
            movementSystem.MoveUnit(selectedUnit);
            ClearOldSelection();
        }
    }
    

    public void ClearOldSelection()
    {
        if (selectedUnit != null)
        {
            HideCurrentRange();
            selectedUnit = null;
        } 
    }


    private bool HandleSelectedTileIsUnitTile(Tile selectedTile)
    {
        if(selectedTile.unit != null)
        {
            //ClearOldSelection();
            return true;
        }
        return false;
    }

    
    private bool HandleTileOutOfRange(Vector3Int tilePosition)
    {
        if (movementSystem.IsTileInRange(tilePosition) == false)
        {
            return true;
        }
        return false;
    }

    public void HideCurrentRange()
    {
        previouslySelectedTile = null;
        movementSystem.HideRange();
    }
}

