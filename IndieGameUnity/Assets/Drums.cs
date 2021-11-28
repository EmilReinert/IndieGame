using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drums : MonoBehaviour
{
    /*
    public AudioClip a;
    public AudioClip b;
    public AudioClip c;
    */          
    private AudioSource audi1;
    private AudioSource audi2;
    private AudioSource audi3;
    
    // Start is called before the first frame update
    void Start()
    {
        AudioSource[] audis = GetComponents<AudioSource>();
        audi1 = audis[0];
        audi2 = audis[1];
        audi3 = audis[2];
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && Input.GetKeyDown(KeyCode.E))
            audi3.Play();
        else
        {
            
            if (Input.GetKeyDown(KeyCode.Q))
                audi1.Play();
            if (Input.GetKeyDown(KeyCode.E))
                audi2.Play();
        }
    }
    void PlaySound(AudioClip x)
    {

    }
}
