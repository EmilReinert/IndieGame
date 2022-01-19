using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToxicPersonBehavior : Puzzle
{
    public FollowRoute followRoute;
    public TriggerManager visionTrigger;

    private float visibleMatThreshold; // required alpha value to see object

    public override void EndPuzzle()
    {
        followRoute.freeze = true;
    }

    public override void Move(int i)
    {
    }

    public override void StartPuzzle()
    {

    }

    public override void UpdatePuzzle()
    {
        // only if lake is visible continue
        if (!visionTrigger.isEntered)
        {
            followRoute.freeze = true; return;
        }
        if (visionTrigger.showtriggerObject.GetComponent<Renderer>().material.GetFloat("_A2") >= visibleMatThreshold)
            followRoute.freeze = false;
        else
            followRoute.freeze = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        visibleMatThreshold = 0.2f; followRoute.freeze = true;
    }
    
}
