using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageControlMode : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject fightCenter;
    public GameObject birdPusherAwayer;

    public float speed;
    private float speedMultiplier;

    private Vector3 prevPos;

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<ModeManager>().GetIDCM())
        {
            // Embody individual raven; fly in a revolution around a central axis, where a raven fight is occuring
            // Shield other ravens and "push" them away from the fight as they slowly gravitate closer around the fight

            if (Input.GetKey("left shift"))
            {
                speedMultiplier = 1.66f;
            }
            else
            {
                speedMultiplier = 1f;
            }

            if (Input.GetKey(KeyCode.D))
            {
                playerPrefab.transform.RotateAround(fightCenter.transform.position, Vector3.up, -speed * speedMultiplier * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.A))
            {
                playerPrefab.transform.RotateAround(fightCenter.transform.position, Vector3.up, speed * speedMultiplier * Time.deltaTime);
            }

            // Updates stress meter

            if (GetComponent<ModeManager>().stress < 100 && GetComponent<ModeManager>().stress != 0)
            {
                GetComponent<ModeManager>().stress += .005f;
            }
        }
    }

    // Initiates a fight between two ravens
    public void SpawnFight()
    {
        fightCenter.GetComponent<MeshRenderer>().enabled = true;
        birdPusherAwayer.GetComponent<MeshRenderer>().enabled = true;
        birdPusherAwayer.GetComponent<BoxCollider>().enabled = true;
    }

    // Ends active raven fight
    public void EndFight()
    {
        fightCenter.GetComponent<MeshRenderer>().enabled = false;
        birdPusherAwayer.GetComponent<MeshRenderer>().enabled = false;
        birdPusherAwayer.GetComponent<BoxCollider>().enabled = false;
    }
}
