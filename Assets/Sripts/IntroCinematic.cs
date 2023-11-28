using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntroCinematic : MonoBehaviour
{
    public UIElement blackFade;
    public UIElement introImage;
    public Sprite intro1;
    public Sprite intro2;
    public float fadeDuration;

    void Start()
    {
        AudioManager.Instance.PlayMusic("musicIntro");
        blackFade.durationOfFade = fadeDuration;
        introImage.durationOfFade = fadeDuration;
        blackFade.Show();
        introImage.ChangeImage(intro1);
        introImage.Show();
        Invoke("BlackFade", fadeDuration  );
        Invoke("BlackUnfade", fadeDuration * 4 );
        Invoke ("ChangeImage1", fadeDuration * 5 );
        Invoke("BlackFade", fadeDuration * 5.2f );
        Invoke("BlackUnfade", fadeDuration * 8f );
        Invoke("LoadLevel1", fadeDuration * 9.2f );


    }


    void ChangeImage1()
    {
        introImage.ChangeImage(intro2);
    }

    public void LoadLevel1()
    {
        
        SceneManager.LoadScene("Level1");
    }

    void ImageFade()
    {
        introImage.Fade();
    }

    void ImageUnfade()
    {
        introImage.Unfade();
    }

    void BlackFade()
    {
        blackFade.Fade();
    }

    void BlackUnfade()
    {
        blackFade.Unfade();
    }




}

