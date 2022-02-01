using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanternMove : Puzzle
{
    Animator animLantern;
    public float rotationStrength;
    float goallantern;

    bool move = false;

    public override void StartPuzzle()
    {
        animLantern = GetComponent<Animator>();
        goallantern = 0.5f;
        SetLantern();

        GameObject.FindObjectOfType<Walk>().main.SetBool("Cflash", true);

        //contiuous = true;
        move = false;
    }

    public override void EndPuzzle()
    {
        GameObject.FindObjectOfType<Walk>().main.SetBool("Cflash", false);
        gameObject.SetActive(false);
    }

    public override void Move(int i)
    {
        if (!move)
        {
            if (i == 0)
                StartCoroutine(SetPos((float)(i + 1) / 2, false));
            else
                StartCoroutine(SetPos((float)(i + 1) / 2, true));

        }
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
    IEnumerator SetPos(float pos, bool wait)
    {
        move = true;
        goallantern = pos;
        if (wait)
            yield return new WaitForSeconds(0.1f);
        move = false;
    }
}
