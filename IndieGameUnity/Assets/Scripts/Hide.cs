using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hide : Puzzle
{
    public GameObject playerBody;
    private GameObject player;
    private Walk playerWalk;

    

   int oneheld; // both hands need to be held. this stores whetehr one is held at the moment 

    bool fullhiding;

    Vector3 tempStartPos;
    Vector3 startPos;
    public override void EndPuzzle()
    {
        playerBody.transform.localPosition = startPos;
        player.GetComponentInChildren<Animator>().SetBool("hide2", false);
        player.GetComponentInChildren<Animator>().SetBool("hide1", false);
    }

    public override void Move(int i)
    {
        if (i == 0)
            SetHide(i);
        else SetHide(i);

    }

    public override void StartPuzzle()
    {
        player = GameObject.Find("Player");
        playerWalk = player.GetComponent<Walk>();
        oneheld = 0; fullhiding = false;
        startPos = playerBody.transform.localPosition;
        playerWalk.Freeze(false);

        player.GetComponentInChildren<Animator>().SetBool("hide2", false);
        player.GetComponentInChildren<Animator>().SetBool("hide1", false);
    }

    public override void UpdatePuzzle()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        contiuous = true;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetHide(int i)
    {
        if (i==0)
        {
            if (oneheld!=0 || fullhiding)
            {
                // reset 
                //player.GetComponent<Walk>().enabled = true;
                playerWalk.Freeze(false);
                fullhiding = false;
                oneheld = 0;
                //playerBody.transform.localPosition = tempStartPos;


                player.GetComponentInChildren<Animator>().SetBool("hide2", false);
                player.GetComponentInChildren<Animator>().SetBool("hide1", false);
            }
        }
        else
        {
            if (oneheld==0)
            {
                // first depth
                //player.GetComponent<Walk>().enabled = true;
                playerWalk.Freeze(true);
                tempStartPos = playerBody.transform.localPosition;
                oneheld = i;
                //playerBody.transform.localPosition = tempStartPos - new Vector3(0, 1, 0);

                player.GetComponentInChildren<Animator>().SetBool("hide1",true);
                player.GetComponentInChildren<Animator>().SetBool("hide2", false);
            }
            if (oneheld!=i&&!fullhiding)
            {
                //second depth
                playerWalk.Freeze(true);
                fullhiding = true;
                //playerBody.transform.localPosition = tempStartPos - new Vector3(0, 2, 0);


                player.GetComponentInChildren<Animator>().SetBool("hide2", true);
                player.GetComponentInChildren<Animator>().SetBool("hide1", false);
            }
        }
    }
    public int GetHiding()
    {
        if (fullhiding) return 2;
        if (oneheld!=0) return 1;
        return 0;
    }
}
