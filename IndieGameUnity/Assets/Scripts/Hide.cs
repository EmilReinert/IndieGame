using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hide : Puzzle
{
    public GameObject player;
    

   int oneheld; // both hands need to be held. this stores whetehr one is held at the moment 

    bool fullhiding;

    Vector3 tempStartPos;
    Vector3 startPos;
    public override void EndPuzzle()
    {
        player.transform.localPosition = startPos;
    }

    public override void Move(int i)
    {
        if (i == 0)
            SetHide(i);
        else SetHide(i);

    }

    public override void StartPuzzle()
    {
        oneheld = 0; fullhiding = false;
        startPos = player.transform.localPosition;
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
                fullhiding = false;
                oneheld = 0;
                player.transform.localPosition = tempStartPos;
            }
        }
        else
        {
            if (oneheld==0)
            {
                // first depth
                //player.GetComponent<Walk>().enabled = true;
                tempStartPos = player.transform.localPosition;
                oneheld = i;
                player.transform.localPosition = tempStartPos - new Vector3(0, 1, 0);
            }
            if (oneheld!=i&&!fullhiding)
            {
                //second depth
                fullhiding = true;
                player.transform.localPosition = tempStartPos - new Vector3(0, 2, 0);
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
