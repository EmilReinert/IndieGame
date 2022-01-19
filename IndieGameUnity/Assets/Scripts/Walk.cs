using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walk : MonoBehaviour
{
    float walkSpeed;
    public GameObject
     rotationBody;
    [SerializeField]
    private bool freeze = false;
    
    
    // Start is called before the first frame update
    void Start()
    {
        walkSpeed = 500;
        freeze = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (freeze) return;

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            Vector2 nextStep = Vector2.zero;

            if (Input.GetKey(KeyCode.W))
                nextStep.y += 1;

            if (Input.GetKey(KeyCode.S))
                nextStep.y -= 1;

            if (Input.GetKey(KeyCode.A))
                nextStep.x -= 1;

            if (Input.GetKey(KeyCode.D))
                nextStep.x += 1;

            nextStep = nextStep * walkSpeed * Time.deltaTime;
            Vector3 next = new Vector3(nextStep.x, 0, nextStep.y);
            StartCoroutine(
                A(next));
        }
        else
            StopAllCoroutines();

    }
    public IEnumerator A(Vector3 nextstep, bool rotate =true)
        {
        Quaternion startRot = rotationBody.transform.rotation;
        Vector3 startPos = rotationBody.transform.position;

        

        float tParam = 0;
        while (tParam <= 1)
        {
            tParam += Time.deltaTime ;
            transform.position = Vector3.Lerp(startPos, startPos+nextstep+new Vector3(0,1,0), tParam);
            if(rotate) transform.rotation = Quaternion.Lerp(startRot, Quaternion.LookRotation(nextstep), tParam*3);

            yield return new WaitForEndOfFrame();
        }

    }
    public void Freeze(bool b)
    {
        if (b)
        {
            //freeze 
            freeze = true;
            GetComponent<Rigidbody>().useGravity = false;
        }
        else
        {

            //unfreeze 
            freeze = false;
            GetComponent<Rigidbody>().useGravity = true;
        }
    }

    public void SetPosition(Vector3 pos)
    {

        StartCoroutine(
            A(pos-transform.position,false));
    }
}
