using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walk : MonoBehaviour
{

    public CharacterController controller;

    public float walkSpeed = 12f;
    public float gravity = -9.81f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundmask;

    Vector3 velocity;
    bool isGrounded;

    ///////////
    private float startWalkSpeed;
    public GameObject
     rotationBody;
    public Animator main;
    [SerializeField]
    private bool freeze = false;

    private Emotions emo;

    public bool camDirection;
    private PlayerStickyCamera cam;

    private RigidbodyConstraints constraints;

    private bool move;
    
    
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        cam = GameObject.FindObjectOfType<PlayerStickyCamera>();
        startWalkSpeed = walkSpeed;
        freeze = false;
        emo = GetComponentInChildren<Emotions>();
        //constraints = GetComponent<Rigidbody>().constraints;
    }

    // Update is called once per frame
    void Update()
    {
        if (freeze)
        {
            main.SetBool("Cwalking", false); return;
        }

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundmask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        Vector3 next = Vector3.zero;
        if (Input.GetAxis("Horizontal")!=0 ||Input.GetAxis("Vertical")!=0) {
            Vector2 nextStep = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            main.SetBool("Cwalking", true);
            

            next = new Vector3(nextStep.x, 0, nextStep.y);
            next.Normalize();
            if (camDirection) next = Quaternion.LookRotation(cam.LookDir()) * next; // camera look direction
            float w = walkSpeed;
            if (Input.GetButton("Fire3")) w *= 5; // speed buff
            //if (Input.GetKey(KeyCode.LeftShift)) w *= 5; // speed buff
            

            controller.Move(next * w*Time.deltaTime);


            // rotation

            StartCoroutine(A(next * w));
        }
        else
        {
            main.SetBool("Cwalking", false);
            StopAllCoroutines();
            walkSpeed = startWalkSpeed;
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
        /*
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
                    */

    }
    public IEnumerator A(Vector3 nextstep, bool rotate =true)
        {
        Quaternion startRot = rotationBody.transform.rotation;
        Vector3 startPos = rotationBody.transform.position;

        float i = 1;
        move = true;
            // roatesa bout float and direction
            float start = 0;
            //float direction = Mathf.Sign(i);
            int steps = 20; // animation steps;
            float increment = i / steps;
            Vector3 eulerRot = new Vector3(0, 0, 1);
            while (Mathf.Abs(start) <= Mathf.Abs(i))
            {
                start += Time.deltaTime * (i / steps) * 40;

            if (freeze && rotate) break;

            //transform.position = Vector3.Lerp(startPos, startPos + nextstep , start);
            if (rotate) transform.rotation = Quaternion.Lerp(startRot, Quaternion.LookRotation(nextstep), start);

            yield return new WaitForEndOfFrame();





            }

        move = false;
    }
    

    
    public IEnumerator A(Vector3 nextstep, Quaternion targetRot)
    {
        Quaternion startRot = rotationBody.transform.rotation;
        Vector3 startPos = rotationBody.transform.position;



        float tParam = 0;
        while (tParam <= 1)
        {

            tParam += Time.deltaTime/2;
            transform.position = Vector3.Lerp(startPos, startPos + nextstep + new Vector3(0, 0, 0), tParam);
            transform.rotation = Quaternion.Lerp(startRot, targetRot, tParam);

            yield return new WaitForEndOfFrame();
        }

    }

    public void Freeze(bool b, bool hideCol = true)
    {
        if (b)
        {

            //freeze 
            StopAllCoroutines();    
            freeze = true;
            //GetComponent<Rigidbody>().useGravity = false;
            if(hideCol)
                GetComponent<Collider>().enabled = false; // forgot why i did this
        }
        else
        {

            //unfreeze 
            freeze = false;
            //GetComponent<Rigidbody>().useGravity = true;
            GetComponent<Collider>().enabled = true; // forgot why i did this
        }
    }

    public void Drop(bool b)
    {

        if (b)
        {

            //freeze 
            StopAllCoroutines();
            freeze = true;
            //GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        }
        else
        {

            //unfreeze 
            freeze = false;
            //GetComponent<Rigidbody>().constraints = constraints;
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
    public void SetPosition(Vector3 pos, Quaternion rot)
    {

        StartCoroutine(
            A(pos - transform.position, rot));
    }
}
