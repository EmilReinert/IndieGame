using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStickyCamera : MonoBehaviour
{
    private GameObject cam;
    public float stickDelay = 0.5f;
    public float speedModifier = 0.1f;
    public bool transition;
    public Vector3 offset;
    private Vector3 startOffset;
    private float startFOV;
    public  float fov;
    private GameObject player;
    public GameObject lookAt;

    public bool on;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        cam = this.gameObject;
        startFOV = fov = cam.GetComponent<Camera>().fieldOfView;
        startOffset =player.transform.position- cam.transform.position ;
        offset = player.transform.position - startOffset    ;
        cam.transform.parent = null;
        transition = false;
        on = false;
        lookAt = player;
    }

     void Update()
    {
        if (transition)
            StartCoroutine(
                Transition(offset));
        else
        {
            if (!on) return;
            StartCoroutine(
                SetPosition(
                    offset));
        }

    }
    private IEnumerator Transition(Vector3 targetPosition)
    {
        Vector3 startCamPosition = cam.transform.position;
        Quaternion startCamRotation = cam.transform.rotation;
        Quaternion targetRotation = Quaternion.LookRotation((lookAt.transform.position + new Vector3(0,5,0)) - transform.position); // slightly above head

        float tempFOV = cam.GetComponent<Camera>().fieldOfView;

        float tParam = 0;
        while (tParam < 1)
        {
            tParam += Time.deltaTime * speedModifier;
            cam.transform.position = Vector3.Lerp(startCamPosition, targetPosition, tParam);
            cam.transform.rotation = Quaternion.LookRotation((lookAt.transform.position + new Vector3(0, 5, 0)) - transform.position);// = Quaternion.Lerp(startCamRotation, targetRotation, tParam * 2);
            cam.GetComponent<Camera>().fieldOfView = Mathf.Lerp(tempFOV, fov, tParam);
            yield return new WaitForEndOfFrame();
        }
        transition = false;

    }
    ///
    private IEnumerator SetPosition(Vector3 targetPosition)
    {
        yield return new WaitForSeconds(stickDelay);
        Vector3 startCamPosition = cam.transform.position;
        

        float tempFOV = cam.GetComponent<Camera>().fieldOfView;
        
            cam.transform.position = targetPosition;
        cam.GetComponent<Camera>().fieldOfView = fov;

        float tParam = 0;
        while (tParam <= 1)
        {
            tParam += Time.deltaTime * speedModifier;
            cam.transform.position = Vector3.Lerp(startCamPosition, targetPosition, tParam);
            cam.GetComponent<Camera>().fieldOfView = Mathf.Lerp(tempFOV, fov, tParam);
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForEndOfFrame();
    }

    public void ResetOffset()
    {
        fov = startFOV;
        offset =player.transform.position -startOffset;
        lookAt = player;
    }

    public void Enable(bool b)
    {
        on = b;
    }
}
