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
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        cam = this.gameObject;
        startFOV = fov = cam.GetComponent<Camera>().fieldOfView;
        startOffset = offset = player.transform.position - cam.transform.position;
        cam.transform.parent = null;
        transition = false;
    }

     void Update()
    {
            StartCoroutine(
                Transition(
                    player.transform.position - offset));


    }
    public IEnumerator Transition(Vector3 targetPosition)
    {
        Vector3 startCamPosition = cam.transform.position;
        Quaternion startCamRotation = cam.transform.rotation;
        Quaternion targetRotation = Quaternion.LookRotation(player.transform.position - transform.position);

        float tempFOV = cam.GetComponent<Camera>().fieldOfView;

        float tParam = 0;
        while (tParam < 1)
        {
            tParam += Time.deltaTime * speedModifier;
            cam.transform.position = Vector3.Lerp(startCamPosition, targetPosition, tParam);
            cam.transform.rotation = Quaternion.LookRotation(player.transform.position - transform.position);// = Quaternion.Lerp(startCamRotation, targetRotation, tParam * 2);
            cam.GetComponent<Camera>().fieldOfView = Mathf.Lerp(tempFOV, fov, tParam);
            yield return new WaitForEndOfFrame();
        }
        transition = false;

    }
    public IEnumerator SetPosition(Vector3 targetPosition)
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
        offset = startOffset;
    }
}
