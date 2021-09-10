using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject gameManager;
    public Transform[] views;
    public float transitionSpeed;
    Transform currentView;
    private bool flagDmg;
    private bool flagTend;
    private bool flagFlock;

    // Start is called before the first frame update
    void Start()
    {
        currentView = views[0];
        Debug.Log("Tend mode");
        flagTend = true;
        
        //Cursor.lockState = CursorLockMode.Confined;
    }

    void Update()
    {
        if (!flagTend && gameManager.GetComponent<ModeManager>().GetITM())
        {
            Debug.Log("Tend mode");
            currentView = views[0];
            gameManager.GetComponent<DamageControlMode>().EndFight();
            flagDmg = false;
            flagTend = true;
        } else if (!flagFlock && gameManager.GetComponent<ModeManager>().GetIFM())
        {
            Debug.Log("Flock mode");
            currentView = views[1];
            flagTend = false;
            flagFlock = true;
        } else if (!flagDmg && (gameManager.GetComponent<ModeManager>().GetIDCM()))
        {
            Debug.Log("Damage control mode");
            currentView = views[2];
            gameManager.GetComponent<DamageControlMode>().SpawnFight();
            flagFlock = false;
            flagDmg = true;
        }
    }

    void LateUpdate()
    {
        // Lerp position & rotation
        transform.position = Vector3.Lerp(transform.position, currentView.position, Time.deltaTime * transitionSpeed);
        transform.rotation = Quaternion.Lerp(transform.rotation, currentView.rotation, Time.deltaTime * transitionSpeed);
    }
}
