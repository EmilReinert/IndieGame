using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToxicPersonBehavior : MonoBehaviour
{
    public FollowRoute followRoute;
    public TriggerManager visionTrigger;

    private float visibleMatThreshold; // required alpha value to see object
    // Start is called before the first frame update
    void Start()
    {
        visibleMatThreshold = 0.2f; followRoute.freeze = true;
    }

    // Update is called once per frame
    void Update()
    {
        // only if lake is visible continue
        if (!visionTrigger.isEntered)
        {
            followRoute.freeze = true; return;
        }
        if (visionTrigger.showtriggerObject.GetComponent<Renderer>().material.color.a >= visibleMatThreshold)
            followRoute.freeze = false;
        else
            followRoute.freeze = true;
    }
}
