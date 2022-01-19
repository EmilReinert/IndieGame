using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{   
    public enum CameraPos {Front, Back, Left, Right, TopFront };
    public GameObject lookAt;
    
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
        if (distance == 0) distance = 20;
        stickyCam.transition = true;
        stickyCam.enabled = true;
        stickyCam.Enable(true);

    }


    public void UpdateCameraSettings()
    {
        stickyCam.transition = true ;
        stickyCam.offset = GetCameraOff();
        stickyCam.fov = fov;

        if (copyCam != null)
        {
            stickyCam.Enable(false);
        }
        if (lookAt != null) stickyCam.lookAt = lookAt;
        else { stickyCam.lookAt = player; stickyCam.on = true; }


    }
    public void RemainCameraSettings()
    {
        stickyCam.offset = GetCameraOff();
        stickyCam.fov = fov;

    }

    public void UpdateDefaultSettings()
    {
        stickyCam.ResetOffset();
        stickyCam.transition = true;
        stickyCam.enabled = true;
        stickyCam.Enable(true);
    }
    Vector3 GetCameraOff()
    {
        CameraPosition off;
        if (copyCam != null)
        {
            return copyCam.transform.position;
        }
        switch (cameraPosition)
        {
            case CameraPos.Front: off = new CameraPosition(0, tilt, distance); break;
            case CameraPos.Back: off = new CameraPosition(180, tilt, distance); break;
            case CameraPos.Left: off = new CameraPosition(90, tilt, distance); break;
            case CameraPos.Right: off = new CameraPosition(270, tilt, distance); break;
            case CameraPos.TopFront: off = new CameraPosition(0, 70, distance); break;
            default: off = new CameraPosition(); break;
        }
        return
                    player.transform.position - off.GetOffsetAngle() - new Vector3(0, 2, 0);
    }
}
