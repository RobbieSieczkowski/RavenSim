using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockMode : MonoBehaviour
{
    public GameObject flock;
    public GameObject playerPrefab;

    public float forwardSpeed;
    public float strafeSpeed;
    private float activeForwardSpeed;
    private float activeStrafeSpeed;
    private float speedMultiplier;

    public float forwardAcceleration;
    public float strafeAcceleration;

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<ModeManager>().GetIFM())
        {
            // Embody entire flock; fly along the xz plane
            // Gather food/resources; grow flock size

            if (Input.GetKey("left shift"))
            {
                speedMultiplier = 1.66f;
            }
            else
            {
                speedMultiplier = 1f;
            }

            activeForwardSpeed = Mathf.Lerp(activeForwardSpeed, Input.GetAxisRaw("Vertical") * forwardSpeed, forwardAcceleration * Time.deltaTime);
            activeStrafeSpeed = Mathf.Lerp(activeStrafeSpeed, Input.GetAxisRaw("Horizontal") * strafeSpeed, strafeAcceleration * Time.deltaTime);

            flock.transform.position += playerPrefab.transform.forward * activeForwardSpeed * speedMultiplier * Time.deltaTime;
            playerPrefab.GetComponent<Rigidbody>().position += playerPrefab.transform.forward * activeForwardSpeed * speedMultiplier * Time.deltaTime;
            flock.transform.position += playerPrefab.transform.right * activeStrafeSpeed * speedMultiplier * Time.deltaTime;
            playerPrefab.GetComponent<Rigidbody>().position += playerPrefab.transform.right * activeStrafeSpeed * speedMultiplier * Time.deltaTime;

            // Updates hunger meter

            if (GetComponent<ModeManager>().hunger < 100 && GetComponent<ModeManager>().hunger != 0)
            {
                GetComponent<ModeManager>().hunger += .005f;
            }
        } else
        {
            activeForwardSpeed = 0f;
            activeStrafeSpeed = 0f;
        }
    }
}
