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
    public float changer;
    private GameObject player;
    private Vector3 startPos;

    public override void EndPuzzle()
    {

    }

    public override void Move(int i)
    {
        changer = -i*10;
    }

    public override void StartPuzzle()
    {
        player = GameObject.Find("Player");
        startRotation = rotatingelement.transform.rotation;
        startPos = player.transform.position;
        moveRadius = 30; // equals cylinders
        Reset();
    }
    private void Reset()
    {

        randoming = false;
        StopAllCoroutines();
        rotatingelement.transform.rotation = startRotation;
        player.transform.position = startPos;
        currentAngle = 0;
}

    public override void UpdatePuzzle()
    {
        float randomdirection = (Random.value - 0.5f)*moveRadius;
        if (!randoming)
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
                currentAngle += Time.deltaTime * (i / steps)  * 40;
                currentAngle += Time.deltaTime * (changer / steps)  * 40;
                //fail
                if (Mathf.Abs(currentAngle) > moveRadius)
                    Reset();


                yield return new WaitForEndOfFrame();
            }

            randoming = false;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        contiuous = true;
    }
    
}
