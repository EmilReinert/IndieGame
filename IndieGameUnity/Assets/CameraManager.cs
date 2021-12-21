using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{   
    public enum CameraPos {Front, Back, Left, Right, TopFront };

    public bool playerAttached;
    
    public CameraPos cameraPosition;
    public float distance;
    public float fov;
    public int tilt;

    public GameObject copyCam;
    private GameObject mainCam;
    private PlayerStickyCamera stickyCam;

    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        mainCam = GameObject.Find("Main Camera");
        stickyCam = mainCam.GetComponent<PlayerStickyCamera>();
        if (fov == 0) fov = 60;
        if (tilt == 0) tilt = 20;
        if (distance == 0) distance = 15;
        UpdateDefaultSettings();
    }


    public void UpdateCameraSettings()
    {
        stickyCam.enabled = true;
        stickyCam.offset = GetCameraOff();
        stickyCam.fov = fov;

    }

    public void UpdateDefaultSettings()
    {
        stickyCam.enabled = true;
        stickyCam.ResetOffset();
    }
    Vector3 GetCameraOff()
    {
        CameraPosition off;
        switch (cameraPosition)
        {
            case CameraPos.Front: off = new CameraPosition(0, tilt, distance); break;
            case CameraPos.Back: off = new CameraPosition(180, tilt, distance); break;
            case CameraPos.Left: off = new CameraPosition(90, tilt, distance); break;
            case CameraPos.Right: off = new CameraPosition(270, tilt, distance); break;
            case CameraPos.TopFront: off = new CameraPosition(0, 70, distance); break;
            default: off = new CameraPosition(); break;
        }
        return off.GetOffsetAngle();
    }
}
