using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeManager : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject flock;
    public GameObject modeCanvas;

    private bool isIntro;
    private bool isTendMode;
    private bool isFlockMode;
    private bool isDmgControlMode;

    private float waitTime;
    public float minWaitTime;
    public float maxWaitTime;
    private static bool isWaiting;
    private static bool doneWaiting;
    private static bool successfulTend;

    [HideInInspector]
    public float hunger;
    [HideInInspector]
    public float stress;
    [HideInInspector]
    public float harmony;

    public ProgressBar HungerBar;
    public ProgressBar StressBar;
    public ProgressBar HarmonyBar;

    public FloatContainer seekWeight;
    public FloatContainer flockWeight;

    public GameObject seekObject;

    Coroutine co;

    // Start is called before the first frame update
    void Start()
    {
        isIntro = true;
        isTendMode = true;
        isFlockMode = false;
        isDmgControlMode = false;
        isWaiting = false;
        doneWaiting = false;
        successfulTend = false;

        hunger = 50f;
        stress = 50f;
        harmony = 50f;

        seekWeight.value = 0.2f;
        flockWeight.value = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        // Circulates between three modes over semi-random intervals of time

        if (isTendMode) // In Tend Mode
        {
            if (!isWaiting && !doneWaiting)
            {
                seekWeight.value = 0f;
                flockWeight.value = 2f;
                co = StartCoroutine(Waiter());
            }
            if (successfulTend || doneWaiting) // Switch to Flock Mode
            {
                successfulTend = false;
                isWaiting = false;
                doneWaiting = false;
                StopCoroutine(co); // Stop the coroutine

                isDmgControlMode = false;
                isTendMode = false;
                isFlockMode = true;
                MoveToDefault();

                modeCanvas.transform.GetChild(0).gameObject.SetActive(false);
                modeCanvas.transform.GetChild(1).gameObject.SetActive(false);
                modeCanvas.transform.GetChild(2).gameObject.SetActive(true);
                modeCanvas.transform.GetChild(3).gameObject.SetActive(true);

                this.transform.Find("FoodSpawner").gameObject.GetComponent<RandomFoodSpawner>().enabled = true;
            }
        } else if (isFlockMode) // In Flock Mode
        {
            isIntro = false;
            if (!isWaiting && !doneWaiting)
            {
                seekWeight.value = 0.1f;
                flockWeight.value = 0.7f;
                co = StartCoroutine(Waiter());
            }
            if (hunger == 0f || doneWaiting) // Switch to Damage Control Mode
            {
                isWaiting = false;
                doneWaiting = false;
                StopCoroutine(co); // Stop the coroutine

                isFlockMode = false;
                isDmgControlMode = true;

                modeCanvas.transform.GetChild(2).gameObject.SetActive(false);
                modeCanvas.transform.GetChild(3).gameObject.SetActive(false);
                modeCanvas.transform.GetChild(4).gameObject.SetActive(true);
                modeCanvas.transform.GetChild(5).gameObject.SetActive(true);

                this.transform.Find("FoodSpawner").gameObject.GetComponent<RandomFoodSpawner>().enabled = false;

                playerPrefab.transform.localPosition = new Vector3(playerPrefab.transform.localPosition.x, playerPrefab.transform.localPosition.y, playerPrefab.transform.localPosition.z + 50f);
                seekObject.transform.localPosition = new Vector3(seekObject.transform.localPosition.x, seekObject.transform.localPosition.y, -seekObject.transform.localPosition.z);
            }
        } else if (isDmgControlMode) // In Damage Control Mode
        {
            if (!isWaiting && !doneWaiting)
            {
                seekWeight.value = 0.1f;
                flockWeight.value = 0.9f;
                co = StartCoroutine(Waiter());
            }
            if (stress == 0f || doneWaiting) // Switch to Tend Mode
            {
                isWaiting = false;
                doneWaiting = false;
                StopCoroutine(co); // Stop the coroutine

                isDmgControlMode = false;
                isTendMode = true;
                doneWaiting = false;

                modeCanvas.transform.GetChild(4).gameObject.SetActive(false);
                modeCanvas.transform.GetChild(5).gameObject.SetActive(false);
                modeCanvas.transform.GetChild(0).gameObject.SetActive(true);
                modeCanvas.transform.GetChild(1).gameObject.SetActive(true);

                seekObject.transform.localPosition = new Vector3(seekObject.transform.localPosition.x, seekObject.transform.localPosition.y, -seekObject.transform.localPosition.z);
            }
        }

        // Updates harmony meter
        
        harmony = 100f - (hunger / 2f) - (stress / 2f);

        HungerBar.BarValue = hunger;
        StressBar.BarValue = stress;
        HarmonyBar.BarValue = harmony;
    }

    IEnumerator Waiter()
    {
        isWaiting = true;

        waitTime = 0f;
        if (isIntro)
        {
            waitTime = 130f;
        } else if (isTendMode)
        {
            waitTime = 45f;
        } else if (isFlockMode && hunger != 0f)
        {
            waitTime = Random.Range(minWaitTime, maxWaitTime) * (2f - (2f * stress / 100f)); // waitTime indirectly relates to stress levels (flock mode will end and fights will spawn more quickly if stress levels are higher)
        } else if (isDmgControlMode && stress != 0f)
        {
            waitTime = Random.Range(minWaitTime, maxWaitTime) * (2f * hunger / 100f); // waitTime directly relates to hunger levels (birds will fight longer if hunger levels are higher)
        }

        Debug.Log("Waiting for " + waitTime + " seconds...");
        yield return new WaitForSeconds(waitTime);
        Debug.Log("Waited for " + waitTime + " seconds.");
        isWaiting = false;
        doneWaiting = true;
    }

    // Rotates player raven back to default z-axis rotation
    // Teleports player raven to center of first three ravens in flock, so that player can see flock

    void MoveToDefault()
    {
        playerPrefab.transform.rotation = new Quaternion(0f, playerPrefab.transform.rotation.y, 0f, 0f);
        playerPrefab.transform.position = new Vector3((flock.transform.GetChild(8).position.x + flock.transform.GetChild(9).position.x + flock.transform.GetChild(10).position.x) / 3, 35f, (flock.transform.GetChild(8).position.z + flock.transform.GetChild(9).position.z + flock.transform.GetChild(10).position.z) / 3);
    }

    public bool GetITM()
    {
        return isTendMode;
    }

    public bool GetIFM()
    {
        return isFlockMode;
    }
    
    public bool GetIDCM()
    {
        return isDmgControlMode;
    }

    public static void NextMode(){
        successfulTend = true;
    }
}
