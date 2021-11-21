using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLakeBehavior : MonoBehaviour
{
    public FollowRoute followRoute;
    public GameObject followWho;
    public int distanceOffset;
    // Start is called before the first frame update
    void Start()
    {
        followRoute.freeze = false;
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(UpdateWalk());
        //StartCoroutine(UpdateRotation());

    }
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
}
