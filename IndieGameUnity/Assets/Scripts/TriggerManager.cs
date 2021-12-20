using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerManager : MonoBehaviour
{
    public GameObject triggerObject;
    public bool isEntered;
    

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer != 4) return; //Environment 
        if (isEntered) return; // only first triggerd object taken

        isEntered = true;
        triggerObject = other.gameObject;
    }

    private void OnTriggerExit(Collider other)
    {
        if(triggerObject == other.gameObject)
        {
            isEntered = false;
            triggerObject = null;
        }
    }

}
