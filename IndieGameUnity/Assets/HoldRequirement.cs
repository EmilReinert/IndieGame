using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldRequirement : MonoBehaviour
{
    public Puzzle requirement;
    public GrandWalk gw;
    public bool done = false;

    private void Start()
    {
        done = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (done) return;

        if (other.gameObject == gw.gameObject)
        {
            print(requirement.done);
            if (requirement.done) { 
                done = true;
                gw.Freeze(false);
            }
            else
            {

                gw.Freeze(true);
            }
        }
    }
}
