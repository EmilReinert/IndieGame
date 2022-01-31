using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walk : MonoBehaviour
{
    public float walkSpeed;
    private float startWalkSpeed;
    public GameObject
     rotationBody;
    public Animator main;
    [SerializeField]
    private bool freeze = false;

    private Emotions emo;

    public bool camDirection;
    private PlayerStickyCamera cam;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.FindObjectOfType<PlayerStickyCamera>();
        walkSpeed = 400;
        startWalkSpeed = walkSpeed;
        freeze = false;
        emo = GetComponentInChildren<Emotions>();
    }

    // Update is called once per frame
    void Update()
    {
        if (freeze)
        {
            main.SetBool("Cwalking", false); return;
        }

        Vector3 next = Vector3.zero;
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
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
                
                next = new Vector3(nextStep.x, 0, nextStep.y);
            next.Normalize();
            if (camDirection) next = Quaternion.LookRotation( cam.LookDir() )* next; // camera look direction
            float w = walkSpeed;
            if (Input.GetKey(KeyCode.LeftShift)) w *= 5; // speed buff
            StartCoroutine(A(next * w * Time.deltaTime));
        }
        else {
            main.SetBool("Cwalking", false);
            StopAllCoroutines();
            walkSpeed = startWalkSpeed;
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
            GetComponent<Collider>().enabled = false; // forgot why i did this
        }
        else
        {

            //unfreeze 
            freeze = false;
            GetComponent<Rigidbody>().useGravity = true;
            GetComponent<Collider>().enabled = true; // forgot why i did this
        }
    }
    public void Hurt()
    {
        StopCoroutine(SlowDown(3.0f));
        walkSpeed = startWalkSpeed;
        print("endhurting");

        StartCoroutine(SlowDown(3.0f));
    }

    IEnumerator SlowDown(float time)
    {
        walkSpeed =startWalkSpeed/ 5;
        emo.PlayHurt();
        print("hurting");
        yield return new WaitForSecondsRealtime(time);
            
        walkSpeed = startWalkSpeed;
        print("endhurting");


    }

    public void SetPosition(Vector3 pos)
    {

        StartCoroutine(
            A(pos-transform.position,false));
    }
}
