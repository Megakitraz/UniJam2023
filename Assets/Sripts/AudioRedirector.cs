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

        
        switch(Random.Range(0, 9))
        {
            case 0:
                AudioManager.Instance.PlaySFX("mage_walk1");
                break;
            case 1:
                AudioManager.Instance.PlaySFX("mage_walk2");
                break;
            case 2:
                AudioManager.Instance.PlaySFX("mage_walk3");
                break;
            case 3:
                AudioManager.Instance.PlaySFX("mage_walk4");
                break;
            case 4:
                AudioManager.Instance.PlaySFX("mage_walk5");
                break;
            case 5:
                AudioManager.Instance.PlaySFX("mage_walk6");
                break;
            case 6:
                AudioManager.Instance.PlaySFX("mage_walk7");
                break;
            case 7:
                AudioManager.Instance.PlaySFX("mage_walk8");
                break;
            case 8:
                AudioManager.Instance.PlaySFX("mage_walk9");
                break;
            case 9:
                AudioManager.Instance.PlaySFX("mage_walk10");
                break;
            default:
                break;
        }
        
        
    }
}
