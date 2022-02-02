using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabWallPuzzle : Puzzle
{
    private Walk walk;
    public GrabWall[] grabs;
    public GrabWall current;
    bool block = false;
    bool fail = false;
    public bool moving = false;
    private Animator ani;
    public override void EndPuzzle()
    {

        walk.Freeze(false);
        ani.SetBool("Cclimb", false);
        walk.transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    public override void Move(int i)
    {
        if (i == 0) return;
        if (current.nextLeft == null && current.nextRight == null) return; // no next step
        if (current.nextLeft != null && i == -1) StartCoroutine(MoveNext(current.nextLeft));
        if (current.nextRight != null && i == 1) StartCoroutine(MoveNext(current.nextRight));
    }

    public override void StartPuzzle()
    {
        block = false;
        Start();
        print("Freeze ");
        walk.Freeze(true);
        StartCoroutine(MoveNext(grabs[0]));
        foreach (GrabWall g in grabs)
        {
            g.active = true;
        }
        moving = false;

        ani.SetBool("Cclimb", true);
    }

    public override void UpdatePuzzle()
    {
        if (Input.GetAxis("Vertical")<0&&!moving)
            StartCoroutine(MovePrevious());

        if (current.animalOut&&!fail) StartCoroutine(Fail());
    }

    private void Start()
    {
        grabs = GetComponentsInChildren<GrabWall>();
        GameObject player = GameObject.Find("Player");
        walk = player.GetComponent<Walk>();
        ani = player.GetComponentInChildren<Animator>();
    }

    IEnumerator MoveNext(GrabWall g)
    {
        if (!block&&!moving)
        {
            // todo animation
            g.previous = current;
            current = g;
            Vector3 look = GameObject.Find("CenterSphere").transform.position - walk.transform.position;
            look.y = 0;
            walk.SetPosition(current.transform.position - new Vector3(0, 3, 0) - look * 0.08f, Quaternion.LookRotation(look));

            ani.SetTrigger("Cclimbing");
            yield return new WaitForSeconds(1);


            if (current.finalGrab) done = true;
        }
    }

    IEnumerator MovePrevious()
    {
        // todo animation
        if (current.previous != null)
        {
            moving = true;
            current = current.previous;
            Vector3 look = GameObject.Find("CenterSphere").transform.position - walk.transform.position;
            look.y = 0;

            ani.SetTrigger("Cclimbing");
            walk.SetPosition(current.transform.position - new Vector3(0, 3, 0) - look * 0.05f, Quaternion.LookRotation(look));
            yield return new WaitForSeconds(1);

            moving = false;
        }
    }

    IEnumerator Fail()
    {
        fail = true;
        block = true;
        // todo animation
        yield return new WaitForSeconds(1);
        StartCoroutine(MovePrevious());

        block = false;
        fail = false;

    }
}
