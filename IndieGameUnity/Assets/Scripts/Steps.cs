using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Steps : Puzzle
{
    public Animator animatedPlayer;
    private GameObject player;

    private void Start()
    {
        player = GameObject.Find("Player");
    }

    public override void EndPuzzle()
    {
        throw new System.NotImplementedException();
    }

    public override void Move(int i)
    {
        throw new System.NotImplementedException();
    }

    public override void StartPuzzle()
    {
        throw new System.NotImplementedException();
    }

    public override void UpdatePuzzle()
    {
        throw new System.NotImplementedException();
    }
    
}
