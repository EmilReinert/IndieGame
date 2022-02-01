using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LionPuzzle : Puzzle
{
    public GameObject bushContainer;
    private GameObject[] bushes;
    public GameObject lionBody;
    public FollowRoute realLion;
    private Hide hide;
    private bool inbush;
    private bool seeing; // lion seeing player
    private bool failing=false;
    public GameObject goalsphere;

    public GameObject steak;
    private void Start()
    {
        TriggerManager[] t = bushContainer.GetComponentsInChildren<TriggerManager>();
        bushes = new GameObject[t.Length];
        for (int i = 0; i < t.Length; i++)
            bushes[i] = t[i].gameObject;
        realLion.Freeze(true);
        inbush = false;
        seeing = false;
        steak.SetActive(false);
        GetComponent<Animator>().enabled = false;
    }
    public override void EndPuzzle()
    {
    }

    public override void Move(int i)
    {
    }

    public override void StartPuzzle()
    {
        realLion.Freeze(false);
        hide = GameObject.FindObjectOfType<Hide>();
        hide.gameObject.SetActive(true);
        steak.SetActive(true);
        GetComponent<Animator>().enabled = false;
        realLion.Re();
        failing = false;
    }

    public override void UpdatePuzzle()
    {
        inbush = false;
        foreach(GameObject b in bushes)
        {
            if (b.GetComponent<TriggerManager>().isEntered) inbush = true;
        }

        if (seeing)
        {
            if (hide.GetHiding() == 2 && inbush) { Reaction(0); }//still hidden
            else {if(!failing) StartCoroutine(Fail()); }
        }
        else { Reaction(1); }
        //print(        hide.GetHiding() +""+ inbush);

        /*
        if (inbush)
        {
            if (hide.GetHiding()==0) Reaction(1);
            if (hide.GetHiding() ==1) Reaction(1);
            if (hide.GetHiding() == 2) Reaction(0);
        }
        else
        {

            if (hide.GetHiding() < 2) Reaction(2);
            if (hide.GetHiding() == 2) Reaction(1);

        }
        */

        if (goalsphere.GetComponent<TriggerManager>().isEntered) done = true;
    }

    void Reaction(int i)
    {
        // 0 calm 1 suspicious 2 agressive
        if (0==i) lionBody.GetComponent<Renderer>().material.color = Color.green;
        if (1==i) lionBody.GetComponent<Renderer>().material.color = Color.yellow;
        if (2==i) lionBody.GetComponent<Renderer>().material.color = Color.red;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            seeing = true;

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            seeing = false;

        }
    }

    IEnumerator Fail()
    {

        failing = true;
        Reaction(2);
        if (GetComponent<Animator>().enabled)
            GetComponent<Animator>().SetTrigger("lioncome");
        else
            GetComponent<Animator>().enabled = true;
        yield return new WaitForSeconds(1);
        faily = true;
        failing = false;
    }
}
