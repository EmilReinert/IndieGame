using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelPuzzle : Puzzle
{
    public GameObject barrelsContainer;
    private Barrel[] barrels;
    public bool barrelInside = true;

    public override void EndPuzzle()
    {
    }

    public override void Move(int i)
    {
    }

    public override void StartPuzzle()
    {
        Start();
    }

    public override void UpdatePuzzle()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        barrels = barrelsContainer.GetComponentsInChildren<Barrel>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerExit(Collider other)
    {
        bool allgone = true;
        if (barrels == null) return;
        foreach (Barrel b in barrels)
            if (Vector3.Distance(b.transform.position, transform.position) < transform.lossyScale.x / 2)
                allgone = false;
        if (allgone) done=true;
    }
}
