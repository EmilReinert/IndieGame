using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeasureParticle : MonoBehaviour
{
    // Particle Collider

    void OnParticleCollision(GameObject other)
    {
        print("hit");
        GameObject.FindObjectOfType<InsectStone>().Response();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
