using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabWall : MonoBehaviour
{
    public GrabWall nextLeft;
    public GrabWall nextRight;
    [HideInInspector]
    public GrabWall previous;
    public bool finalGrab = false;

}
