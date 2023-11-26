using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPauseGroup : MonoBehaviour
{
    // Start is called before the first frame update
    public void PlaySound(string name)
    {
        AudioManager.Instance.PlaySFX(name);
    }
}
