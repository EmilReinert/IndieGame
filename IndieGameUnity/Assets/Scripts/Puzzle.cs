using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Puzzle: MonoBehaviour
{
    [HideInInspector] // Hides var below
    public bool contiuous = false;
    public bool done = false; // task done
    public bool ended = false; // puzzle ended by puzzlemanager
    public bool hideObject = true;
    [HideInInspector]
    public bool doneAnimation = true; // happy animation when done

    public abstract void StartPuzzle();

    public abstract void EndPuzzle(); // called in puzzlemanager aftre Done = true

    public abstract void Move(int i); // -1 left 0 default 1 right 
    


    public abstract void UpdatePuzzle();
    
}
