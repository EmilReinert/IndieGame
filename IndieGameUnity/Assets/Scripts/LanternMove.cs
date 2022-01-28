using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanternMove : Puzzle
{
    Animator animLantern;
    public float rotationStrength;
    float goallantern;

    public override void StartPuzzle()
    {
        animLantern = GetComponent<Animator>();
        goallantern = 0.5f;
        SetLantern();

        GameObject.FindObjectOfType<Walk>().main.SetBool("Cflash", true);

        contiuous = true;
    }

    public override void EndPuzzle()
    {
        GameObject.FindObjectOfType<Walk>().main.SetBool("Cflash", false);
        gameObject.SetActive(false);
    }

    public override void Move(int i)
    {
        goallantern = (float)(i + 1) / 2;
    }

    public override void UpdatePuzzle()
    {
        SetLantern();
    }

    void SetLantern()
    {
        float to = goallantern;
        float blend = animLantern.GetFloat("lantern");
        if (blend == goallantern) return;

        float tempblend = rotationStrength * Time.deltaTime * (blend - to);
        animLantern.SetFloat("lantern", blend - tempblend);

    }
}
