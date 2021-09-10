using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quit : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Quit");
                #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
                #else
            Application.Quit();
                #endif
        }
    }
}
