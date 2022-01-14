using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollBarrel : MonoBehaviour
{
    public GameObject barrel;
    private GameObject player;
    public Vector3 moveDirection;
    private bool move;
    private float moveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        move = false;
        moveSpeed = 0.2f;
        
    }
    private void Update()
    {
        if (move)
        {
            barrel.GetComponent<Animator>().speed = 1;
            transform.Translate(moveDirection * Time.deltaTime * moveSpeed);
        }
        else
        {

            barrel.GetComponent<Animator>().speed = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            move = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
            move = false;
    }

}
