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

    public GameObject particles;
    AudioSource audi;
    bool hitNote;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        goalpos = 0.5f;
        minDist = 0.5f-3*(dist * 0.5f)/4;
        maxDist = 0.5f+3*(dist * 0.5f)/4;

        audi = GetComponent<AudioSource>();
        audi.Play();
        hitNote = true;
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

        // when note is hit
        if (Mathf.Abs(anim.GetFloat("Blend") - goalpos) < (dist * 0.5f) / 8&&!hitNote)
        {
            // tweak audio
            audi.pitch = 1 + 1.9f * (goalpos - 0.5f);
            audi.Play();
            
            particles.SetActive(true);
            hitNote = true;
        }


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
        hitNote = false;
        particles.SetActive(false);
    }
}
