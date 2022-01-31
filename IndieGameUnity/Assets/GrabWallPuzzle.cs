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

    public override void EndPuzzle()
    {

        walk.Freeze(false);
    }

    public override void Move(int i)
    {
        if (i == 0) return;
        if (current.nextLeft == null && current.nextRight == null) return; // no next step
        if (current.nextLeft != null && i == -1) MoveNext(current.nextLeft);
        if (current.nextRight != null && i == 1) MoveNext(current.nextRight);
    }

    public override void StartPuzzle()
    {
        block = false;
        Start();
        print("Freeze ");
        walk.Freeze(true);
        MoveNext(grabs[0]);
        foreach (GrabWall g in grabs)
        {
            g.active = true;
        }
    }

    public override void UpdatePuzzle()
    {
        if (Input.GetKeyDown(KeyCode.S))
            MovePrevious();

        if (current.animalOut&&!fail) StartCoroutine(Fail());
    }

    private void Start()
    {
        grabs = GetComponentsInChildren<GrabWall>();
        GameObject player = GameObject.Find("Player");
        walk = player.GetComponent<Walk>();
    }

    void MoveNext(GrabWall g)
    {
        if (block) return;
        // todo animation
        g.previous = current;
        current = g;
        walk.SetPosition( current.transform.position- new Vector3(0,3,0));
        if (current.finalGrab) done = true;
    }

    void MovePrevious()
    {
        // todo animation
        if (current.previous == null) return;

        current = current.previous;
        walk.SetPosition(current.transform.position - new Vector3(0, 3, 0));
    }

    IEnumerator Fail()
    {
        fail = true;
        block = true;
        // todo animation
        yield return new WaitForSeconds(1);
        MovePrevious();
        block = false;
        fail = false;

    }
}
