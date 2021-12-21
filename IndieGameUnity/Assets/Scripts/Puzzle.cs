using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Puzzle: MonoBehaviour
{
    [HideInInspector] // Hides var below
    public bool contiuous = false;
    public bool done = false;
    public bool ended = false;
    public bool hideObject = true;

    public abstract void StartPuzzle();

    public abstract void EndPuzzle();

    public abstract void Move(int i); // -1 left 0 default 1 right 
    


    public abstract void UpdatePuzzle();
    
}
