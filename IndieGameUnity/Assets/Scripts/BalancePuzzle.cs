using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalancePuzzle : Puzzle
{
    private int moveRadius; // avalable space left AnD right
    public  float currentAngle;
    private bool randoming;
    public GameObject rotatingelement;
    private Quaternion startRotation;
    private Quaternion startRotationB;
    public float changer;
    private GameObject player;
    public GameObject startPos;
    public GameObject rotationBody;
    private bool failing = false;
        public TriggerManager end;

    public GameObject build;

    public override void EndPuzzle()
    {
        build.SetActive(false);

    }

    public override void Move(int i)
    {
        changer = -i*10;
    }

    public override void StartPuzzle()
    {
        build.SetActive(true);
        moveRadius = 30; // equals cylinders
        ResetPos();
        failing = false;
    }
    private void ResetPos()
    {
        player.GetComponent<Walk>().Freeze(true);
        randoming = false;
        StopAllCoroutines();
        rotatingelement.transform.rotation = startRotation;
        rotationBody.transform.rotation = startPos.transform.rotation;
        player.transform.position = startPos.transform.position;
        currentAngle = 0;
        player.GetComponent<Walk>().Freeze(false);
        failing = false;

    }

    public override void UpdatePuzzle()
    {
        if (end.isEntered) done = true;
        float randomdirection = (Random.value - 0.5f)*moveRadius;
        if (!randoming&&!failing)
        {
            StartCoroutine(
            MoveAbout(randomdirection));
           // MoveAbout(1)); // testing latency
        }

    }
    IEnumerator MoveAbout(float i)
    {
        if (i != 0)
        {
            // roatesa bout float and direction
            randoming = true;
            float start = 0;
            //float direction = Mathf.Sign(i);
            int steps = 20; // animation steps;
            float increment = i / steps;
            Vector3 eulerRot = new Vector3(0, 0, 1);
            while (Mathf.Abs(start) <= Mathf.Abs(i))
            {
                start += Time.deltaTime * (i / steps) * 40;

                rotatingelement.transform.Rotate(Time.deltaTime * (i / steps) * eulerRot * 40);
                rotatingelement.transform.Rotate(Time.deltaTime * (changer / steps) * eulerRot * 40);

                rotationBody.transform.Rotate(Time.deltaTime * (i / steps) * eulerRot * 40);
                rotationBody.transform.Rotate(Time.deltaTime * (changer / steps) * eulerRot * 40);

                currentAngle += Time.deltaTime * (i / steps)  * 40;
                currentAngle += Time.deltaTime * (changer / steps)  * 40;


                //fail
                if (Mathf.Abs(currentAngle) > moveRadius)
                {
                    if (currentAngle < -moveRadius)
                        StartCoroutine(Fail(true));
                    else
                        StartCoroutine(Fail(false));    
                }
                


                yield return new WaitForEndOfFrame();
            }

            randoming = false;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        build.SetActive(false);
        contiuous = true;
        player = GameObject.Find("Player");
        startRotation = rotatingelement.transform.rotation;
        startRotationB = rotatingelement.transform.rotation;

    }
    
    IEnumerator Fail(bool left)
    {
        failing = true;
        player.GetComponent<Walk>().Drop(true);
        if(left)
            player.GetComponent<Rigidbody>().velocity = new Vector3(10, 0, 0);
        else
            player.GetComponent<Rigidbody>().velocity = new Vector3(-10, 0, 0);
        yield return new WaitForSeconds(2);
        player.GetComponent<Walk>().Drop(false);
        faily = true;
    }
}
