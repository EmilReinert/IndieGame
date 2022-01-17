using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSpin : Puzzle
{
    public GameObject rotationStick;

    float fireTime;
    float requiredFireTime;

    float axil;
    float speedModifier;

    public override void EndPuzzle()
    {

    }

    public override void Move(int i)
    {if (i != 0)
            Spin(i);
    }

    public override void StartPuzzle()
    {
        contiuous = true;
        axil = 0;
        speedModifier = 2;
    }

    public override void UpdatePuzzle()
    {
        float size = fireTime / requiredFireTime;

        rotationStick.transform.Rotate(0,axil , 0);

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Spin(int i)
    {
        axil += speedModifier * i*Time.deltaTime;
    }
}
