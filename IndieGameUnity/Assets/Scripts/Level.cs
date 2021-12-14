using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public PuzzleManager pm;
    public GameObject playerCollider;
    bool playing = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playing)
            pm.UpdatePuzzles();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == playerCollider)
            StartLevel();
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == playerCollider)
            EndLevel();


    }

    void StartLevel()
    {
        playing = true;
        pm.StartPuzzles();

    }
    void EndLevel()
    {
        playing = false;
        pm.EndPuzzles();
    }
}
