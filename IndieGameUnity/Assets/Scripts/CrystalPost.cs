using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalPost : Puzzle
{
    public bool startPost=false;
    public CrystalPost next;
    public GameObject crystal;
    public GameObject ray;

    private bool glow;

    private int goalRotation;
    private int currentRotation;
    private int rotationRange; // number of directions it can be rotated to

    private Color startcolor;


    private LakeBehaviour[] lakes;

    public override void EndPuzzle()
    {

    }

    public override void Move(int i)
    {
        if (i != 0&& GetComponent<TriggerManager>().isEntered) // player is entered
            Rotate(i);
    }

    public override void StartPuzzle()
    {
        //turn on ray
        if (startPost)
        {
            ray.SetActive(true);
            StartGlow();
        }
        lakes = GameObject.FindObjectsOfType<LakeBehaviour>();
    }

    public override void UpdatePuzzle()
    {
        if (glow)
        {
            float distance;
            foreach (LakeBehaviour l in lakes)
            {
                distance = Vector3.Distance(l.transform.position, crystal.transform.position);
                float distanceMultiplyer = -0.07f * (distance - 5) + 5;
                l.decreaseByTime = false;
                l.IncreaseLight(distanceMultiplyer);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        hideObject = false;
        ray.SetActive(false);
        goalRotation = 1; /// manage correct rotation
        currentRotation = 0;
        rotationRange = 8;
        glow = false;

        startcolor = crystal.GetComponent<Renderer>().material.color;

    }
    

    public void StartGlow()
    {
        // TODO animate

        //turn on crystal
        crystal.GetComponent<Renderer>().material.color =new Color(229, 105, 0, 0.3f); // orange


        glow = true;

    }

    public void GlowOff()
    {

        glow = false;
        crystal.GetComponent<Renderer>().material.color = startcolor;

        foreach (LakeBehaviour l in lakes)
        {
            l.decreaseByTime = true;
        }
    }

    void Rotate(int dir )
    {
        if (!glow) return;
        transform.Rotate(0, dir *(360/rotationRange), 0);
        currentRotation += dir;
        if (currentRotation < 0) currentRotation = rotationRange - 1;
        if (currentRotation >= rotationRange) currentRotation = 0;

        if(currentRotation == goalRotation)
        {
            if (next != null)
            {
                next.StartGlow();
                next.ray.SetActive(true);
            }
        }
        else
        {
            if (next != null)
            {
                next.GlowOff();
                next.ray.SetActive(false);
            }
        }
           
    }
}
