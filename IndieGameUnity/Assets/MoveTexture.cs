using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTexture : MonoBehaviour
{
    public enum Mode {Linear, Bounce }
    public Mode mode;
    public float moveSpeed = 1;
    private Renderer r;
    public Vector2 modifier;
    int direction;
    private void Start()
    {
        r =
        GetComponent<Renderer>();
        direction = 1; // = up

    }
    // Update is called once per frame
    void Update()
    {
        switch (mode) {
            case Mode.Linear:
                r.material.mainTextureOffset += modifier * Time.deltaTime * moveSpeed * 0.01f;
                r.material.mainTextureOffset = new Vector2(r.material.mainTextureOffset.x % 1, r.material.mainTextureOffset.y % 1);

                break;
            case Mode.Bounce:
                r.material.mainTextureOffset += direction * modifier * Time.deltaTime * moveSpeed * 0.01f;
                if (r.material.mainTextureOffset.x >= 0.02f && direction == 1)
                    direction = -1;
                if (r.material.mainTextureOffset.x <= 0 && direction == -1)
                    direction = 1;

                break;

                    }
    }
}
