using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinOrLose : MonoBehaviour
{
    public static bool isWin;

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<ModeManager>().harmony == 100f) // Win Case
        {
            isWin = true;
            SceneManager.LoadScene("WinScreen");
        } else if (GetComponent<ModeManager>().harmony <= 0.5f) // Lose Case
        {
            isWin = false;
            SceneManager.LoadScene("LoseScreen");
        }
    }
}
