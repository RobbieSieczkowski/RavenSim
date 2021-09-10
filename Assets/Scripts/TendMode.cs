using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TendMode : MonoBehaviour
{
    public Camera mainCamera;
    public GameObject playerPrefab;

    public float forwardSpeed;
    public float strafeSpeed;
    private float activeForwardSpeed;
    private float activeStrafeSpeed;
    private float speedMultiplier;

    public float forwardAcceleration;
    public float strafeAcceleration;

    public float lookRotateSpeed;
    private Vector2 lookInput;
    private Vector2 screenCenter;
    private Vector2 mouseDistance;

    private float rollInput;
    public float rollSpeed;
    public float rollAcceleration;

    private static GameObject testRaven;
    private Color defaultColor;

    private float time;

    // Start is called before the first frame update
    void Start()
    {
        screenCenter.x = Screen.width * .5f;
        screenCenter.y = Screen.height * .5f;

        var ravens = Resources.FindObjectsOfTypeAll<GameObject>().Where(obj => obj.name == "Bird Pref B(Clone)");
        testRaven = ravens.ElementAt(Random.Range(0,ravens.Count()));

        defaultColor = testRaven.GetComponent<MeshRenderer>().material.color;
        /*Color c = testRaven.GetComponent<MeshRenderer>().material.GetColor("_EmissionColor");
        c *= 10.0f;
        testRaven.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor",c);*/
    }
    
    void FixedUpdate()
    {
        if (GetComponent<ModeManager>().GetITM())
        {
            // Embody individual raven; fly freely along all axes w/ front of raven aimed where cursor moves
            // Search for raven that lost the fight; will have a red indicator to signify high arousal
            // Groom/comfort raven to bring stress levels down

            if (Input.GetKey("left shift"))
            {
                speedMultiplier = 1.66f;
            }
            else
            {
                speedMultiplier = 1f;
            }

            lookInput.x = Input.mousePosition.x;
            lookInput.y = Input.mousePosition.y;

            mouseDistance.x = (lookInput.x - screenCenter.x) / screenCenter.y;
            mouseDistance.y = (lookInput.y - screenCenter.y) / screenCenter.y;

            mouseDistance = Vector2.ClampMagnitude(mouseDistance, 1f);

            rollInput = Mathf.Lerp(rollInput, Input.GetAxisRaw("Roll"), rollAcceleration * Time.deltaTime);
            
            playerPrefab.transform.Rotate(-mouseDistance.y * lookRotateSpeed * Time.deltaTime, mouseDistance.x * lookRotateSpeed * Time.deltaTime, rollInput * rollSpeed * Time.deltaTime, Space.Self);

            activeForwardSpeed = Mathf.Lerp(activeForwardSpeed, Input.GetAxisRaw("Vertical") * forwardSpeed * speedMultiplier, forwardAcceleration * Time.deltaTime);
            activeStrafeSpeed = Mathf.Lerp(activeStrafeSpeed, Input.GetAxisRaw("Horizontal") * strafeSpeed * speedMultiplier, strafeAcceleration * Time.deltaTime);

            playerPrefab.GetComponent<Rigidbody>().position += playerPrefab.transform.forward * activeForwardSpeed * Time.deltaTime;
            playerPrefab.GetComponent<Rigidbody>().position += playerPrefab.transform.right * activeStrafeSpeed * Time.deltaTime;

            // Janky methodology for fixing movement "pushback"

            playerPrefab.GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 0f);
            
            // Updates hunger meter

            if (GetComponent<ModeManager>().hunger < 100 && GetComponent<ModeManager>().hunger != 0)
            {
                GetComponent<ModeManager>().hunger += .005f;
            }

            // Updates stress meter

            if (GetComponent<ModeManager>().stress < 100 && GetComponent<ModeManager>().stress != 0)
            {
                GetComponent<ModeManager>().stress += .005f;
            }
        }
    }
    void Update(){
    	if(GetComponent<ModeManager>().GetITM()){
    		time += Time.deltaTime*1f;
    		if(time > 0.5){
    			if(testRaven.GetComponent<MeshRenderer>().material.color == defaultColor){
    				testRaven.GetComponent<MeshRenderer>().material.color = Color.red;
    			}else{
    				testRaven.GetComponent<MeshRenderer>().material.color = defaultColor;
    			}
    			time = 0;
    		}

    		testRaven.GetComponent<Rigidbody>().velocity = new Vector3(0.1f,0,0.1f);
    	if(Vector3.Distance(testRaven.transform.position, playerPrefab.GetComponent<Rigidbody>().position) < 15 && Input.GetKey("space")){
    		playerPrefab.GetComponent<MeshRenderer>().material.color = defaultColor;
    		testRaven.GetComponent<MeshRenderer>().material.color = defaultColor;
            
            if (GetComponent<ModeManager>().stress >= 20f)
            {
                GetComponent<ModeManager>().stress -= 20f;
            } else
            {
                GetComponent<ModeManager>().stress = 0f;
            }

                var ravens = Resources.FindObjectsOfTypeAll<GameObject>().Where(obj => obj.name == "Bird Pref B(Clone)");
            testRaven = ravens.ElementAt(Random.Range(0,ravens.Count()));

    		ModeManager.NextMode();
    	}

    	Vector3 screenPoint = mainCamera.WorldToViewportPoint(testRaven.transform.position);
 		bool onScreen = screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;

    	if(onScreen){
    		//playerPrefab.GetComponent<MeshRenderer>().material.color = Color.cyan;
    	}else{
    		playerPrefab.GetComponent<MeshRenderer>().material.color = defaultColor;
    	}
    }else{
    	playerPrefab.GetComponent<MeshRenderer>().material.color = defaultColor;
    	testRaven.GetComponent<MeshRenderer>().material.color = defaultColor;
    }
    }
    public static GameObject getRaven(){
    	return testRaven;
    }
}
