using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerManager : MonoBehaviour
{
    public GameObject showtriggerObject;
    public bool isEntered;

    public GameObject requiredObject;

    private void OnTriggerStay(Collider other)
    {

        if (requiredObject != null)
        {
            if (other.gameObject != requiredObject) return;
        }
        
        if (other.gameObject.layer == 3) return; //Environment 
        if (isEntered) return; // only first triggerd object taken
        
        isEntered = true;
        showtriggerObject = other.gameObject;
    }

    private void OnTriggerExit(Collider other)
    {
        if(showtriggerObject == other.gameObject)
        {
            isEntered = false;
            showtriggerObject = null;
        }
    }

}
