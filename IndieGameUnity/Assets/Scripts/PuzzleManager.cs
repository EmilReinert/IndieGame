using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    // External components
    public GameObject puzzleObjects;
    public Puzzle[] puzzles;
    public bool ended = false;
    private Level l;
    private int move;
    GameManager GM;
    public bool endcut = false;
    public GameObject positionSphere;

    public void Start()
    {
        GM = GameObject.FindObjectOfType<GameManager>();
        l = GetComponent<Level>();
        ended = false;
    }

    public void StartPuzzles()
    {
        Start();
        if (positionSphere != null)
        {
            GameObject.Find("Player").transform.position = positionSphere.transform.position;
            GameObject.Find("Player").transform.rotation = positionSphere.transform.rotation;
        }
        if(puzzleObjects!=null)
            puzzleObjects.SetActive(true);
        foreach (Puzzle p in puzzles)
        {
            if (p != null && p.gameObject != null)
            {
                p.gameObject.SetActive(true);
                p.enabled = true;
                p.StartPuzzle();
            }
        }
    }
    public void EndPuzzles()
    {
        if (ended) return;

       // if (puzzleObjects != null) {            puzzleObjects.SetActive(false);        }
        foreach (Puzzle p in puzzles)
        {
            if (p != null)
            {
                p.EndPuzzle();
                p.ended = true;
            }
            //  if (p.hideObject)                p.gameObject.SetActive(false);
        }
        ended = true;

        if (l != null) 
        l.EndLevel();

    }
    public void UpdatePuzzles()
    {
        move = 0;
        foreach (Puzzle p in puzzles)
        {

            if (p != null && p.gameObject != null)
            {
                if (p.done && !p.ended) { p.EndPuzzle();
                    l.EndLevel(); return; }

                if (p.faily && !p.ended) { p.faily = false; GM.Cut(); StartPuzzles(); return; }

                if (Input.GetButton("Fire2") || Input.GetButton("Fire1"))
                {
                    // continuous update
                    if (p.contiuous)
                    {
                        if (Input.GetButton("Fire2")) { move = -1; }
                        if (Input.GetButton("Fire1")) { move = 1; }
                    }
                    // singular update
                    else
                    {
                        if (Input.GetButtonDown("Fire2")) { move = -1; }
                        if (Input.GetButtonDown("Fire1")) { move = 1; }
                    }
                }
                
               /*
               if (Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.E))
               {
                   // continuous update
                   if (p.contiuous)
                   {
                       if (Input.GetKey(KeyCode.Q)) { move = -1; }
                       if (Input.GetKey(KeyCode.E)) { move = 1; }
                   }
                   // singular update
                   else
                   {
                       if (Input.GetKeyDown(KeyCode.Q)) { move = -1; }
                       if (Input.GetKeyDown(KeyCode.E)) { move = 1; }
                   }
               }*/
               //input in puzzle
               p.Move(move);
                //puzzle update
                p.UpdatePuzzle();
                GM.UpdateGUI(move);
            }
        }

    }
    

}
