using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStickyCamera : MonoBehaviour
{
    public GameObject cam;
    public float stickDelay = 0.5f;
    private Vector3 offset;
    private float speedModifier;
    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - cam.transform.position;
        cam.transform.parent = null;
        speedModifier = 0.1f;
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(
        UpdateCamPos(stickDelay));
    }
    public IEnumerator UpdateCamPos(float delay)
    {
        yield return new WaitForSeconds(delay);
        Vector3 startCamPosition = cam.transform.position;
        Vector3 targetPosition = transform.position - offset;
        float tParam = 0;
        while (tParam < 1)
        {
            tParam += Time.deltaTime * speedModifier;
            cam.transform.position = Vector3.Lerp(startCamPosition, targetPosition, tParam);
            yield return new WaitForEndOfFrame();
        }
    }
}
