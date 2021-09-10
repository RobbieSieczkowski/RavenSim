using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundaryMover : MonoBehaviour
{
    public GameObject playerBird;
    public GameObject playerController;

    void LateUpdate()
    {
        // Follows position of player bird

        transform.position = new Vector3(playerBird.transform.localPosition.x + playerController.transform.localPosition.x, playerBird.transform.localPosition.y + playerController.transform.localPosition.y, playerBird.transform.localPosition.z + playerController.transform.localPosition.z);
    }
}
