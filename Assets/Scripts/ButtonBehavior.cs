using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonBehavior : MonoBehaviour
{
    public void OnReplayPress()
    {
        Debug.Log("Replay");
        SceneManager.LoadScene("Raven World");
    }

    public void OnQuitPress()
    {
        Debug.Log("Quit");
            #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
            #else
        Application.Quit();
            #endif
    }
}
