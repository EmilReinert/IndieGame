using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartLvl : MonoBehaviour
{
    public GameObject menu;
    public GameObject s;
    public GameObject e;
    // Start is called before the first frame update
    void Start()
    {
        menu.SetActive(false);
        s.GetComponent<Text>().enabled = false;
        e.GetComponent<Text>().enabled = false;
        StartCoroutine(Hi());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator Hi()
    {
        yield return new WaitForSeconds(15);
        menu.SetActive(true);
        yield return new WaitForSeconds(2);
        s.GetComponent<Text>().enabled = true;
        e.GetComponent<Text>().enabled = true;
    }
}
