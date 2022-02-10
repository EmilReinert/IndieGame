using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHide : MonoBehaviour
{
    ///
    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Renderer>()!=null)
        other.GetComponent<Renderer>().enabled = false;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Renderer>() != null)
            other.GetComponent<Renderer>().enabled = true;

    }
}
