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
        EndPuzzles();
    }

    public void StartPuzzles()
    {
        if(puzzleObjects!=null)
            puzzleObjects.SetActive(true);
        foreach (Puzzle p in puzzles)
        {
            p.gameObject.SetActive(true);
            p.StartPuzzle();
        }
    }
    public void EndPuzzles()
    {

        if (puzzleObjects != null)
            puzzleObjects.SetActive(false);
        foreach (Puzzle p in puzzles)
        {
            p.EndPuzzle();
            p.gameObject.SetActive(false);
        }
    }
    public void UpdatePuzzles()
    {
        foreach (Puzzle p in puzzles)
        {
            if (p.done) { p.EndPuzzle();return; }

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
