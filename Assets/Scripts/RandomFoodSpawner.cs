using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomFoodSpawner : MonoBehaviour
{
    private Object[] foodArray;
    private GameObject[] activeFoodObjects;
    public float radius = 250f;
    private int count;

    void OnEnable()
    {
        // Create foodArray of 10 existing food prefabs
        foodArray = Resources.LoadAll<Object>("Food Prefabs");

        // Assign "count" based on harmony level
        count = (int)(transform.parent.GetComponent<ModeManager>().harmony);

        // Instantiate "count" number of randomized food gameobjects randomly within radius
        activeFoodObjects = new GameObject[count];
        for (int i = 0; i < count; i++)
        {
            Vector3 randomPos = Random.insideUnitCircle * radius;
            randomPos = new Vector3(randomPos.x, 35f, randomPos.y);

            activeFoodObjects[i] = (GameObject) Instantiate (foodArray[Random.Range(0,9)], randomPos, Quaternion.identity);
            activeFoodObjects[i].transform.Rotate(-90f, 0f, 0f, Space.Self);
        }
    }

    void OnDisable()
    {
        // Destroy all foodobjects
        for (int i = 0; i < count; i++)
        {
            Destroy(activeFoodObjects[i]);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        Gizmos.DrawWireSphere(this.transform.position, radius);
    }
}
