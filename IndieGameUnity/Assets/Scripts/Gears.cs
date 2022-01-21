using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gears : Puzzle
{
    public GameObject player;
    public GameObject playergear;

    public GameObject[] gears;
    int pointer;
    
    

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == player)
        {
            if (playergear.activeSelf)
                PlaceGear();
        }

    }

    void PlaceGear()
    {
        if (pointer >= gears.Length) { done = true; return; }
        gears[pointer].SetActive(true);
        pointer++;
        playergear.SetActive(false);  // releasing gear
    }

    public override void StartPuzzle()
    {
    }

    public override void EndPuzzle()
    {
    }

    public override void Move(int i)
    {
    }

    public override void UpdatePuzzle()
    {
    }
    private void Start()
    {

        pointer = 0;
        foreach (GameObject g in gears)
            g.SetActive(false);
    }
}
