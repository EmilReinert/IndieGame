using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Animator animLantern;
    Animator anim;
    float goalpos;
    float goallantern;
    public float moveStrength;
    public float rotationStrength;
    public GameObject lantern;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        animLantern = lantern.GetComponent<Animator>();
        SetPosition(0.5f);
        SetLantern(0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            if (Input.GetKey(KeyCode.A)) goalpos = 0;
            if (Input.GetKey(KeyCode.D)) goalpos = 1;
        }

        else
            goalpos = 0.5f;

        SetPosition(goalpos);

        if (Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.E))
        {
            if (Input.GetKey(KeyCode.Q)) goallantern = 0;
            if (Input.GetKey(KeyCode.E)) goallantern = 1;
        }

        else
            goallantern = 0.5f;

            SetLantern(goallantern);

    }

    void SetPosition(float to)
    {
        float blend = anim.GetFloat("Blend");
        if (blend == goalpos) return;

        float tempblend = moveStrength * (blend - to);
        anim.SetFloat("Blend", blend-tempblend);
       
    }
    void SetLantern(float to)
    {
        if (lantern == null) return;
            float blend = animLantern.GetFloat("Blend");
        if (blend == goalpos) return;

        float tempblend = rotationStrength * (blend - to);
        animLantern.SetFloat("Blend", blend - tempblend);

    }
}
