using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlRingVersion : MonoBehaviour
{
    Rigidbody rb;
    Rigidbody ringRb;
    public GameObject ring;
    public GameObject ringCase;
    //public List<Transform> arms;
    private float velocity;
    float rotation = 0;
    Vector3 normalVelocityX;
    Vector3 normalVelocityZ;
    Camera cam;
    bool throwing = false;
    bool ringThrown = false;
    bool retrieving = false;
    bool grabbing = false;
    Vector3 ringCasePos;
    Vector3 ringOGPos;
    float throwingSpeed = 20;
    SphereCollider triggerSphereRef;
    public GameObject charRing;
    Animator walkAnimator;
    int stickCheck = 1;
    float yCamOffset = 0;
    int requeiredKeys = 0;
    Vector3 checkLastContact = Vector3.zero;

    public Rigidbody GetRBRing()
    {
        return ringRb;
    }
    public void SetStickVars(int setCheck, float setRot, int setKeys)
    {
        stickCheck = setCheck;
        yCamOffset = setRot;
        requeiredKeys = setKeys;
    }
    private void Awake()
    {
        Application.targetFrameRate = 60;
        rb = this.GetComponent<Rigidbody>();
        cam = this.GetComponentInChildren<Camera>();
        ringCase.transform.localPosition = new Vector3(0, 1, 2);
        ringCasePos = ringCase.transform.position;
        ring.transform.position = ringCasePos;
        //ringCasePos = ring.transform.parent.position;
        ringOGPos = ring.transform.localPosition;
        ringRb = ring.GetComponent<Rigidbody>();
        triggerSphereRef = GetComponent<SphereCollider>();
        ring.SetActive(false);
        walkAnimator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        PlayerInput();
        Camera();

        rb.AddForce(Vector3.down * 5, ForceMode.Impulse);

        //if (throwing && ringThrown == false)
        //{
        //    Throw();
        //    ringThrown = true;
        //}
        if (throwing)
        {
            if (ringThrown == false)
            {
                Throw();
            }
            //ringThrown = true;
        }
        //if (ringThrown && retrieving)
        //{
        //    Retrieve();
        //    grabbing = true;
        //    retrieving = false;
        //}

        /*
        else
        {
            if (throwingSpeed < 0)
            {
                throwingSpeed = 50;
            }
        }
        */

    }


    private void PlayerInput()
    {
        Vector3 desiredVelocity = new Vector3(0, 0, 0);
        float speed = 0;

        if (Input.GetButton("Vertical") || Input.GetButton("Horizontal"))
        {
            /*
            for (int i = 0; i < arms.Count; i++)
            {
                arms[i].Rotate(200 * Time.deltaTime, 0, 0);
            }
            */
            normalVelocityX = Input.GetAxis("Horizontal") * transform.right;
            normalVelocityZ = Input.GetAxis("Vertical") * transform.forward;
            speed = 10;
            if (!walkAnimator.GetBool("walkCheck"))
            {
                walkAnimator.SetBool("walkCheck", true);
            }
        }
        else
        {
            if (walkAnimator.GetBool("walkCheck"))
            {
                walkAnimator.SetBool("walkCheck", false);
            }
        }
        if (Input.GetAxis("Mouse X") != 0)
        {
            rotation += 2 * Input.GetAxis("Mouse X");
        }
        if (Input.GetMouseButtonDown(0))
        {
            throwing = true;
            walkAnimator.SetTrigger("throwTrigger");
        }
        if (Input.GetMouseButton(1))
        {
            //retrieving = true;
            Retrieve();
        }

        desiredVelocity = (normalVelocityX + normalVelocityZ) * speed;
        Move(desiredVelocity, rotation);
    }

    private void Move(Vector3 vel, float r)
    {
        rb.velocity = vel * stickCheck;
        rb.rotation = Quaternion.Euler(0, (r * stickCheck) + yCamOffset, 0);
    }

    private void Throw()
    {
        charRing.SetActive(false);
        ring.SetActive(true);
        ringRb.isKinematic = false;
        //ringRb.useGravity = true;
        ringRb.velocity += cam.transform.forward.normalized * throwingSpeed;
        throwing = false;
        ringThrown = true;
        //StartCoroutine("throwingCoutdown");
    }
    private void Retrieve()
    {
        if (triggerSphereRef.enabled == false)
        {
            triggerSphereRef.enabled = true;
        }
        ringCasePos = ringCase.transform.position;
        //ringCasePos = ring.transform.parent.position;
        //ringRb.useGravity = false;
        if (GetComponent<KeyPickup>().GetKeys() - requeiredKeys >= 0 )
        {
            if (ring.activeSelf == true && checkLastContact != GetComponentInChildren<StickRing>().GetPointOfCollision())
            {
                this.transform.position += (GetComponentInChildren<StickRing>().GetPointOfCollision() - this.transform.position).normalized * throwingSpeed * Time.deltaTime;
                ring.transform.position = GetComponentInChildren<StickRing>().GetPointOfCollision();
                walkAnimator.SetBool("climbCheck", true);
            }
        }
        else
        {
            ringRb.constraints = RigidbodyConstraints.None;
            ringRb.velocity += new Vector3(ringCasePos.x - ring.transform.position.x, ringCasePos.y - ring.transform.position.y, ringCasePos.z - ring.transform.position.z).normalized * throwingSpeed * Time.deltaTime;   
        }
    }
    private void Camera()
    {
        float rotSpeed = -2f;
        cam.transform.Rotate(rotSpeed * Input.GetAxis("Mouse Y"), 0, 0);
    }

    private void GrabRing()
    {
        walkAnimator.SetBool("climbCheck", false);
        checkLastContact = GetComponentInChildren<StickRing>().GetPointOfCollision();
        ring.transform.localPosition = ringOGPos;
        ring.transform.eulerAngles = new Vector3(90, 0, 0);
        ringRb.isKinematic = true;
        throwing = false;
        ringThrown = false;
        triggerSphereRef.enabled = false;
        charRing.SetActive(true);
        ring.SetActive(false);
        ringRb.constraints = RigidbodyConstraints.None;
        stickCheck = 1;
        yCamOffset = 0;
        //GetComponentInChildren<StickRing>().SetPointOfCollision(null);
    }

    private void OnTriggerEnter(Collider other)
    {
        //if (ringThrown && grabbing)
        //{
        //    GrabRing();
        //}

        if (other.tag == "RingThrow")
        {
            GrabRing();
        }

        if (other.tag == "climbing")
        {
            if (ring.activeSelf == false)
            {
                rb.AddForce(Vector3.up * 300, ForceMode.Impulse);
            }
            //rb.AddForce(cam.transform.forward * 200, ForceMode.Impulse);
        }
    }
}
