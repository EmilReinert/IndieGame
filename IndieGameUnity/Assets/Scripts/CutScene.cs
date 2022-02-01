using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutScene : Puzzle
{
    GameObject player;
    Walk playerWalk;
    public GameObject grandperson;
    public Conversation talk;
    public string filePath;
    public bool disappearOld;
    
    public Animator ani;
    public string aniName;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        playerWalk = player.GetComponent<Walk>();
        doneAnimation = false;

        talk = grandperson.GetComponentInChildren<Conversation>();
        if (ani != null) 
        { ani.speed = 0; ani.SetBool(aniName, false); }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void StartPuzzle()
    {
        Start();
        if (filePath == null || filePath == "") done = true;
        talk.StartNew(filePath);
        playerWalk.Freeze(true);
        talk.continuous = true;
        talk.ReadNextLine(); // triggers full dialogue with continous = true

        if (ani != null) { ani.speed = 1;ani.SetBool(aniName, true); }
    }

    public override void EndPuzzle()
    {
        playerWalk.Freeze(false);
        talk.Reset();
        if (disappearOld)
            grandperson.SetActive(false);
        if (ani != null)
        {ani.SetBool(aniName, false); }
    }

    public override void Move(int i)
    {
    }

    public override void UpdatePuzzle()
    {
        if (Input.GetKeyDown(KeyCode.Space)) done = true;
        if (talk.textOver) done = true;
        
        if (Input.GetButtonDown("Fire3")) talk.ReadNextLine();
    }
}
