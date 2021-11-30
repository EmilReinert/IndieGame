using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music2 : MonoBehaviour
{
    Animator anim;
    float goalpos;
    public float moveStrength;
    public float dist = 0.4f; //0.01-1
    float minDist;
    float maxDist;

    AudioSource audi;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        goalpos = 0.5f;
        minDist = 0.5f-3*(dist * 0.5f)/4;
        maxDist = 0.5f+3*(dist * 0.5f)/4;

        audi = GetComponent<AudioSource>();
        audi.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.E))
        {
            if (Input.GetKeyDown(KeyCode.Q))SetGoalPos( -(dist*0.5f)/4);
            if (Input.GetKeyDown(KeyCode.E)) SetGoalPos( + dist*0.5f/4);
        }
        

        SetPosition(goalpos);


    }

    void SetPosition(float to)
    {
        float blend = anim.GetFloat("Blend");
        if (blend == goalpos) return;

        float tempblend = moveStrength * Time.deltaTime * (blend - to);
        anim.SetFloat("Blend", blend - tempblend);

    }
    void SetGoalPos(float difference)
    {
        float newPos = goalpos + difference;
        if (newPos > maxDist || newPos < minDist) return;
        goalpos = newPos;
        audi.pitch =1+ 2*(goalpos-0.5f);
    }
}
