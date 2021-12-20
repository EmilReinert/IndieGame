using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStickyCamera : MonoBehaviour
{
    private GameObject cam;
    public float stickDelay = 0.5f;
    public float speedModifier = 0.1f;

    public Vector3 offset;
    private Vector3 startOffset;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        cam = this.gameObject;
        startOffset = offset = player.transform.position - cam.transform.position;
        cam.transform.parent = null;
    }

     void Update()
    {

        StartCoroutine(
            UpdateCamPos(
                player.transform.position - offset
                ));

    }
    public IEnumerator UpdateCamPos(Vector3 targetPosition)
    {
        yield return new WaitForSeconds(stickDelay);
        Vector3 startCamPosition = cam.transform.position;
        Quaternion startCamRotation = cam.transform.rotation;
        Quaternion targetRotation = Quaternion.LookRotation( player.transform.position-transform.position );
        float tParam = 0;
        while (tParam < 1)
        {
            tParam += Time.deltaTime * speedModifier;
            cam.transform.position = Vector3.Lerp(startCamPosition, targetPosition, tParam);
            cam.transform.rotation = Quaternion.Lerp(startCamRotation, targetRotation , tParam);
            yield return new WaitForEndOfFrame();
        }
    }

    public void ResetOffset()
    {
        offset = startOffset;
    }
}
