using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrandWalk : Puzzle
{
    public FollowRoute f;
    GameObject player;
    public float  maxDist = 5;
    public bool disappear=false;

    [SerializeField]
    bool localFreeze;

    public override void EndPuzzle()
    {
        f.Freeze(true);
        if(disappear)
            GetComponentInChildren<SkinnedMeshRenderer>().enabled = false;
    }

    public override void Move(int i)
    {

    }

    public override void StartPuzzle()
    {
        f.Freeze(false);
        localFreeze = false;
        player = GameObject.Find("Player");
        GetComponentInChildren<SkinnedMeshRenderer>().enabled = true;
    }

    public override void UpdatePuzzle()
    {
        if (f.done) done = true;
        if (localFreeze) return;


        float currentDistance =Vector3.Distance( player.transform.position,f.transform.position);
        float speed = -(1 / maxDist) * currentDistance + 1;
        if (speed < 0.01f)
            f.Freeze(true);
        else
            f.Freeze(false);
        f.speedModifier = speed;
    }

    // Start is called before the first frame update
    void Start()
    {

        f.Freeze(true);
        f.speedModifier = 0.2f;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Freeze(bool b)
    {
        localFreeze = b;
        f.Freeze(b);
    }


}
