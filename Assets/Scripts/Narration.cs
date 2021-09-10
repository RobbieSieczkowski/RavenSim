using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Narration : MonoBehaviour
{
    public GameObject gameManager;

    public AudioSource audio1;
    public AudioSource audio2;
    public AudioSource audio3;
    public AudioSource audio4;
    public AudioSource audio5;
    public AudioSource audio6;
    public AudioSource audio7;
    public AudioSource audio8;
    public AudioSource audio9;
    public AudioSource audio10;
    public AudioSource audio11;

    private bool flag1;
    private bool flag2;
    private bool flag3;
    private bool flag4;
    private bool flag5;
    private bool flag6;
    private bool flag7;
    private bool flag8;
    private bool flag9;
    private bool flag10;
    private bool flag11;

    private float timeTracker;

    // Start is called before the first frame update
    void Start()
    {
        flag1 = true;
        flag2 = true;
        flag3 = true;
        flag4 = true;
        flag5 = true;
        flag6 = true;
        flag7 = true;
        flag8 = true;
        flag9 = true;
        flag10 = true;
        flag11 = true;
    }

    // Update is called once per frame
    void Update()
    {
        timeTracker += Time.deltaTime;

        Debug.Log(timeTracker);

        if (flag1)
        {
            flag1 = false;
            audio1.Play();
        }
        if (timeTracker >= 3.5f && flag2)
        {
            flag2 = false;
            audio2.Play();
        }
        if (timeTracker >= 14.5f && flag3)
        {
            flag3 = false;
            audio3.Play();
        }
        if (timeTracker >= 29f && flag4)
        {
            flag4 = false;
            audio4.Play();
        }
        if (timeTracker >= 40f && flag5)
        {
            flag5 = false;
            audio5.Play();
        }
        if (timeTracker >= 56f && flag6)
        {
            flag6 = false;
            audio6.Play();
        }
        if (timeTracker >= 76.5f && flag7)
        {
            flag7 = false;
            audio7.Play();
        }
        if (timeTracker >= 91f && flag8)
        {
            flag8 = false;
            audio8.Play();
        }
        if (timeTracker >= 105f && flag9)
        {
            flag9 = false;
            audio9.Play();
        }
        if (timeTracker >= 109f && flag10)
        {
            flag10 = false;
            audio10.Play();
        }
        if (timeTracker >= 122f && flag11)
        {
            flag11 = false;
            audio11.Play();
        }
    }
}
