using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : Puzzle
{
    public GameObject player;
    Animator anim;
    float goalpos;
    public float moveStrength;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {



    }

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
        goalpos = 0.5f;
        SetPosition();
    }

    public override void EndPuzzle()
    {
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
