using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class TitleScreen : MonoBehaviour
{
    [SerializeField] GameObject PlayPanel;
    [SerializeField] GameObject OptionPanel;

    // Start is called before the first frame update
    void Start()
    {
        DisplayTitleScreen();
    }

    // Update is called once per frame
    void Update()
    {
        
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
        SceneManager.LoadScene(1);
    }
}