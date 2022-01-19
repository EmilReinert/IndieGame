using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LionPuzzle : Puzzle
{
    public GameObject[] bushes;
    public GameObject lionBody;
    public Hide hide;

    public override void EndPuzzle()
    {
    }

    public override void Move(int i)
    {
    }

    public override void StartPuzzle()
    {
    }

    public override void UpdatePuzzle()
    {
        bool inbush = false;
        foreach(GameObject b in bushes)
        {
            if (b.GetComponent<TriggerManager>().isEntered) inbush = true;
        }
        //print(        hide.GetHiding() +""+ inbush);
        if (inbush)
        {
            if (hide.GetHiding()==0) Reaction(1);
            if (hide.GetHiding() ==1) Reaction(1);
            if (hide.GetHiding() == 2) Reaction(0);
        }
        else
        {

            if (hide.GetHiding() < 2) Reaction(2);
            if (hide.GetHiding() == 2) Reaction(1);

        }
    }

    void Reaction(int i)
    {
        // 0 calm 1 suspicious 2 agressive
        if (0==i) lionBody.GetComponent<Renderer>().material.color = Color.green;
        if (1==i) lionBody.GetComponent<Renderer>().material.color = Color.yellow;
        if (2==i) lionBody.GetComponent<Renderer>().material.color = Color.red;
    }
    
}
