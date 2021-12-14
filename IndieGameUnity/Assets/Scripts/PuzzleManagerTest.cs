using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManagerTest : MonoBehaviour
{
    // External components
    public Puzzle[] puzzles;
    public int IDX;

    public void Start()
    {
        if (IDX >= puzzles.Length || puzzles[IDX] == null) return;
        puzzles[IDX].StartPuzzle();
    }

    public void Update()
    {
        if (IDX >= puzzles.Length || puzzles[IDX] == null) return;
        if (Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.E))
        {
            // continuous update
            if (puzzles[IDX].contiuous)
            {
                if (Input.GetKey(KeyCode.Q)) { puzzles[IDX].Move(-1); }
                if (Input.GetKey(KeyCode.E)) { puzzles[IDX].Move(1); }
            }
            // singular update
            else
            {
                if (Input.GetKeyDown(KeyCode.Q)) { puzzles[IDX].Move(-1); }
                if (Input.GetKeyDown(KeyCode.E)) { puzzles[IDX].Move(1); }
            }
        }
        else
            puzzles[IDX].Move(0); //reset

        //puzzle update
        puzzles[IDX].UpdatePuzzle();

    }

}