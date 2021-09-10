using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public GameObject gameManager;

    private bool flag;

    private void Awake()
    {
        gameManager = GameObject.Find("GameManager");
    }

    // Detects when player collides w/ another gameobject w an "Is Trigger" = true collider

    void OnTriggerEnter(Collider collisionInfo)
    {
        // Detects when flock touches food
        // Satisfies hunger incrementally

        if (collisionInfo.tag.Equals("Food"))
        {
            Debug.Log("Yum, food eaten!");
            if (gameManager.GetComponent<ModeManager>().hunger >= 3f)
            {
                gameManager.GetComponent<ModeManager>().hunger -= 3f;
            }
            else if (gameManager.GetComponent<ModeManager>().hunger < 3f)
            {
                gameManager.GetComponent<ModeManager>().hunger = 0f;
            }
            Destroy(collisionInfo.gameObject);
        }

        // Detects when a raven touches fight center
        // Increases stress levels

        if (gameManager.GetComponent<ModeManager>().GetIDCM() && collisionInfo.tag.Equals("Fight"))
        {
            Debug.Log("Another raven enters the fight!");
            if (gameManager.GetComponent<ModeManager>().stress <= 99.9f)
            {
                gameManager.GetComponent<ModeManager>().stress += 0.1f;
            }
            else if (gameManager.GetComponent<ModeManager>().stress > 99.9f)
            {
                gameManager.GetComponent<ModeManager>().stress = 100f;
            }
        }

        if (gameManager.GetComponent<ModeManager>().GetIDCM() && collisionInfo.tag.Equals("PusherAwayer"))
        {
            if (gameManager.GetComponent<ModeManager>().seekWeight.value > 0f)
            {
                Debug.Log("You're slowing them down... keep holding the line!");
                gameManager.GetComponent<ModeManager>().seekWeight.value -= 0.1f;
                Debug.Log(gameManager.GetComponent<ModeManager>().seekWeight.value);
                flag = true;
            }
            else if (flag && gameManager.GetComponent<ModeManager>().seekWeight.value <= 0f)
            {
                flag = false;
                Debug.Log("The damage is controlled... for now!");
            }
        }
    }
}
