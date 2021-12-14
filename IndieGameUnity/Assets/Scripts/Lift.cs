using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lift : Puzzle
{

    private GameObject wheel;
    public GameObject player;
    public float rotationSpeed;
    float acceleration;

    float minHeight; //lift boundaries
    float maxHeight;

    Vector3 startPos;
    float prevRot; // previous rotation
    float travel; // travel distance
   

    void DecreaseAcc()
    {
        acceleration = acceleration *( 0.97f-Time.deltaTime);
        acceleration -= 3*Time.deltaTime;
    }

    public override void StartPuzzle()
    {
        contiuous = true;

        acceleration = 0;
        startPos = transform.position;
        minHeight = startPos.y;
        maxHeight = 7;
        wheel = transform.Find("wheel").gameObject;

        player.transform.parent = transform;
        
    }

    public override void EndPuzzle()
    {
        player.transform.parent = null;
    }

    public override void Move(int i)
    {
        if(i==-1)
        acceleration += Rotate(90);
        if(i==1)
        acceleration += Rotate(-90);
    }

    public override void UpdatePuzzle()
    { 
        //Rotate and translate
        float newTravel = travel + (acceleration / 200);

        Vector3 newPos = startPos + new Vector3(0, newTravel, 0);
        if (newPos.y <= maxHeight && newPos.y >= minHeight)
        {
            travel = newTravel;

            wheel.transform.Rotate(0, 0, acceleration);
        }
        else
        {
            acceleration = 0;
            if (newPos.y > maxHeight) newPos.y = maxHeight;
            if (newPos.y < minHeight) newPos.y = minHeight;
        }

        transform.position = newPos;

        DecreaseAcc();
        if (newPos.y >= maxHeight) done = true;
    }

    float Rotate(float goalangle)
    {
        float current = wheel.transform.rotation.eulerAngles.z;
        if (current < 0) current += 360;
        if (goalangle < 0) goalangle += 360;
        if (goalangle >= 360) goalangle = goalangle % 360;
        float deltaRot = (goalangle - current);
        int multi = 0;
        if (Mathf.Abs(deltaRot) < 180)
        {
            if (deltaRot < 0)
            {
                multi = -1;
            }
            else
            {
                multi = 1;
            }
            return rotationSpeed * multi * (Mathf.Abs(deltaRot) * 0.01f + 10) * Time.deltaTime;
        }
        else
        {
            if (deltaRot > 0) deltaRot -= 180;
            if (deltaRot < 0) deltaRot += 180;
            if (deltaRot > 0)
            {
                multi = -1;
            }
            else
            {
                multi = 1;
            }
            return rotationSpeed * multi * (Mathf.Abs(deltaRot) * 0.01f + 10) * Time.deltaTime;

        }


    }
}
