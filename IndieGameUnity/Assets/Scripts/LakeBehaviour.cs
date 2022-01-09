using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LakeBehaviour : MonoBehaviour
{
    public GameObject lantern;
    public GameObject playerRoot;
    public GameObject[] sublakes;

    public float increaseSpeed = 2;
    public float decreaseSpeed = 0.002f;
    public float distance;
    private float maxIntensity = 1;
    private float minIntensity = 0;

    private bool isColliding;
    private bool lightOn;
    private Renderer ren;
    private Material[] mat;
    // Start is called before the first frame update
    void Start()
    {
        lightOn = false;
        isColliding = false;
        ren = GetComponent<Renderer>();
        mat = new Material[sublakes.Length+1];
        mat[0] = ren.material;
        for (int i = 1; i < mat.Length; i++)
        {
            mat[i] = sublakes[i-1].GetComponent<Renderer>().material;
        }
        SetAlpha(minIntensity);
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
        if (mat[0].GetFloat("_A2") >= maxIntensity)
        {
            SetAlpha(maxIntensity);
            yield return new WaitForEndOfFrame();
        }
        else
        {
            lightOn = true;
            float distanceMultiplyer = Vector3.Distance(playerRoot.transform.position, transform.position);
            distanceMultiplyer = -0.009f * distanceMultiplyer + 1;
            distance = mat[0].GetFloat("_A2"); // mat[0].color.a;
            if (distanceMultiplyer <= 0) distanceMultiplyer = 0;
            float maxBright = distanceMultiplyer * maxIntensity;
            if (mat[0].GetFloat("_A2") >= maxBright)
            {
                SetAlpha(maxBright);
                yield return new WaitForEndOfFrame();
            }
            else
            {
                SetAlpha(mat[0].GetFloat("_A2") + increaseSpeed * Time.deltaTime);
                yield return new WaitForEndOfFrame();
            }
        }
    }
    IEnumerator DecreaseLight()
    {
        if (mat[0].GetFloat("_A2") <= minIntensity)
        {

            SetAlpha(minIntensity);  lightOn = false;
        }
        else
        {
            SetAlpha(mat[0].GetFloat("_A2") * (1 - decreaseSpeed * Time.deltaTime));
        }
        yield return new WaitForEndOfFrame();
    }

    void SetAlpha(float goal)
    {/*
        Color tempCol = mat[0].color;
        tempCol.a =goal;
        foreach(Material m in mat)
            m.color = tempCol;*/
        mat[0].SetFloat("_A2",goal);
        foreach (Material m in mat)
            m.SetFloat("_A2", goal);
    }
}
