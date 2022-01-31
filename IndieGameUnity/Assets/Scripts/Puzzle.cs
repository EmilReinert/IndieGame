using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Puzzle: MonoBehaviour
{

    public bool contiuous = false;
    public bool done = false; // task done
    public bool ended = false; // puzzle ended by puzzlemanager
    [HideInInspector]
    public bool hideObject = true;
    [HideInInspector]
    public bool doneAnimation = true; // happy animation when done
    [HideInInspector]
    public bool faily = false;

    public abstract void StartPuzzle();

    public abstract void EndPuzzle(); // called in puzzlemanager aftre Done = true

    public abstract void Move(int i); // -1 left 0 default 1 right 
    


    public abstract void UpdatePuzzle();
    
}
