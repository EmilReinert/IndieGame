using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gear : MonoBehaviour
{
    public GameObject playergear;
    public Animator playerani;
    public AudioClip coll;
    public GameObject player;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            if (playergear.activeSelf) return;
            if (coll != null)
            {
                GetComponent<AudioSource>().clip = coll;
                GetComponent<AudioSource>().Play();
            }
            gameObject.SetActive(false); // taking gear
            playergear.SetActive(true); // carrying gear
            playerani.SetBool("Cfront", true);
        }

    }

}
