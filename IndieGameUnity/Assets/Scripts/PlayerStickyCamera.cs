using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStickyCamera : MonoBehaviour
{
    private GameObject cambase;
    private Camera cam;
    public float stickDelay = 0.5f;
    public float speedModifier = 0.1f;
    public bool transition;
    public Vector3 offset;
    private Vector3 startOffset;
    private float startFOV;
    public  float fov;
    private GameObject player;
    public GameObject lookAt;

    public float rotation=0;

    public bool on;
    // Start is called before the first frame update
    void Start()
    {
        transform.parent = null; // detach from player
        rotation = 0;
        cam = GetComponentInChildren<Camera>();
        player = GameObject.Find("Player");
        cambase = this.gameObject;
        startFOV = fov = cam.fieldOfView;
        startOffset =cam.transform.localPosition ;
        offset = startOffset;
        //cam.transform.parent = null;
        transition = false;
        on = true;
        lookAt = player;

        // base transform 

    }

     void Update()
    {
        //if (Input.GetAxis("Mouse X") != 0)            cambase.transform.Rotate(0, Input.GetAxis("Mouse X"), 0);
        if (transition)
            StartCoroutine(
                Transition(lookAt.transform.position));
        else
        {
            if (!on) return;
            StartCoroutine(
                SetPosition(
                    lookAt.transform.position));
        }

    }
    private IEnumerator Transition(Vector3 targetPosition)
    {
        Vector3 startCamPosition = cambase.transform.position;
        Vector3 startCamPosition2 = cam.transform.position;

        float tempFOV = cam.fieldOfView;

        float tParam = 0;
        while (tParam < 1)
        {
            tParam += Time.deltaTime * speedModifier;
            cambase.transform.position = Vector3.Lerp(startCamPosition, targetPosition, tParam);
            cam.transform.position = Vector3.Lerp(startCamPosition2,transform.rotation * offset, tParam);
            cam.transform.rotation = Quaternion.LookRotation((lookAt.transform.position - cam.transform.position));// = Quaternion.Lerp(startCamRotation, targetRotation, tParam * 2);
            cam.fieldOfView = Mathf.Lerp(tempFOV, fov, tParam);
            yield return new WaitForEndOfFrame();
        }
        transition = false;

    }
    ///
    private IEnumerator SetPosition(Vector3 targetPosition)
    {
        yield return new WaitForSeconds(stickDelay);
        Vector3 startCamPosition = cambase.transform.position;
        Vector3 startCamPosition2 = cam.transform.position;
        

        float tempFOV = cam.fieldOfView;

        cambase.transform.position = targetPosition;
        cam.fieldOfView = fov;

        float tParam = 0;
        while (tParam <= 1)
        {
            tParam += Time.deltaTime * speedModifier;
            cambase.transform.position = Vector3.Lerp(startCamPosition, targetPosition, tParam);
            cam.transform.position = Vector3.Lerp(startCamPosition2, transform.rotation* offset , tParam);
            cam.transform.rotation = Quaternion.LookRotation((lookAt.transform.position - cam.transform.position));
            cam.fieldOfView = Mathf.Lerp(tempFOV, fov, tParam);
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
