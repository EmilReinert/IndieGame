using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LakeBehaviour : MonoBehaviour
{
    [HideInInspector]
    private GameObject player;
    public bool decreaseByTime;
    public GameObject lantern;
    public GameObject playerRoot;
    public GameObject[] sublakes;

    public float increaseSpeed = 2;
    public float decreaseSpeed = 0.002f;

    private float maxIntensity = 1;
    private float minIntensity = 0;

    private bool isColliding;
    private bool lightOn;
    private Renderer ren;
    private Material[] mat;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        lightOn = false;
        isColliding = false;
        ren = GetComponent<Renderer>();
        mat = new Material[sublakes.Length + 1];
        mat[0] = ren.material;
        for (int i = 1; i < mat.Length; i++)
        {
            mat[i] = sublakes[i - 1].GetComponent<Renderer>().material;
        }
        SetAlpha(minIntensity);
        decreaseByTime = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (decreaseByTime && lightOn && !isColliding)
        {
            StartCoroutine(DecreaseLight());
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
            float distance = Vector3.Distance(playerRoot.transform.position, transform.position);
            float distanceMultiplyer = -0.007f * (distance - 10) + 1.5f;
            IncreaseLight(distanceMultiplyer);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == lantern)
            isColliding = false;
    }

    public void IncreaseLight(float distanceMultiplyer)
    {
        lightOn = true;


        StartCoroutine(LightOn( distanceMultiplyer));

    }

    IEnumerator LightOn(float distanceMultiplyer )
    {
        

        if (distanceMultiplyer <= 0) distanceMultiplyer = 0;
        float maxBright = distanceMultiplyer * maxIntensity;
        
        if (mat[0].GetFloat("_A2") >= maxBright)
        {
            SetAlpha(maxBright,true);
        }
        else
        {
            lightOn = true;
            SetAlpha(mat[0].GetFloat("_A2") + increaseSpeed * Time.deltaTime,true);
            yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator DecreaseLight()
    {
        if (mat[0].GetFloat("_A2") <= minIntensity)
        {

            SetAlpha(minIntensity); lightOn = false;
        }
        else
        {
            SetAlpha(mat[0].GetFloat("_A2") * (1 - decreaseSpeed * Time.deltaTime));
        }
        yield return new WaitForEndOfFrame();
    }

    void SetAlpha(float goal, bool a = false) // a always increases
    {/*
        Color tempCol = mat[0].color;
        tempCol.a =goal;
        foreach(Material m in mat)
            m.color = tempCol;*/

        if (goal > 1) return;
        if (a)
        {
            mat[0].SetFloat("_A2", Mathf.Max(goal,mat[0].GetFloat("_A2")));
            foreach (Material m in mat)
                m.SetFloat("_A2", Mathf.Max(goal, mat[0].GetFloat("_A2")));
        }
        else
        {
            mat[0].SetFloat("_A2", goal);
            foreach (Material m in mat)
                m.SetFloat("_A2", goal);
        }
    }
}
