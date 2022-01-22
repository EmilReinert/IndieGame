using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalancePuzzle : Puzzle
{
    private int moveRadius; // avalable space left AnD right
    private float currentAngle;
    private bool randoming;
    public GameObject rotatingelement;
    private Quaternion startRotation;

    public override void EndPuzzle()
    {

    }

    public override void Move(int i)
    {

    }

    public override void StartPuzzle()
    {
        moveRadius = 20;
        randoming = false;
        startRotation = rotatingelement.transform.rotation;
    }

    public override void UpdatePuzzle()
    {
        float randomdirection = (Random.value - 0.5f)*moveRadius;
        if (!randoming)
        {
            StartCoroutine(
           // MoveAbout(randomdirection));
            MoveAbout(1));
        }

    }
    IEnumerator MoveAbout(float i)
    {
        // roatesa bout float and direction
        randoming = true;
        float start = 0;
        //float direction = Mathf.Sign(i);
        int steps = 10; // animation steps;
        float increment = i / steps;
        Vector3 eulerRot = new Vector3(0, 0, 1);
        while (Mathf.Abs(start) <= Mathf.Abs(i))
        {
            start += Time.deltaTime *(i / steps)*100;
            rotatingelement.transform.Rotate(Time.deltaTime * (i / steps) * eulerRot*100);
            yield return new WaitForEndOfFrame();
        }

        randoming = false;

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
