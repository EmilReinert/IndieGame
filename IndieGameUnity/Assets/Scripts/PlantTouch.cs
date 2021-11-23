using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantTouch : MonoBehaviour
{
    public GameObject playerCollider;

    Animator ani;

    // Start is called before the first frame update
    void Start()
    {

        ani = GetComponentInChildren<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == playerCollider)
            ani.SetTrigger("play");
    }
}
