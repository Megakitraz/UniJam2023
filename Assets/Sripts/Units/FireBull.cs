using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBull : Unit
{
    [SerializeField] private Animator _animatorFireBull;
    private bool isEnraged = false;
    [SerializeField] private Direction direction;
    private GameObject exclamationMark;
    
    // Start is called before the first frame update
    void Start()
    {
        //if (AudioManager.Instance != null) AudioManager.Instance.PlaySFX("idle_taureau", true);
        HideIndicator();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ShowIndicator()
    {
        //exclamationMark.SetActive(true);
    }

    void HideIndicator()
    {
        //exclamationMark.SetActive(false);
    }

    public override void Tick()
    {
        if (isEnraged)
        {
            Vector3Int coords = tileOn.tileCoords;

            switch (direction)
            {
                case Direction.up:
                    coords += new Vector3Int(0, 0, 1);
                    break;

                case Direction.right:
                    coords += new Vector3Int(1, 0, 0);
                    break;

                case Direction.down:
                    coords += new Vector3Int(0, 0, -1);
                    break;

                case Direction.left:
                    coords += new Vector3Int(-1, 0, 0);
                    break;
            }

            _animatorFireBull.SetBool("Run", true);
            AudioManager.Instance.PlaySFX("course_taureau", true);



            movementSystem.MoveEntity(this,coords);
            return;
        }
        else
        {
            _animatorFireBull.SetBool("Run", false);
        }
        ApplyEffectOnNeighbor();
        CheckPlayerVisibility();
        //if(AudioManager.Instance != null) AudioManager.Instance.PlaySFX("idle_taureau", true);
    }

    public override void ApplyEffectOnNeighbor()
    {
        var neighbors= tileGrid.GetNeighborsFor(tileOn.tileCoords);
        foreach (var neighbor in neighbors)
        {
            Tile tile = tileGrid.GetTileAt(neighbor);
            if(tile != null)
                tile.ApplyHeat();
        }
    }

    public void CheckPlayerVisibility()
    {
        if (LookAt(Direction.left))
        {
            isEnraged = true;
            direction = Direction.left;
            StartCoroutine(RotationCoroutine(new Vector3Int(0,0,1)));
        }
        else if (LookAt(Direction.right))
        {
            isEnraged = true;
            direction = Direction.right;
            StartCoroutine(RotationCoroutine(new Vector3Int(1,0,0)));
        }
        else if (LookAt(Direction.up))
        {
            isEnraged = true;
            direction = Direction.up;
            StartCoroutine(RotationCoroutine(new Vector3Int(0, 0, 1)));
        }
        else if (LookAt(Direction.down))
        {
            isEnraged = true;
            direction = Direction.down;
            StartCoroutine(RotationCoroutine(new Vector3Int(0, 0, -1)));
        }
    }

    private bool LookAt(Direction dir)
    {
        Vector3Int coords = tileOn.tileCoordinates.GetCoords();
        bool solved = false;
        while(!solved)
        {
            switch(dir)
            {
            case Direction.up:
                coords += new Vector3Int(0,0,1);
                break;

            case Direction.right:
                coords += new Vector3Int(1,0,0);
                break;

            case Direction.down:
                coords += new Vector3Int(0,0,-1);
                break;

            case Direction.left:
                coords += new Vector3Int(-1,0,0);
                break;

            default:
                Debug.Log(":(");
            break;
            }
            Debug.Log(coords);
            Tile tile = TileGrid.Instance.GetTileAt(coords);
            if (tile != null)
            {
                if (tile.IsPlayerOnTile())
                {
                    return true;
                }
                if (!tile.IsReachable())
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        return false;
    }


    public enum Direction
    {
        up,
        right,
        down,
        left
    }
    
    
}
