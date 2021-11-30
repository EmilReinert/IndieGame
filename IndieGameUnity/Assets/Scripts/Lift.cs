using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lift : MonoBehaviour
{
    public GameObject wheel;
    float rotationSpeed;
    float acceleration;

    float minHeight; //lift boundaries
    float maxHeight;

    Vector3 startPos;
    float prevRot; // previous rotation
    float travel; // travel distance
    // Start is called before the first frame update
    void Start()
    {
        rotationSpeed = 10;
        acceleration = 0;
        startPos = transform.position;
        minHeight = startPos.y;
        maxHeight = 10;
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(DecreaseAcc());
        //if (transform.position.y == startPos.y) { transform.position = startPos; return; }
        float newAcceleration = acceleration;
        if (Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.E))
        {
            if (Input.GetKey(KeyCode.Q)) newAcceleration += Rotate(90);
            if (Input.GetKey(KeyCode.E)) newAcceleration += Rotate(-90);
        }

        //Rotate and translate
        float newTravel =travel + acceleration / 10000;
        
        Vector3 newPos = startPos + new Vector3(0,newTravel,0);
        print(newPos.y + " " + maxHeight + " " + minHeight);
        if (newPos.y <= maxHeight && newPos.y>= minHeight)
        {
            travel = newTravel;
            acceleration = newAcceleration;

            wheel.transform.Rotate(0, 0, acceleration * Time.deltaTime);
        }
        else
        {
            acceleration = 0;
            if (newPos.y > maxHeight) newPos.y = maxHeight;
            if (newPos.y < minHeight) newPos.y = minHeight;
        }

        transform.position = newPos;





    }
    float Rotate(float goalangle){
        float current = wheel.transform.rotation.eulerAngles.z;
        if (current < 0) current += 360;
        if (goalangle < 0) goalangle += 360;
        float deltaRot = (goalangle - current);
        int multi = 0;
        if (Mathf.Abs(deltaRot)<180)
        {
            if (deltaRot < 0)
            {
                multi = -1;
            }
            else
            {
                multi = 1;
            }
            return rotationSpeed * multi * (Mathf.Abs(deltaRot) * 0.01f + 10);
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
            return rotationSpeed * multi * (Mathf.Abs(deltaRot) * 0.01f + 10);

        }
        

    }
    IEnumerator DecreaseAcc()
    {
        yield return new WaitForSeconds(0.1f);
        acceleration = acceleration * 0.95f;
        acceleration -= 10;
    }

}
