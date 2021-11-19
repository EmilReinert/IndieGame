using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LakeBehaviour : MonoBehaviour
{
    public GameObject lantern;
    public GameObject playerRoot;
    public float increaseSpeed = 1;
    public float decreaseSpeed = 0.002f;
    public float distance;
    private float maxIntensity = 0.4f;
    
    private bool isColliding;
    private bool lightOn;
    private Renderer ren;
    private Material mat;
    // Start is called before the first frame update
    void Start()
    {
        lightOn = false;
        isColliding = false;
        ren = GetComponent<Renderer>();
        mat = ren.material;
        SetAlpha(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (lightOn&&!isColliding)
        {
            StartCoroutine( DecreaseLight());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == lantern)
            isColliding = true;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject == lantern)
        {
            StartCoroutine(IncreaseLight());
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == lantern)
            isColliding = false;
    }
    IEnumerator IncreaseLight()
    {
        if (mat.color.a >= maxIntensity)
        {
            SetAlpha(maxIntensity);
            yield return new WaitForEndOfFrame();
        }
        else
        {
            lightOn = true;
            float distanceMultiplyer = Vector3.Distance(playerRoot.transform.position, transform.position);
            distanceMultiplyer = -0.009f * distanceMultiplyer + 1;
            distance = mat.color.a;
            if (distanceMultiplyer <= 0) distanceMultiplyer = 0;
            float maxBright = distanceMultiplyer * maxIntensity;
            if (mat.color.a >= maxBright)
            {
                SetAlpha(maxBright);
                yield return new WaitForEndOfFrame();
            }
            else
            {
                SetAlpha(mat.color.a + increaseSpeed * Time.deltaTime);
                yield return new WaitForEndOfFrame();
            }
        }
    }
    IEnumerator DecreaseLight()
    {
        if (mat.color.a <= 0.004)
        {

            SetAlpha(0);  lightOn = false;
        }
        else
        {
            SetAlpha(mat.color.a * (1 - decreaseSpeed * Time.deltaTime));
        }
        yield return new WaitForEndOfFrame();
    }

    void SetAlpha(float goal)
    {
        Color tempCol = mat.color;
        tempCol.a =goal;
        mat.color = tempCol;
    }
}
