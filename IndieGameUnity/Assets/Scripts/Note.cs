using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    public GameObject testtube;
    private Renderer[] r;
    public bool done; // fail
    public bool hit;

    // Start is called before the first frame update
    private void Start()
    {
        r = GetComponentsInChildren<Renderer>();
        foreach (Renderer rs in r)
            rs.enabled = false;
        done = false;
        hit = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name =="body")
            foreach (Renderer rs in r)
                rs.enabled = true;

        if (other.gameObject.name == "goal")
            hit = true;
        if (other.gameObject.name == "end"&&!hit)
            done = true;

    }
}
