using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanternMove : MonoBehaviour
{
    Animator animLantern;
    public float rotationStrength;
    float goallantern;
    // Start is called before the first frame update
    void Start()
    {
        animLantern = GetComponent<Animator>();
        SetLantern(0.5f);

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.E))
        {
            if (Input.GetKey(KeyCode.Q)) goallantern = 0;
            if (Input.GetKey(KeyCode.E)) goallantern = 1;
        }

        else
            goallantern = 0.5f;

        SetLantern(goallantern);
    }
    void SetLantern(float to)
    {
        float blend = animLantern.GetFloat("Blend");
        if (blend == goallantern) return;

        float tempblend = rotationStrength * Time.deltaTime * (blend - to);
        animLantern.SetFloat("Blend", blend - tempblend);

    }
}
