using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class TitleScreen : MonoBehaviour
{
    [SerializeField] GameObject PlayPanel;
    [SerializeField] GameObject OptionPanel;
    [SerializeField] GameObject QuitButton;

    // Start is called before the first frame update
    void Start()
    {
        DisplayTitleScreen();
        AudioManager.Instance.PlayMusic("menu");
        PauseScreen.Instance.gameObject.SetActive(false);
        if (Application.platform == RuntimePlatform.WebGLPlayer) QuitButton.SetActive(false);
    }


    public void DisplayOptionScreen()
    {
        OptionPanel?.SetActive(true);
        PlayPanel?.SetActive(false);
    }

    public void DisplayTitleScreen()
    {
        OptionPanel?.SetActive(false);
        PlayPanel?.SetActive(true);
    }

    public void Play()
    {
        SceneManager.LoadScene("Cinematic");
    }
    public void Quit()
    {
    #if (UNITY_EDITOR)
        UnityEditor.EditorApplication.isPlaying = false;
    #elif (UNITY_STANDALONE)
        Application.Quit();
    #elif (UNITY_WEBGL)
        Application.ExternalEval("window.close();");
    #endif
    }
}
