using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutScene : Puzzle
{
    GameObject player;
    Walk playerWalk;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        playerWalk = player.GetComponent<Walk>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void StartPuzzle()
    {
        playerWalk.Freeze(true);
    }

    public override void EndPuzzle()
    {
        playerWalk.Freeze(false);
    }

    public override void Move(int i)
    {
    }

    public override void UpdatePuzzle()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            done = true;
        }
    }
}
