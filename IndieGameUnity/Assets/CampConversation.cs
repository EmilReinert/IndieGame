using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampConversation : Puzzle
{
    public GameObject campcharacters;
    public override void EndPuzzle()
    {
    }

    public override void Move(int i)
    {
    }

    public override void StartPuzzle()
    {
    }

    public override void UpdatePuzzle()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        foreach(Animator a in campcharacters.GetComponentsInChildren<Animator>())
        {
            a.SetBool("walk", false);
            a.SetBool("sitter", true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
