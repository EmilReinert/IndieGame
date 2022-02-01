using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StayInframe : MonoBehaviour
{
    Camera c;
    Vector3 startLoc;
    bool modified;
    // Start is called before the first frame update
    void Start()
    {
        c = GameObject.Find("Main Camera").GetComponentInChildren<Camera>() ;
        startLoc = transform.localPosition;
        modified = false;
    }

    // Update is called once per frame
    void Update()
    {
            // curretn pos inscreen
            Vector3 current = transform.parent.TransformPoint(startLoc);
        if (c.WorldToScreenPoint(current).x < Screen.width && c.WorldToScreenPoint(current).x > 0
                && c.WorldToScreenPoint(current).y < Screen.height && c.WorldToScreenPoint(current).y > 0)
        { 

                transform.position = current;
            return;
        }


            if (c.WorldToScreenPoint(current).x > Screen.width)// || c.WorldToScreenPoint(transform.position).y > Screen.height / 2)
        {
            Vector3 start = current;
            start = c.ScreenToWorldPoint(new Vector3(Screen.width, c.WorldToScreenPoint(current).y, c.WorldToScreenPoint(current).z));
            transform.position = new Vector3(start.x, current.y, current.z);
        }

        if (c.WorldToScreenPoint(current).x < 0)// || c.WorldToScreenPoint(transform.position).y > Screen.height / 2)
        {
            Vector3 start = current;
            start = c.ScreenToWorldPoint(new Vector3(0, c.WorldToScreenPoint(current).y, c.WorldToScreenPoint(current).z));
            transform.position = new Vector3(start.x, current.y, current.z);
        }


        if (c.WorldToScreenPoint(current).y > Screen.height)// || c.WorldToScreenPoint(transform.position).y > Screen.height / 2)
        {
            Vector3 start = current;
            start = c.ScreenToWorldPoint(new Vector3(c.WorldToScreenPoint(current).x, Screen.height, c.WorldToScreenPoint(current).z));
            transform.position = new Vector3(current.x, start.y, current.z);
        }
        if (c.WorldToScreenPoint(current).y < 0)// || c.WorldToScreenPoint(transform.position).y > Screen.height / 2)
        {
            Vector3 start = current;
            start = c.ScreenToWorldPoint(new Vector3(c.WorldToScreenPoint(current).x, 0, c.WorldToScreenPoint(current).z));
            transform.position = new Vector3(current.x, start.y, current.z);
        }
    }
}
