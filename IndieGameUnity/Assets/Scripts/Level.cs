using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : GamePart
{
    private PuzzleManager pm;
    private CameraManager cm;
    private GameManager GM;

    private GameObject playerCollider;

    public bool playing = false;

    public Puzzle endrequirement;

    // Start is called before the first frame update
    void Start()
    {
        GM = GameObject.FindObjectOfType<GameManager>();
        playerCollider = GameObject.Find("Player");
        //levelmanager = GameObject.FindObjectOfType<LevelManager>();
        pm = GetComponent<PuzzleManager>();
        cm = GetComponent<CameraManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playing)
        {
            pm.UpdatePuzzles();
            cm.RemainCameraSettings();////// remove
        }        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == playerCollider)
            GM.SetLevel(this);
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == playerCollider)
        {
            //EndLevel();
        }


    }

    public void StartLevel()
    {
        playing = true;
        pm.StartPuzzles();
        //levelmanager.inPuzzle = true;
        cm.UpdateCameraSettings();
    }
    public void EndLevel()
    {
        if (endrequirement!=null &&!endrequirement.done) return;

        playing = false;
        pm.EndPuzzles();
        //levelmanager.inPuzzle = false;
        cm.UpdateDefaultSettings();
    }
}
