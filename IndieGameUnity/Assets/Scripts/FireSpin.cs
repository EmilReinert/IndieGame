using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSpin : Puzzle
{
    public GameObject rotationStick;
    public GameObject rightHand;
    public GameObject leftHand;
    public GameObject effect1;
    public GameObject effect2;

    private Walk player;
    private Animator playerani;

    public Vector3 startRight;
    public Vector3 startLeft;

    float fireTime;
    float requiredFireTime;

    float axil;
    float speedModifier;

    float playtime; // time in seconds that the spin is fast enough

    public override void EndPuzzle()
    {

        effect1.SetActive(false);
        effect2.SetActive(true);
        playerani.SetBool("Cfire", false);
        player.Freeze(false);
        rotationStick.SetActive(false);


    }

    public override void Move(int i)
    {
        if (i != 0)
            Spin(i);
    }

    public override void StartPuzzle()
    {
        contiuous = true;
        axil = 0;
        speedModifier = 0.5f;
        playtime = 0;
        effect1.SetActive(true);
        effect2.SetActive(false);
        playerani.SetBool("Cfire", true);

        player.Freeze(true);
        rotationStick.SetActive(true);
    }

    public override void UpdatePuzzle()
    {
        if (done) return;
        float size = fireTime / requiredFireTime;
        int maxRot = 170;
        float nextRot = axil + rotationStick.transform.eulerAngles.y%360;
        if (nextRot < 360-maxRot && nextRot > maxRot)
        {
            // stop rotation
            //axil = 0;
        }
        else
            rotationStick.transform.Rotate(0,axil , 0);

        //hand positions
        float moveFrame = 0.5f;
        float handPosition;

        if (rotationStick.transform.eulerAngles.y < 180)
            handPosition = moveFrame * (rotationStick.transform.eulerAngles.y / maxRot);
        else
            handPosition =- moveFrame * ((360-rotationStick.transform.eulerAngles.y) / maxRot);
        playerani.SetFloat("fire", handPosition + 0.5f);

        Vector3 r = startRight; r.z += handPosition;
        rightHand.transform.localPosition = r;

        Vector3 l = startLeft; l.z -= handPosition;
        leftHand.transform.localPosition = l;

        //print(axil/ Time.deltaTime);

        // end puzzle
        float minAxil = Time.deltaTime * 300;
        if (Mathf.Abs(axil) > minAxil)
            playtime += Time.deltaTime;
        else // decrease
            playtime -= Time.deltaTime;
        if (playtime < 0) playtime = 0;
        //print(playtime);
        if (playtime > 5) done = true;

        //fire intensity
        effect1.transform.localScale = new Vector3(1, 1, 1) * (playtime/5);

    }

    // Start is called before the first frame update
    void Start()
    {
        startLeft = leftHand.transform.localPosition;
        startRight = rightHand.transform.localPosition;
        
        effect2.SetActive(false);
        effect1.SetActive(false);

        player = GameObject.FindObjectOfType<Walk>();
        playerani = GameObject.Find("Player").GetComponentInChildren<Animator>();


        rotationStick.SetActive(false);
    }

    void Spin(int i)
    {
        if (Mathf.Sign(i) == Mathf.Sign(axil))
        {
            axil += speedModifier * i * Time.deltaTime;
        }
        else
        {
            axil = -axil+ speedModifier * i * Time.deltaTime; ;
        }
        float maxAxil = Time.deltaTime * 600;
        if (axil > maxAxil) axil = maxAxil;
        if (axil < -maxAxil) axil = -maxAxil;
    }
}
