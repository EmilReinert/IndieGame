using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsectStone : Puzzle
{
    public GameObject insects;
    public GameObject insectStone;
    public ToxicPersonBehavior t;
    public override void EndPuzzle()
    {
        insects.SetActive(false);
        insectStone.SetActive(false);
    }

    public override void Move(int i)
    {
    }

    public override void StartPuzzle()
    {
        insects.SetActive(true);
        insectStone.SetActive(true);
        t.walkAnyway = true;
    }

    public override void UpdatePuzzle()
    {
    }
    private void Start()
    {
        
        insects.SetActive(false);
        insectStone.SetActive(false);
    }
}
