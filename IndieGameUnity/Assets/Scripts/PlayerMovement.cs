using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Animator anim;
    float goalpos;
    public float moveStrength;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        SetPosition(0.5f);
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


    }

    void SetPosition(float to)
    {
        float blend = anim.GetFloat("Blend");
        if (blend == goalpos) return;

        float tempblend = moveStrength * Time.deltaTime * (blend - to);
        anim.SetFloat("Blend", blend-tempblend);
       
    }
}
