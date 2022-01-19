using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    GameObject player;
    public Level currentLevel;


    PlayerStickyCamera stickyCam;
    private CameraManager cm;

    private void Start()
    {
        player = GameObject.Find("Player");
        GameObject mainCam = GameObject.Find("Main Camera");
        stickyCam = mainCam.GetComponent<PlayerStickyCamera>();
        cm = GetComponent<CameraManager>();
    }
    private void Update()
    {
        if (currentLevel == null)
        {
            cm.RemainCameraSettings();
        }

    }
    public void SetLevel(Level l)
    {
        if (l == currentLevel) return;
        if (currentLevel != null) { currentLevel.EndLevel();
            if (currentLevel.playing) return;
        }
        currentLevel = l;
        currentLevel.StartLevel();
    }

}
