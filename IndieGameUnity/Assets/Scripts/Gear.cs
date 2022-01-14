using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gear : MonoBehaviour
{
    public GameObject playergear;

    public GameObject player;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            if (playergear.activeSelf) return;
            gameObject.SetActive(false); // taking gear
            playergear.SetActive(true); // carrying gear
        }

    }

}
