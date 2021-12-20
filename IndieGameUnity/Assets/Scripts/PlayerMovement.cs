using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : Puzzle
{
    public GameObject player;
    Animator anim;
    float goalpos;
    public float moveStrength;
    
    void SetPosition()
    {
        float to = goalpos;
        float blend = anim.GetFloat("Blend");
        if (blend == goalpos) return;

        float tempblend = moveStrength * Time.deltaTime * (blend - to);
        anim.SetFloat("Blend", blend-tempblend);
       
    }

    public override void StartPuzzle()
    {
        anim = player.GetComponent<Animator>();
        anim.enabled = true;
        goalpos = 0.5f;

        anim.SetFloat("Blend", goalpos);
    }

    public override void EndPuzzle()
    {
        if(anim!=null)
            anim.enabled = false;
    }

    public override void Move(int i)
    {
        goalpos = (float)(i + 1) / 2;
    }

    public override void UpdatePuzzle()
    {
        SetPosition();
    }
}
