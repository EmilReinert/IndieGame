using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowRoute : MonoBehaviour
{

    public GameObject optRotationBody; // body of person to be rotated to move direction
    public Animator ani;
    [SerializeField]
    private bool freeze = false;
    [SerializeField]
    private GameObject route;
    private Transform[] routes;
    public bool UseStartRotation = false;
    private Quaternion startRotation;
    private Vector3 startPos;

    private int routeIDX;

    private float tParam;

    private Vector3 objectPosition;

    public  float speedModifier=0.1f;

    private bool coroutineAllowed;
    private bool done = false;
    public bool loop = false;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        if(optRotationBody != null)
            startRotation = optRotationBody.transform.rotation;
        routeIDX = 0;
        tParam = 0f;
        coroutineAllowed = true;
        if (route != null)
        {
            routes = new Transform[route.GetComponentsInChildren<Route>().Length];
            Route[] rs = route.GetComponentsInChildren<Route>();
            for (int i = 0; i < routes.Length; i++)
                routes[i] = rs[i].transform;
        }
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
                if (optRotationBody != null&&tParam!=0)
                {

                    optRotationBody.transform.rotation = Quaternion.LookRotation(objectPosition - transform.position);// + transform.forward);// * startRotation;
                }
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

        coroutineAllowed = true;


        // reset path
        if (routeIDX > routes.Length - 1)
        {
            if (loop)
                routeIDX = 0;
            else
            {
                done = true;
                Freeze(true);
                coroutineAllowed = false;
            }
        }
    }

    public void Freeze(bool b)
    {

        if (b)
        {

            //freeze 
            freeze = true;
            if (ani != null)
                ani.SetBool("walk", false);
        }
        else
        {

            //unfreeze 
            freeze = false;
            if (ani != null)
                ani.SetBool("walk", true);
        }
    }

    public void Re()
    {
        StopAllCoroutines();
        print("re");
        routeIDX = 0;
        tParam = 0f;
        coroutineAllowed = true;
        transform.position = startPos;

    }

}
