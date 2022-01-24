using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    GameObject player;
    public Level currentLevel;

    PlayerStickyCamera stickyCam;
    private CameraManager cm;

    //UI
    public GameObject leftButton;
    public GameObject rightButton;
    public Material defaultMaterial;
    public Material highlightMaterial;
    private bool moving;

    private void Start()
    {
        player = GameObject.Find("Player");
        GameObject mainCam = GameObject.Find("Main Camera");
        stickyCam = mainCam.GetComponent<PlayerStickyCamera>();
        cm = GetComponent<CameraManager>();
        ResetGUI();
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

    public void UpdateGUI(int move) // called in Puzzlemanager on state change
    {
        if (currentLevel == null || currentLevel.pm == null) { ResetGUI(); return; }

        MoveIn();
        leftButton.GetComponent<Image>().material = defaultMaterial;
        rightButton.GetComponent<Image>().material = defaultMaterial;

        if (move == -1) {
            leftButton.GetComponent<Image>().material = highlightMaterial;
            rightButton.GetComponent<Image>().material = defaultMaterial;
        }

        if (move == 1)
        {
            leftButton.GetComponent<Image>().material = defaultMaterial;
            rightButton.GetComponent<Image>().material = highlightMaterial;
        }
    }
    void ResetGUI()
    {
        moving = false;
        
        leftButton.GetComponent<Image>().material = defaultMaterial;
        rightButton.GetComponent<Image>().material = defaultMaterial;
        MoveIn(false);

    }

    void MoveIn(bool b = true)
    {
        StopAllCoroutines();
        //POS Y 75 in // -25 out
        float goalpos;
        if (b)
        {
            //move in
            goalpos = 40
                ;
        }
        else
        {
            //move out
            goalpos = -25;
        }
        StartCoroutine(MoveButtons(goalpos));
    }

    IEnumerator MoveButtons(float posY)
    {
        Vector3 startL = leftButton.transform.position;
        Vector3 startR = rightButton.transform.position;
        float c = 0; // current y difference
        if (startL.y != posY)
        {
            float diff = posY - startL.y;
            moving = true;
            int steps = 20; // animation steps;
            float increment = diff / steps;
            Vector3 eulerRot = new Vector3(0, 0, 1);
            while (Mathf.Abs(c) <= Mathf.Abs(diff))
            {
                c += Time.deltaTime * (diff / steps) * 40;
                leftButton.transform.position += new Vector3(0, 1, 0) * c;
                rightButton.transform.position += new Vector3(0, 1, 0) * c;
                yield return new WaitForEndOfFrame();
            }
            leftButton.transform.position =startL + new Vector3(0, 1, 0) * diff;
            rightButton.transform.position = startR + new Vector3(0, 1, 0) * diff;
            moving = false;
        }
    }

}
