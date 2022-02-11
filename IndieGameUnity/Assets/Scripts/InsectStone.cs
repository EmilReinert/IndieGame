using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsectStone : Puzzle
{
    public GameObject insects1;
    public GameObject insects2;
    public GameObject insects3;
    public GameObject insectStone;
    public ToxicPersonBehavior t;

    float neededtime = 20; //TODO increase later
    float waittime = 3;
    float timer = 0;
    public float currenttime = 0;
    public override void EndPuzzle()
    {
        insects1.SetActive(false);
        insects2.SetActive(false);
        insects3.SetActive(false);

        insectStone.SetActive(false);
        GameObject.Find("Player").GetComponentInChildren<Animator>().SetBool("Ctop", false);

    }

    public override void Move(int i)
    {
    }

    public override void StartPuzzle()
    {
        insects1.SetActive(true);
        insects2.SetActive(true);
        insects3.SetActive(true);

        insectStone.SetActive(true);
        t.walkAnyway = true;

        GameObject.Find("Player").GetComponentInChildren<Animator>().SetBool("Ctop", true);
    }

    public override void UpdatePuzzle()
    {
        timer += Time.deltaTime;
        if (timer > waittime)
        {
            currenttime += Time.deltaTime;
        }
        if (currenttime > neededtime)
            done = true;
    }
    private void Start()
    {
        
        insects1.SetActive(false);
        insects2.SetActive(false);
        insects3.SetActive(false);

        insectStone.SetActive(false);
    }

    public void Response()
    {
        timer = 0;
    }
}
