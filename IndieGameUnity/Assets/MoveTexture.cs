using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTexture : MonoBehaviour
{
    
    public float moveSpeed = 1;

    private Renderer r;
    public  Vector2 modifier;
    private void Start()
    {
        r =
        GetComponent<Renderer>();
        
    }
    // Update is called once per frame
    void Update()
    {
        r.material.mainTextureOffset += modifier * Time.deltaTime * moveSpeed*0.01f;
        r.material.mainTextureOffset = new Vector2(r.material.mainTextureOffset.x % 1, r.material.mainTextureOffset.y % 1);
    }
}
