using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    static public GameManager Instance;

    public static int mana;
    public static List<Vector3Int> range;
    [SerializeField] private Camera mainCamera;
    [SerializeField] public Unit player;
    [SerializeField] private ActionManager actionManager;
    [SerializeField] public MovementSystem movementSystem;
    public static bool playerCanPlay;

    void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);    
        Instance = this;
        DontDestroyOnLoad(this.gameObject);
        playerCanPlay = true;
        
    }

    void Start()
    {
        TileGrid.Instance.InitGrid();
        StartTurn();
    }

    public void StartTurn()
    {
        Debug.Log("Start turn");
        
        //actionManager.selectedUnit = null;
        actionManager.HandleUnitSelected(player.gameObject);
    }

    private void OnLevelWasLoaded(int level)
    {
        AudioManager.Instance.StopMusic();
        AudioManager.Instance.StopSFX();
        AudioManager.Instance.StopSFXLoop();

        if(level == 0)
        {
            AudioManager.Instance.PlayMusic("menu");
        }
        else if(level == 1)
        {
            AudioManager.Instance.PlayMusic("musicLevel1");
        }
        else if(level == 2)
        {
            AudioManager.Instance.PlayMusic("musicLevel2");
        }
        else
        {
            AudioManager.Instance.PlayMusic("musicLevel3");
        }
    }


}
