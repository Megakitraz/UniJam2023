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
    [SerializeField] public Player player;
    [SerializeField] private ActionManager actionManager;
    [SerializeField] public MovementSystem movementSystem;
    public static bool playerCanPlay;
    public int currentLevel = 1 ;
    private int maxLevel = 9 ;
    public float turnDelay  = 0.1f;

    private Coroutine _coroutineWin;
    public bool _isWinning;
    public bool _isDying;

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

        _isWinning = false;
        _isDying = false;
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

        if (scene.name != "TitleScreen" && scene.name != "Cinematic" && scene.name != "EndScene")
        {
        Debug.Log(1);
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        Debug.Log(2);
        player = GameObject.Find("Player").GetComponent<Player>();
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
        LoadSound(scene);
        
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
        TryWin();
        actionManager.HandleUnitSelected(player.gameObject);
        
    }

    void TryWin()
    {
        Debug.Log(player.tileOn.groundtype);
        Debug.Log(Groundtype.Portal);
        if(player.tileOn.groundtype == Groundtype.Portal)
        {
            if (_isWinning) return;
            _coroutineWin = StartCoroutine(Win());
        }
    }
    private IEnumerator Win()
    {
        if (!_isDying)
        {
            _isWinning = true;
            Debug.Log("Win True");
            currentLevel++;

            player.ActivateParticules();
            yield return new WaitForSeconds(1);
            if(!_isDying)
            {
                if (currentLevel <= maxLevel)
                {
                    SceneManager.LoadScene("Level" + currentLevel.ToString());
                }
                else
                {
                    SceneManager.LoadScene("EndScene");
                }
            }
            
        }
        

        _isWinning = false;
    }

    private void LoadSound(Scene scene)
    {
        AudioManager.Instance.StopMusic();
        AudioManager.Instance.StopSFX();
        AudioManager.Instance.StopSFXLoop();

        if(scene.name == "TitleScreen")
        {
            AudioManager.Instance.PlayMusic("menu");
        }
        else if(scene.name == "Cinematic")
        {
            AudioManager.Instance.PlayMusic("musicIntro");
        }
        else if(scene.name == "EndScene")
        {
            AudioManager.Instance.PlayMusic("musicIntro");
        }
        else if(scene.name == "Level1")
        {
            AudioManager.Instance.PlayMusic("musicLevel1");
        }
        else if (scene.name == "Level2")
        {
            AudioManager.Instance.PlayMusic("musicLevel2");
        }
        else if(scene.name == "Level3")
        {
            AudioManager.Instance.PlayMusic("musicLevel3");
        }
        else if(scene.name == "Level4")
        {
            AudioManager.Instance.PlayMusic("musicLevel4");
        }
        else if(scene.name == "Level5")
        {
            AudioManager.Instance.PlayMusic("musicLevel2");
        }
        else if(scene.name == "Level6")
        {
            AudioManager.Instance.PlayMusic("musicLevel4");
        }
        else if(scene.name == "Level7")
        {
            AudioManager.Instance.PlayMusic("musicLevel3");
        }
        else
        {
            AudioManager.Instance.PlayMusic("musicLevel2");
        }
    }


}
