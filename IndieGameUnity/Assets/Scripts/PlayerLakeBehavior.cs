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
            followRoute.freeze = true;
        else
            followRoute.freeze = false;
    }

    IEnumerator UpdateRotation()
    {
        yield return new WaitForSeconds(0.5f); // reduce stuttering
        transform.rotation = Quaternion.LookRotation(followWho.transform.position - transform.position); // look at grandpa
    }

    public override void StartPuzzle()
    {
        player = this.gameObject;
        followRoute.freeze = false;
        player.GetComponent<Rigidbody>().isKinematic = true;
    }

    public override void EndPuzzle()
    {
        player.GetComponent<Rigidbody>().isKinematic = false;
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
