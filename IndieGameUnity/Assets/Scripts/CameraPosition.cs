using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPosition 
{
     int aroundAngle;
     int tiltAngle;
    float distance;
     Vector3 rotationOff;

    public CameraPosition()
    {

        distance = 5;
        aroundAngle = 0;
        tiltAngle = 0;
        rotationOff = Vector3.zero;
    }
    public CameraPosition(int a, int b, float dis)
    {
        distance = dis;
        aroundAngle = a;
        tiltAngle = b;
        rotationOff = Vector3.zero;
    }
    public Vector3 GetOffsetAngle()
    {
        Vector3 direction = new Vector3(0, 0, distance);
        direction = Quaternion.Euler(tiltAngle, aroundAngle, 0)*direction;
        return direction;
    }
}
