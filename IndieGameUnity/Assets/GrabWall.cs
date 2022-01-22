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
    private Animator ani;
    bool playing; // playing whole coroutine
    public bool animalOut;
    public bool active;

    private void Start()
    {
        active = false;
        playing = false;
        animalOut = false;
        animal = transform.Find("Animal").gameObject; // TODO ANIMATE
        if (animalAppears){
            animal.SetActive(true);
            ani = GetComponentInChildren<Animator>();
            ani.speed = 0;
        }
        else
            animal.SetActive(false);
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
            else { ani.speed = 0; }
        }
    }
    IEnumerator ShowAnimal(float time)
    {
        ani.SetBool("show", false);
        playing = true;
        yield return new WaitForSeconds(time);
        animalOut = true;
        if (ani.speed == 0) ani.speed = 1;
            ani.SetBool("show", true);
        yield return new WaitForSeconds(5);//playtime
        animalOut = false;
        playing = false;
    }
}
