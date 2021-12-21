using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    // External components
    public GameObject puzzleObjects;
    public Puzzle[] puzzles;

    public void Start()
    {
    }

    public void StartPuzzles()
    {
        if(puzzleObjects!=null)
            puzzleObjects.SetActive(true);
        foreach (Puzzle p in puzzles)
        {
            p.gameObject.SetActive(true);
            p.enabled = true;
            p.StartPuzzle();
        }
    }
    public void EndPuzzles()
    {

        if (puzzleObjects != null) {
            //puzzleObjects.SetActive(false);
        }
        foreach (Puzzle p in puzzles)
        {
            p.EndPuzzle();
            if (p.hideObject)
                p.gameObject.SetActive(false);
        }
    }
    public void UpdatePuzzles()
    {
        foreach (Puzzle p in puzzles)
        {
            if (p.done && !p.ended) { p.EndPuzzle(); p.ended = true; return;}

            if (Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.E))
            {
                // continuous update
                if (p.contiuous)
                {
                    if (Input.GetKey(KeyCode.Q)) { p.Move(-1); }
                    if (Input.GetKey(KeyCode.E)) { p.Move(1); }
                }
                // singular update
                else
                {
                    if (Input.GetKeyDown(KeyCode.Q)) { p.Move(-1); }
                    if (Input.GetKeyDown(KeyCode.E)) { p.Move(1); }
                }
            }
            else
                p.Move(0); //reset

            //puzzle update
            p.UpdatePuzzle();
        }

    }
    

}
