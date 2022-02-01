using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabWall : MonoBehaviour
{
    public GrabWall nextLeft;
    public GrabWall nextRight;
    [HideInInspector]
    public GrabWall previous;
    public bool finalGrab = false;
    public bool animalAppears = false;
    private GameObject animal;
    public Animator aniA;
    public Animator aniB;
     bool playing; // playing whole coroutine
    public bool animalOut;
    public bool active = false;

    private void Start()
    {
        //active = false;
        playing = false;
        animalOut = false;
        animal = transform.Find("sq").gameObject; // TODO ANIMATE
        if (animalAppears)
        {
            animal.SetActive(true);
            aniA.speed = 0;
            aniB.speed = 0;
        }
        else
            Destroy(animal);

        Vector3 look = transform.position - GameObject.Find("CenterSphere").transform.position;
        look.y = 0;
        transform.rotation = Quaternion.LookRotation(look);
    }
    private void Update()
    {
        if (animalAppears)
        {
            if (active)
            {
                float randomInterval = Random.value * 10 + 7;
                if (!playing)
                    StartCoroutine(ShowAnimal(randomInterval));
            }
            else { aniA.speed = 0; aniB.speed = 0; }
        }
    }
    IEnumerator ShowAnimal(float time)
    {
        aniA.SetBool("comeout", false);
        aniB.SetBool("show", false);
        playing = true;
        yield return new WaitForSeconds(time);
        animalOut = true;
        if (aniA.speed == 0) aniA.speed = 1;
        if (aniB.speed == 0) aniB.speed = 1;

        aniA.SetBool("comeout", true);
        aniB.SetBool("show", true);
        yield return new WaitForSeconds(3);//playtime
        animalOut = false;
        playing = false;
    }
}
