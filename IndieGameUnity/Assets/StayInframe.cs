using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StayInframe : MonoBehaviour
{
    Camera c;
    Vector3 startLoc;
    bool modified;
    public float h;
    public float w;

    Vector3 startscale;

    // Start is called before the first frame update
    void Start()
    {
        h = 3f;
        w = 10f;
        c = GameObject.Find("Main Camera").GetComponentInChildren<Camera>() ;
        startLoc = transform.localPosition;
        modified = false;
        startscale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
            // curretn pos inscreen
        Vector3 current = transform.parent.TransformPoint(startLoc);
        // float h =Mathf.Abs( c.WorldToScreenPoint(current - new Vector3(0, 1, 0) * mh).y);
        // float w = Mathf.Abs(c.WorldToScreenPoint(current - new Vector3(1, 0, 0) * mw).x);

        if (c.WorldToScreenPoint(current).x - w < Screen.width && c.WorldToScreenPoint(current).x + w > 0
                && c.WorldToScreenPoint(current).y - h < Screen.height && c.WorldToScreenPoint(current).y + h > 0)
        {

            transform.position = current;
        }
        else
        {

            if (c.WorldToScreenPoint(current).x - w > Screen.width)// || c.WorldToScreenPoint(transform.position).y > Screen.height / 2)
            {
                Vector3 start = current;
                start = c.ScreenToWorldPoint(new Vector3(Screen.width, c.WorldToScreenPoint(current).y, c.WorldToScreenPoint(current).z));
                transform.position = new Vector3(start.x - w, transform.position.y, transform.position.z);
            }

            if (c.WorldToScreenPoint(current).x + w < 0)// || c.WorldToScreenPoint(transform.position).y > Screen.height / 2)
            {
                Vector3 start = current;
                start = c.ScreenToWorldPoint(new Vector3(0, c.WorldToScreenPoint(current).y, c.WorldToScreenPoint(current).z));
                transform.position = new Vector3(start.x + w, transform.position.y, transform.position.z);
            }


            if (c.WorldToScreenPoint(current).y - h > Screen.height)// || c.WorldToScreenPoint(transform.position).y > Screen.height / 2)
            {
                Vector3 start = current;
                start = c.ScreenToWorldPoint(new Vector3(c.WorldToScreenPoint(current).x, Screen.height, c.WorldToScreenPoint(current).z));
                transform.position = new Vector3(transform.position.x, start.y - h, transform.position.z);
            }
            if (c.WorldToScreenPoint(current).y + h < 0)// || c.WorldToScreenPoint(transform.position).y > Screen.height / 2)
            {
                Vector3 start = current;
                start = c.ScreenToWorldPoint(new Vector3(c.WorldToScreenPoint(current).x, 0, c.WorldToScreenPoint(current).z));
                transform.position = new Vector3(transform.position.x, start.y + h, transform.position.z);
            }
        }
        //transform.position = new Vector3(transform.position.x, transform.position.y, c.WorldToScreenPoint(new Vector3 (0,0,0)).z);

        float scale = Vector3.Distance(c.transform.position, transform.position);
        transform.localScale = startscale *( scale/20);
        if (transform.localScale.magnitude < startscale.magnitude)
            transform.localScale = startscale;
    }
}
