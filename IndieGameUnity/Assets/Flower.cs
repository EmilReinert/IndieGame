using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower : MonoBehaviour
{
    public bool playerEntered = false;
    public bool done = false;
    // Start is called before the first frame update
    void Start()
    {
        playerEntered = false;
        GetComponentInChildren<Animator>().enabled = false;
        done = false;
    }
    

    public void PlayCorrectAnimation()
    {
        // correct if bad and cut 
        // or good and water
        GetComponentInChildren<Animator>().enabled = true;
        GetComponentInChildren<Animator>().SetBool("correct",true);
        done = true;

    }
    public void PlayIncorrectAnimation()
    {
        // correct if bad and cut 
        // or good and water
        GetComponentInChildren<Animator>().enabled = true;
        GetComponentInChildren<Animator>().SetBool("correct", false);
        done = true;


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
            playerEntered = true;
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.gameObject.name == "Player")
            playerEntered = false;
    }
}
