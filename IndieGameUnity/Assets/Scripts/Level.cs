using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : GamePart
{
    [HideInInspector]
    public PuzzleManager pm;
    private CameraManager cm;
    private GameManager GM;

    private GameObject playerCollider;

    public bool playing = false;

    public Level next;
    public Puzzle endrequirement;

    private bool done;


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
            if (pm != null)
            {
                pm.UpdatePuzzles();
            }
            cm.RemainCameraSettings();////// remove
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == playerCollider)
        {
            if (!done)
                GM.SetLevel(this);
        }
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
        done = false;
        playing = true;
        if (pm != null)
            pm.StartPuzzles();
        //levelmanager.inPuzzle = true;
        cm.UpdateCameraSettings();
    }
    public void EndLevel()
    {
        if (!playing) return;
        if (endrequirement != null && !endrequirement.done) return;

        playing = false;
        if (pm != null)
            pm.EndPuzzles();
        //levelmanager.inPuzzle = false;

        if (next != null)
            GM.SetLevel(next);
        else cm.UpdateDefaultSettings();
        done = true;
        if(endrequirement != null && endrequirement.doneAnimation)  GameObject.Find("Player").GetComponentInChildren<Emotions>().PlayHappy();
    }
}
