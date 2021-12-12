using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleNode : Puzzle
{
    public Puzzle[] subpuzzles;

    public override void EndPuzzle()
    {
        foreach (Puzzle p in subpuzzles)
            p.EndPuzzle();
    }

    public override void Move(int i)
    {
        foreach (Puzzle p in subpuzzles)
            p.Move(i);
    }

    public override void StartPuzzle()
    {
        foreach (Puzzle p in subpuzzles)
            p.StartPuzzle();
    }

    public override void UpdatePuzzle()
    {
        foreach (Puzzle p in subpuzzles)
            p.UpdatePuzzle();
    }
    
}
