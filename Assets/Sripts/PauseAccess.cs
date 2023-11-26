using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseAccess : MonoBehaviour
{
    public void OpenClosePauseGroup()
    {
        GameManager.Instance.Pause();
    }
}
