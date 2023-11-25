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
        StartTurn();
    }

    void StartTurn()
    {
        actionManager.HandleUnitSelected(player.gameObject);
    }   

}
