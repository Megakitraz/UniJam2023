using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileCoordinates : MonoBehaviour
{
   public static float xOffset = 2, yOffset = 2, zOffset = 2;

    internal Vector3Int GetCoords()
    {
        return offsetCoordinates;
    }

   [SerializeField]
   private Vector3Int offsetCoordinates;

    private void Awake()
    {
        offsetCoordinates = ConvertPositionToOffset(transform.position);
    }

    private Vector3Int ConvertPositionToOffset(Vector3 position)
    {
        int x = Mathf.RoundToInt(position.x / xOffset);
        int y = Mathf.RoundToInt(position.y / yOffset);
        int z = Mathf.RoundToInt(position.z / zOffset);
        return new Vector3Int(x, y, z);
    }

}
