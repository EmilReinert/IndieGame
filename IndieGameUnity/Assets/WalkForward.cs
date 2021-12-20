using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkForward : Puzzle
{
    public float walkSpeed;
    public bool freeze;
    public GameObject player;

    public override void EndPuzzle()
    {
    }

    public override void Move(int i)
    {
    }

    public override void StartPuzzle()
    {
        freeze = false;
    }

    public override void UpdatePuzzle()
    {
        if (!freeze)
        {
            player.transform.Translate(0, 0, walkSpeed * Time.deltaTime);
        }
    }
    
}
