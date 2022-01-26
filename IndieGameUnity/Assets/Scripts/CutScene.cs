using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutScene : Puzzle
{
    GameObject player;
    Walk playerWalk;
    public Conversation talk;
    public string filePath;

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
        Start();
        talk.StartNew(filePath);
        playerWalk.Freeze(true);
        talk.continuous = true;
        talk.ReadNextLine(); // triggers full dialogue with continous = true
    }

    public override void EndPuzzle()
    {
        playerWalk.Freeze(false);
        talk.Reset();
    }

    public override void Move(int i)
    {
    }

    public override void UpdatePuzzle()
    {
        if (Input.GetKeyDown(KeyCode.Space)) done = true;
        if (talk.textOver) done = true;
    }
}
