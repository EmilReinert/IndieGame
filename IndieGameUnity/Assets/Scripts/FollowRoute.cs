using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowRoute : MonoBehaviour
{

    public GameObject optRotationBody; // body of person to be rotated to move direction

    public bool freeze = false;
    [SerializeField]
    private Transform[] routes;

    private Quaternion startRotation;

    private int routeIDX;

    private float tParam;

    private Vector3 objectPosition;

    private float speedModifier;

    private bool coroutineAllowed;

    // Start is called before the first frame update
    void Start()
    {
        if(optRotationBody != null)
            startRotation = optRotationBody.transform.rotation;
        routeIDX = 0;
        tParam = 0f;
        speedModifier = 0.1f;
        coroutineAllowed = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (coroutineAllowed)
        {
            StartCoroutine(GoByTheRoute(routeIDX));
        }
    }

    private IEnumerator GoByTheRoute(int routeNum)
    {
        coroutineAllowed = false;
        Vector3 p0 = routes[routeNum].GetChild(0).position;
        Vector3 p1 = routes[routeNum].GetChild(1).position;
        Vector3 p2 = routes[routeNum].GetChild(2).position;
        Vector3 p3 = routes[routeNum].GetChild(3).position;

        while (tParam < 1)
        {
            if (!freeze)
            {

                objectPosition = Mathf.Pow(1 - tParam, 3) * p0 +
                    3 * Mathf.Pow(1 - tParam, 2) * tParam * p1 +
                    3 * (1 - tParam) * Mathf.Pow(tParam, 2) * p2 + Mathf.Pow(tParam, 3) * p3;
                //objectPosition.y = transform.position.y; // dont change height
                if(optRotationBody != null)
                    optRotationBody.transform.rotation =  Quaternion.LookRotation(objectPosition - transform.position)* startRotation;
                transform.position = objectPosition;
                yield return new WaitForEndOfFrame();
                tParam += Time.deltaTime * speedModifier;
            }
            else
                break;
        }
        if (tParam >= 1)
        {
            tParam = 0f;

            routeIDX += 1;
        }

        // reset path
        if (routeIDX > routes.Length - 1)
        {
            routeIDX = 0;
        }

        coroutineAllowed = true;
    }
    
}
