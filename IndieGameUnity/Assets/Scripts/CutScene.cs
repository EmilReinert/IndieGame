using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutScene : Puzzle
{
    GameObject player;
    Walk playerWalk;
    public bool freezePlayer;
    public GameObject grandperson; // root
    public Conversation talk;
    private StayInframe frame;
    public string filePath;
    public bool disappearOld;
   public  AudioClip audi;
    
    public Animator ani;
    public string aniName;
    public bool resetAfter = false;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        playerWalk = player.GetComponent<Walk>();
        doneAnimation = false;

        talk = grandperson.GetComponentInChildren<Conversation>();
        if (ani != null && aniName != "" && aniName != null) 
        {  ani.SetBool(aniName, false); }
        frame = talk.GetComponent<StayInframe>();
        frame.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void StartPuzzle()
    {
        Start();
        grandperson.SetActive(true);
        grandperson.GetComponentInChildren<SkinnedMeshRenderer>().enabled = true;
        if (ani != null && aniName != "" && aniName != null) { ani.SetBool(aniName, true); }
        if (freezePlayer) playerWalk.Freeze(true, false);
        talk.StartNew(filePath);

        if (freezePlayer)
            talk.ReadNextLine(false); // triggers full dialogue with continous = true
        else talk.ReadNextLine(true);


        if (audi != null)
        {
            GetComponent<AudioSource>().clip = audi;
            GetComponent<AudioSource>().Play();
        }
        frame.enabled = true;
    }

    public override void EndPuzzle()
    {

        frame.enabled = false;
        talk.Reset();
        if (disappearOld)
            grandperson.SetActive(false);
        if (ani != null && resetAfter)        { ani.SetBool(aniName, false); }
        if (freezePlayer) playerWalk.Freeze(false);
    }

    public override void Move(int i)
    {
    }

    public override void UpdatePuzzle()
    {
        //if (Input.GetKeyDown(KeyCode.Space)) done = true;
        if (talk.textOver) done = true;
        
        if (Input.GetButtonDown("Fire3")|| Input.GetKeyDown(KeyCode.Space)) talk.ReadNextLine(false);
    }
}
