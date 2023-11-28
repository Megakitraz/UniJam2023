using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndCinematic : MonoBehaviour
{
    public UIElement blackFade;
    public UIElement introImage;
    public Sprite end1;
    public Sprite end2;
    public float fadeDuration;

    void Start()
    {
        blackFade.durationOfFade = fadeDuration;
        introImage.durationOfFade = fadeDuration;
        blackFade.Show();
        introImage.ChangeImage(end1);
        introImage.Show();
        Invoke("BlackFade", fadeDuration);
        Invoke("BlackUnfade", fadeDuration * 4);
        Invoke("ChangeImage1", fadeDuration * 5);
        Invoke("BlackFade", fadeDuration * 5.2f);
        Invoke("BlackUnfade", fadeDuration * 18.8f);
        Invoke("LoadTitle", fadeDuration * 20f);
    }

    void ChangeImage1()
    {
        introImage.ChangeImage(end2);
    }

    public void LoadTitle()
    {
        SceneManager.LoadScene("TitleScreen");
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
