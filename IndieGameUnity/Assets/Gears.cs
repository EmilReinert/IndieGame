using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gears : MonoBehaviour
{
    public GameObject player;
    public GameObject playergear;

    public GameObject[] gears;
    int pointer;

    // Start is called before the first frame update
    void Start()
    {
        pointer = 0;
        foreach (GameObject g in gears)
            g.SetActive(false);
    }
    

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == player)
        {
            if (playergear.activeSelf)
                PlaceGear();
        }

    }

    void PlaceGear()
    {
        if (pointer >= gears.Length) return;
        gears[pointer].SetActive(true);
        pointer++;
        playergear.SetActive(false);  // releasing gear
    }

    
}
