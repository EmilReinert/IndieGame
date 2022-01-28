using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLakeBehavior : Puzzle
{
    public FollowRoute followRoute;
    public GameObject followWho;
    public int distanceOffset;
    public GameObject player;

    // Start is called before the first frame update
    IEnumerator UpdateWalk()
    {
        yield return new WaitForSeconds(0.5f); // reduce stuttering
        if (Vector3.Distance(followWho.transform.position, transform.position) < distanceOffset)
            followRoute.Freeze(true);
        else
            followRoute.Freeze(false);
    }

    IEnumerator UpdateRotation()
    {
        yield return new WaitForSeconds(0.5f); // reduce stuttering
        transform.rotation = Quaternion.LookRotation(followWho.transform.position - transform.position); // look at grandpa
    }

    public override void StartPuzzle()
    {
        hideObject = false;
        player = this.gameObject;
        followRoute.Freeze(false);
    }

    public override void EndPuzzle()
    {
        
    }

    public override void Move(int i)
    {
    }

    public override void UpdatePuzzle()
    {
        StartCoroutine(UpdateWalk());
        //StartCoroutine(UpdateRotation());
    }
}
