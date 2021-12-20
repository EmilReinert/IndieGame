using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManagerTest : MonoBehaviour
{
    // External components
    public Puzzle[] puzzles;

    public void Start()
    {foreach(Puzzle p in puzzles)p.StartPuzzle();
    }

    public void Update()
    {
        foreach (Puzzle p in puzzles)
        {
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
