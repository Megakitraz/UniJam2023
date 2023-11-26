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
    public int currentLevel = 1 ;
    private int maxLevel = 10 ;
    public float turnDelay  = 0.1f;

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
        if (scene.name != "TitleScreen" && scene.name != "Cinematic")
        {
        Debug.Log(1);
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        Debug.Log(2);
        player = GameObject.Find("Player").GetComponent<Unit>();
        Debug.Log(3);
        actionManager = GameObject.Find("ActionManager").GetComponent<ActionManager>();
        Debug.Log(4);
        movementSystem = GameObject.Find("MovementSystem").GetComponent<MovementSystem>();
        Debug.Log(5);
        movementSystem.grid.InitGrid();
        Debug.Log(6);
        PauseScreen.Instance.gameObject.SetActive(false);
        Debug.Log(7);
        StartTurn();
        }
        PauseScreen.Instance.gameObject.SetActive(false);
        Debug.Log(7);
        
    }

    void Start()
    {
        movementSystem.grid.InitGrid();
        StartTurnAux();
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
        Invoke("StartTurnAux2", 0.1f);
    }

    public void StartTurnAux2()
    {
        Invoke("StartTurnAux", turnDelay);
    }

    public void StartTurnAux()
    {
        Debug.Log("Start turn");
        turnDelay = 0.1f;
        //actionManager.selectedUnit = null;
        actionManager.HandleUnitSelected(player.gameObject);
        TryWin();
    }

    void TryWin()
    {
        Debug.Log(player.tileOn.groundtype);
        Debug.Log(Groundtype.Portal);
        if(player.tileOn.groundtype == Groundtype.Portal)
        {
            Win();
        }
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

        if(SceneManager.GetSceneByName("TitleScreen").buildIndex == level)
        {
            AudioManager.Instance.PlayMusic("menu");
        }
        else if(SceneManager.GetSceneByName("Cinematic").buildIndex == level)
        {
            AudioManager.Instance.PlayMusic("musicIntro");
        }
        else if(SceneManager.GetSceneByName("Level1").buildIndex == level)
        {
            AudioManager.Instance.PlayMusic("musicLevel1");
        }
        else if (SceneManager.GetSceneByName("Level2").buildIndex == level)
        {
            AudioManager.Instance.PlayMusic("musicLevel2");
        }
        else if(SceneManager.GetSceneByName("Level3").buildIndex == level)
        {
            AudioManager.Instance.PlayMusic("musicLevel3");
        }
        else
        {
            AudioManager.Instance.PlayMusic("musicLevel4");
        }
    }


}
