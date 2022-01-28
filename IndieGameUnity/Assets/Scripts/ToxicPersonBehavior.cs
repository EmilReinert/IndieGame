using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToxicPersonBehavior : Puzzle
{
    public FollowRoute f;
    public GameObject player;
    public float maxDist = 20;
    public bool walkAnyway = false;
    
    public TriggerManager visionTrigger;

    private float visibleMatThreshold; // required alpha value to see object

    public override void EndPuzzle()
    {

        f.Freeze(true);
    }

    public override void Move(int i)
    {
    }

    public override void StartPuzzle()
    {

    }

    public override void UpdatePuzzle()
    {
        float currentDistance = Vector3.Distance(player.transform.position, f.transform.position);
        float speed = -(1 / maxDist) * currentDistance + 1;
        /*
        if (speed < 0.01f) f.freeze = true;
        else f.freeze = false;
        f.speedModifier = speed;
        */
        // only if lake is visible continue
        if (walkAnyway)
        {

            f.Freeze(false); return;
        }
        if (!visionTrigger.isEntered)
        {

            f.Freeze(true); return;
        }
        if (visionTrigger.showtriggerObject.GetComponent<Renderer>().material.GetFloat("_A2") >= visibleMatThreshold)

            f.Freeze(false);
        else

            f.Freeze(true); 
    }

    // Start is called before the first frame update
    void Start()
    {
        visibleMatThreshold = 0.99f;
        f.Freeze(true);
    }
    
}
