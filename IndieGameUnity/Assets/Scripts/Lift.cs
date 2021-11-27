using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lift : MonoBehaviour
{
    public GameObject wheel;
    float rotationSpeed;
    float acceleration;

    Vector3 startPos;
    float prevRot; // previous rotation
    float travel; // travel distance
    // Start is called before the first frame update
    void Start()
    {
        rotationSpeed = 10;
        acceleration = 0;
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //if (transform.position.y == startPos.y) { transform.position = startPos; return; }
        if (Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.E))
        {
            if (Input.GetKey(KeyCode.Q)) Rotate(90);
            if (Input.GetKey(KeyCode.E)) Rotate(-90);
        }

        //Rotate
        wheel.transform.Rotate(0,0,acceleration * Time.deltaTime);
        travel += acceleration/10000;
        print(travel);
        transform.position = startPos + new Vector3(0,travel,0);

        StartCoroutine(DecreaseAcc());

    }
    void Rotate(float goalangle){
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
            acceleration += rotationSpeed * multi * (Mathf.Abs(deltaRot) * 0.01f + 10);
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
            acceleration += rotationSpeed * multi * (Mathf.Abs(deltaRot) * 0.01f + 10);

        }
        

    }
    IEnumerator DecreaseAcc()
    {
        yield return new WaitForSeconds(0.1f);
        acceleration = acceleration * 0.95f;
        acceleration -= 10;
    }

}
