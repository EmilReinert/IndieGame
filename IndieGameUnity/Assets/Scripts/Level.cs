using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    private PuzzleManager pm;
    private CameraManager cm;

    private GameObject playerCollider;
    private LevelManager levelmanager; // set by puzzlemanager
    bool playing = false;
    bool end = false; // ending puzzle
    // Start is called before the first frame update
    void Start()
    {
        playerCollider = GameObject.Find("Player");
        levelmanager = GameObject.FindObjectOfType<LevelManager>();
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
            StartLevel();
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == playerCollider)
        {
            EndLevel();
        }


    }

    void StartLevel()
    {
        playing = true;
        pm.StartPuzzles();
        levelmanager.inPuzzle = true;
        cm.UpdateCameraSettings();
    }
    void EndLevel()
    {
        playing = false;
        pm.EndPuzzles();
        levelmanager.inPuzzle = false;
        cm.UpdateDefaultSettings();
    }
}
