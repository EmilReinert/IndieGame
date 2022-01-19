using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrandWalk : Puzzle
{
    public FollowRoute f;
    public GameObject player;
    public float  maxDist = 5;

    public override void EndPuzzle()
    {
        f.freeze = true;
    }

    public override void Move(int i)
    {

    }

    public override void StartPuzzle()
    {
        f.freeze = false;
    }

    public override void UpdatePuzzle()
    {
        float currentDistance =Vector3.Distance( player.transform.position,f.transform.position);
        float speed = -(1 / maxDist) * currentDistance + 1;
        if (speed < 0.01f)f.freeze = true;
        else f.freeze = false;
        f.speedModifier = speed;
    }

    // Start is called before the first frame update
    void Start()
    {
        f.freeze = true;
        f.speedModifier = 0.2f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
