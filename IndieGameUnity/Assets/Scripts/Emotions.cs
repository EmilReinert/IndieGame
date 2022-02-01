using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emotions : MonoBehaviour
{
    public GameObject hurt;
    private bool hurtB;
    public GameObject happy;
    private bool happyB;
    // Start is called before the first frame update
    void Start()
    {
    }

     void StartHurt()
    {
        hurt.GetComponent<Animator>().SetTrigger("playemotion");
    }

     void StartHappy()
    {
        happy.GetComponent<Animator>().SetTrigger("playemotion");
    }
    

     public void PlayHappy()
    {
        StartHappy();
    }
     public void PlayHurt()
    {
        StartHurt();
    }

    public bool IsActiveEmotion()
    {
        if (hurtB||happyB)
            return true;
        else return false;
    }

}
