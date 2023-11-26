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
    private int currentLevel = 0 ;
    private int maxLevel = 10 ;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        playerCanPlay = true;
    }
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        player = GameObject.Find("Player").GetComponent<Unit>();
        actionManager = GameObject.Find("ActionManager").GetComponent<ActionManager>();
        movementSystem = GameObject.Find("MovementSystem").GetComponent<MovementSystem>();
        movementSystem.grid.InitGrid();
        PauseScreen.Instance.gameObject.SetActive(false);
        StartTurn();
    }

    void Start()
    {
        movementSystem.grid.InitGrid();
        StartTurn();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }

    public void Pause()
    {
        if (playerCanPlay)
        {
            playerCanPlay = false;
            Debug.Log(PauseScreen.Instance);
            PauseScreen.Instance.gameObject.SetActive(true);
        }
        else
        {
            playerCanPlay = true;
            PauseScreen.Instance.gameObject.SetActive(false);
        }
    }

    public void StartTurn()
    {
        Debug.Log("Start turn");
        
        //actionManager.selectedUnit = null;
        actionManager.HandleUnitSelected(player.gameObject);
    }

    void Win()
    {
        currentLevel ++;
        if (currentLevel <= maxLevel)
        {
            SceneManager.LoadScene("Level" + currentLevel.ToString());
        }
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
        else if (level == 3)
        {
            AudioManager.Instance.PlayMusic("musicLevel3");
        }
        else
        {
            AudioManager.Instance.PlayMusic("musicLevel4");
        }
    }


}
