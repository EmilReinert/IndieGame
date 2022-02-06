using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gears : Puzzle
{
    public GameObject player;
    public GameObject playergear;
    public Animator ani;
    public GameObject[] gears;
    int pointer;
    
    

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject == player)
        {
            if (playergear.activeSelf)
                PlaceGear();
        }

    }

    void PlaceGear()
    {
        gears[pointer].SetActive(true);
        playergear.SetActive(false);  // releasing gear

        GetComponent<AudioSource>().Play();
        ani.SetBool("Cfront", false);
        if (pointer >= gears.Length-1) { done = true; return; }
        pointer++;
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
