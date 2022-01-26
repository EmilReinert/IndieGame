using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walk : MonoBehaviour
{
    public float walkSpeed;
    public GameObject
     rotationBody;
    public Animator main;
    [SerializeField]
    private bool freeze = false;

    public bool camDirection;
    private PlayerStickyCamera cam;
    
    
    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.FindObjectOfType<PlayerStickyCamera>();
        walkSpeed = 4;
        freeze = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (freeze)
        {
            main.SetBool("Cwalking", false); return;
        }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            Vector3 next = Vector3.zero;
            if (camDirection)
            {
                Vector2 nextStep = Vector2.zero;
                main.SetBool("Cwalking", true);

                if (Input.GetKey(KeyCode.W))
                    nextStep.x += 1;

                if (Input.GetKey(KeyCode.S))
                    nextStep.x -= 1;

                if (Input.GetKey(KeyCode.A))
                    nextStep.y -= 1;

                if (Input.GetKey(KeyCode.D))
                    nextStep.y += 1;
                float angle = Vector2.Angle(new Vector2(1,0), nextStep);

                nextStep = nextStep ;// * Time.deltaTime;
                next = new Vector3(nextStep.x, 0, nextStep.y);

                next = (Quaternion.Euler(0,angle,0) ) *new Vector3(1,0,0) ;
            }
            else
            {
                Vector2 nextStep = Vector2.zero;
                main.SetBool("Cwalking", true);

                if (Input.GetKey(KeyCode.W))
                    nextStep.y += 1;

                if (Input.GetKey(KeyCode.S))
                    nextStep.y -= 1;

                if (Input.GetKey(KeyCode.A))
                    nextStep.x -= 1;

                if (Input.GetKey(KeyCode.D))
                    nextStep.x += 1;

                nextStep = nextStep ;// * Time.deltaTime;
                next = new Vector3(nextStep.x, 0, nextStep.y);
            }
            StartCoroutine(
                A(next * walkSpeed));
        }
        else {
            StopAllCoroutines();
            main.SetBool("Cwalking", false);
            } 

        }
    public IEnumerator A(Vector3 nextstep, bool rotate =true)
        {
        Quaternion startRot = rotationBody.transform.rotation;
        Vector3 startPos = rotationBody.transform.position;

        

        float tParam = 0;
        while (tParam <= 1)
        {
            if (freeze && rotate) break;
            tParam += Time.deltaTime ;
            transform.position = Vector3.Lerp(startPos, startPos+nextstep+new Vector3(0,0,0), tParam);
            if(rotate) transform.rotation = Quaternion.Lerp(startRot, Quaternion.LookRotation(nextstep), tParam*3);

            yield return new WaitForEndOfFrame();
        }

    }
    public void Freeze(bool b)
    {

        if (b)
        {

            //freeze 
            StopAllCoroutines();    
            freeze = true;
            GetComponent<Rigidbody>().useGravity = false;
            GetComponent<Collider>().enabled = false;
        }
        else
        {

            //unfreeze 
            freeze = false;
            GetComponent<Rigidbody>().useGravity = true;
            GetComponent<Collider>().enabled = true;
        }
    }

    public void SetPosition(Vector3 pos)
    {

        StartCoroutine(
            A(pos-transform.position,false));
    }
}
