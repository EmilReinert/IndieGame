using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldRequirement : MonoBehaviour
{
    public Puzzle requirement;
    public GrandWalk gw;
    public ToxicPersonBehavior tx;
    public bool done = false;

    private void Start()
    {
        done = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (done) return;
        if (gw == null && tx == null) return;
        if (gw!=null&&other.gameObject == gw.gameObject)
        {
            //print(requirement.done);
            if (requirement.done) { 
                done = true;
                gw.Freeze(false);
            }
            else
            {

                gw.Freeze(true);
            }
        }
        if (tx != null && other.gameObject == tx.gameObject)
        {
            //print(requirement.done);
            if (requirement.done)
            {
                done = true;
                tx.Freeze(false);
            }
            else
            {

                tx.Freeze(true);
            }
        }
    }
}
