using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerPuzzle : Puzzle
{
    public Flower good;
    public Flower bad;

    public GameObject knife;
    public GameObject can;
    public bool last = false;

    private Animator ani; // player ani

    public override void EndPuzzle()
    {
        ani.SetBool("Choldboth", false);
        if (last) {
            can.SetActive(false);
            knife.SetActive(false); }

    }

    public override void Move(int i)
    {
        if (i == 0) return;
        if(i==-1) // cut
        {
            ani.SetTrigger("cut");
            //bad cuz good
            if (good.playerEntered)
                good.PlayIncorrectAnimation();
            //good cuz bad
            if (bad.playerEntered)
                bad.PlayCorrectAnimation();
        }
        if(i == 1) // water
        {
            //good cuz good
            ani.SetTrigger("water");
            if (good.playerEntered)
                good.PlayCorrectAnimation();
            //bad cuz bad
            if (bad.playerEntered)
                bad.PlayIncorrectAnimation();

            }
            if (good.done && bad.done) done = true;
    }

    public override void StartPuzzle()
    {
        ani.SetBool("Choldboth", true);
        can.SetActive(true);
        knife.SetActive(true);
        
    }

    public override void UpdatePuzzle()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        ani = GameObject.Find("Player").GetComponentInChildren<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
