using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioRedirector : MonoBehaviour
{
    public void PlaySoundDebutAnim()
    {
        AudioManager.Instance.StopSFXLoop();
        AudioManager.Instance.PlaySFX("course_taureau");
        //AudioManager.Instance.PlaySFX("feu_taureau");
    }

    public void PlaySoundFinAnim()
    {
        AudioManager.Instance.PlayLoopSFX("idle_taureau");
    }


    public void PlayRandomStepHuman()
    {
        Random.InitState(System.DateTime.Now.Second);

        /*
        switch(Random.Range(0, 3))
        {
            case 0:
                AudioManager.Instance.PlaySFX("mage_walk");
                break;
            case 1:
                AudioManager.Instance.PlaySFX("mage_walk");
                break;
            case 2:
                AudioManager.Instance.PlaySFX("mage_walk");
                break;
            default:
                
                break;
        }
        */
        AudioManager.Instance.PlaySFX("mage_walk");
    }
}
