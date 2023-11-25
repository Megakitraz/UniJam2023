#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(AudioManager))]
public class AudioManagerInterface : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        AudioManager audioManager = (AudioManager)target;

        if (GUILayout.Button("Play Music with Test name"))
        {
            audioManager.TestSound(true);
        }

        if (GUILayout.Button("Play SFX with Test name"))
        {
            audioManager.TestSound(false);
        }

        if (GUILayout.Button("Stop Music"))
        {
            audioManager.StopMusic();
        }

        if (GUILayout.Button("Stop SFX"))
        {
            audioManager.StopSFX();
        }

        if (GUILayout.Button("Reset Audio Sources"))
        {
            audioManager.ResetAudioSources();
        }

    }
}
#endif