using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drums : Puzzle
{      
    private AudioSource audi1;
    private AudioSource audi2;
    private AudioSource audi3;
    

    public override void StartPuzzle()
    {
        AudioSource[] audis = GetComponents<AudioSource>();
        audi1 = audis[0];
        audi2 = audis[1];
        audi3 = audis[2];
    }

    public override void EndPuzzle()
    {
    }

    public override void Move(int i)
    {
        if(i==-1)
            audi1.Play();
        if(i==1)
            audi2.Play();   
    }

    public override void UpdatePuzzle()
    {

    }
}
