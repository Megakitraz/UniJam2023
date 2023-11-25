using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SelectionManager : MonoBehaviour
{
    [SerializeField]
    private Camera mainCamera;

    public LayerMask selectionMask;

    public TileGrid grid;

    private List<Vector3Int> selectedTiles = new List<Vector3Int>();

    public UnityEvent<GameObject> UnitSelected;
    public UnityEvent<GameObject> TileSelected;

    private void Awake()
    {
        if (mainCamera == null)
            mainCamera = Camera.main;
    }

    public void HandleClick(Vector3 mousePosition)
    {
        GameObject result;
        if (FindTarget(mousePosition, out result))
        {
            if (IsSelectedAnUnit(result))
            {
                Debug.Log("unit");
                UnitSelected?.Invoke(result);
            }
            else if (IsSelectedATile(result))
            {
                Debug.Log("tile");
                TileSelected?.Invoke(result);
            }
        }
    }

    private bool IsSelectedAnUnit(GameObject result)
    {
        return result.GetComponent<Unit>() != null;
    }

    private bool IsSelectedATile(GameObject result)
    {
        return result.GetComponent<Tile>() != null;
    }


    private bool FindTarget(Vector3 mousePosition, out GameObject result)
    {
        RaycastHit hit;
        Ray ray = mainCamera.ScreenPointToRay(mousePosition);
        if (Physics.Raycast(ray, out hit, 50f, selectionMask))
        {
            result = hit.collider.gameObject;
            return true;
        }
        result = null;
        return false;
    }

}
