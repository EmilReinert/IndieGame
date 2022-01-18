using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    public GameObject cam;

    // Start is called before the first frame update
    void Start()
    {

        if (cam == null)
            cam = GameObject.Find("Main Camera");
    }

    // Update is called once per frame
    void Update()
    {

        // update view direction
        transform.LookAt(cam.transform.position);
        transform.rotation *= Quaternion.Euler(0, 180, 0);
    }
}
