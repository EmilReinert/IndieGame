using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPattern : Puzzle
{
    private int[] pattern;
    bool ongoing;
    int IDX;
    float currentStartTime;
    float currentTime;
    float requiredInterval=0.5f;
    float errorTime=0.2f;

    public Drums drums;
    public LanternMove stick;
    public  ParticleSystem stickparticles;
    public GameObject door;

    public override void EndPuzzle()
    {
        print("done");
        var main = stickparticles.main;
        main.loop = true;ShowTrail(Color.yellow);
        door.GetComponent<Animator>().enabled = true;
    }

    public override void Move(int i)
    {
        if (i == 0) return;
        if (!ongoing) // official start after first input
        { // reset
            bool requiremnet2 = pattern[0] == i; //right tone
            IDX = 0;
            if (requiremnet2)
            {
                ongoing = true;
                ShowTrail(Color.green);
            }
            else
            {
                //play sound
                StopAllCoroutines();
                StartCoroutine(PlayPattern());
            }
        }
        else // actual following notes
        {
            float currentinterval = (Time.time - currentStartTime);
            float error = currentinterval - requiredInterval;
            bool requirement1 = Mathf.Abs(error) < errorTime; // right time
            bool requiremnet2 = pattern[IDX] == i; //right tone
            if ( requirement1&&requiremnet2 )
            {//note hit correctly
                if (IDX >= pattern.Length-1)
                {// done
                    done = true;
                }
                ShowTrail(Color.green);
            }
            else
            { //note hit incorrectly restarat
                ongoing = false;
                ShowTrail( Color.red);
                Move(1);
            }
        }
        currentStartTime = Time.time;
        IDX++;

    }

    public override void StartPuzzle()
    {
        pattern = new int[] { -1, 1, 1, -1, 1, 1 };
        ongoing = false;

        stickparticles = stick.GetComponentInChildren<ParticleSystem>();

        var main = stickparticles.main;
        main.loop = false;

        //play sound
        StartCoroutine(PlayPattern());


    }
    private void Start()
    {

        door.GetComponent<Animator>().enabled = false;
    }

    public override void UpdatePuzzle()
    {
        if (ongoing&&!done)
        {

            float currentinterval = (Time.time - currentStartTime);
            if (currentinterval - errorTime > requiredInterval)
            {
                ongoing = false;
                ShowTrail(Color.red);
                Move(1);
            }

        }
    }

    IEnumerator PlayPattern()
    {
        yield return new WaitForSeconds(2*requiredInterval);
        foreach (int i in pattern)
        {
            if (i == 1)
                drums.audi2.Play();
            else
                drums.audi1.Play();
            print("sound played");
            yield return new WaitForSeconds(requiredInterval);
        }
        yield return null;
    }

    void ShowTrail(Color c)
    {
        stickparticles.GetComponent<ParticleSystemRenderer>().trailMaterial.color = c;
        stickparticles.Play();
    }
    
}
