using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IndicateRaven : MonoBehaviour
{
    // Start is called before the first frame update
    public Camera mainCamera;
    private GameObject raven;
    private RectTransform canvasrect;

    void Start()
    {
        gameObject.GetComponent<Image>().enabled = true;
        canvasrect = GameObject.Find("ModeCanvas").GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update(){
    	raven = TendMode.getRaven();
    	Vector3 pos = mainCamera.WorldToScreenPoint(raven.transform.position);
        if (pos.z >= 0f & pos.x <= canvasrect.rect.width * canvasrect.localScale.x
         & pos.y <= canvasrect.rect.height * canvasrect.localScale.x & pos.x >= 0f & pos.y >= 0f)
        {
            pos.z = 0f;
            offsight(false, pos);
        }
        else if (pos.z >= 0f)
        {
            pos = offrange(pos);

            offsight(true, pos);
        }
        else
        {
            pos *= -1f;
            pos = offrange(pos);
            offsight(true, pos);

        }

        transform.position = pos;
    }
    private void offsight(bool b, Vector3 pos){
    	if (b)
        {
            if (!gameObject.GetComponent<Image>().enabled) gameObject.GetComponent<Image>().enabled = true;

            Vector3 center = new Vector3(canvasrect.rect.width / 2f, canvasrect.rect.height / 2f, 0f) * canvasrect.localScale.x;
        	float angle = Vector3.SignedAngle(Vector3.up, pos - center, Vector3.forward);
            transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        }
        else
        {
            if (gameObject.GetComponent<Image>().enabled) gameObject.GetComponent<Image>().enabled = false;
        }
    }
    private Vector3 offrange(Vector3 pos){
    	pos.z = 0f;

        Vector3 center = new Vector3(canvasrect.rect.width / 2f, canvasrect.rect.height / 2f, 0f) * canvasrect.localScale.x;
        pos -= center;

        float divX = (canvasrect.rect.width / 2f - 20f) / Mathf.Abs(pos.x);
        float divY = (canvasrect.rect.height / 2f - 20f) / Mathf.Abs(pos.y);

        
        if (divX < divY)
        {
            float angle = Vector3.SignedAngle(Vector3.right, pos, Vector3.forward);
            pos.x = Mathf.Sign(pos.x) * (canvasrect.rect.width * 0.5f - 20f) * canvasrect.localScale.x;
            pos.y = Mathf.Tan(Mathf.Deg2Rad * angle) * pos.x;
        }

        else
        {
            float angle = Vector3.SignedAngle(Vector3.up, pos, Vector3.forward);

            pos.y = Mathf.Sign(pos.y) * (canvasrect.rect.height / 2f - 20f) * canvasrect.localScale.y;
            pos.x = -Mathf.Tan(Mathf.Deg2Rad * angle) * pos.y;
        }

        pos += center;
        return pos;
    }
}
