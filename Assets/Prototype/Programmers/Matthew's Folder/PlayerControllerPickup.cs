﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerPickup : MonoBehaviour
{
    Rigidbody rb;
    Rigidbody ringRb;
    public GameObject ring;
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

    private void Awake()
    {
        Application.targetFrameRate = 60;
        rb = this.GetComponent<Rigidbody>();
        cam = this.GetComponentInChildren<Camera>();
        ringCasePos = ring.transform.parent.position;
        ringOGPos = ring.transform.localPosition;
        ringRb = ring.GetComponent<Rigidbody>();
        triggerSphereRef = GetComponent<SphereCollider>();
        ring.SetActive(false);
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        PlayerInput();
        Camera();

        //if (throwing && ringThrown == false)
        //{
        //    Throw();
        //    ringThrown = true;
        //}
        if (throwing)
        {
            Throw();
         
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
            speed = 5;
        }
        if (Input.GetAxis("Mouse X") != 0)
        {
            rotation += 2 * Input.GetAxis("Mouse X");
        }
        if (Input.GetMouseButtonDown(0))
        {
            throwing = true;
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
        rb.velocity = vel;
        rb.rotation = Quaternion.Euler(0, r, 0);
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
        ringCasePos = ring.transform.parent.position;
        //ringRb.useGravity = false;
        ringRb.velocity += new Vector3(ringCasePos.x - ring.transform.position.x, ringCasePos.y - ring.transform.position.y, ringCasePos.z - ring.transform.position.z) * throwingSpeed * Time.deltaTime;
    }
    private void Camera()
    {
        float rotSpeed = -2f;
        cam.transform.Rotate(rotSpeed * Input.GetAxis("Mouse Y"), 0, 0);
    }

    private void GrabRing()
    {
        ring.transform.localPosition = ringOGPos;
        ring.transform.eulerAngles = new Vector3(90, 0, 0);
        ringRb.isKinematic = true;
        throwing = false;
        ringThrown = false;
        triggerSphereRef.enabled = false;
        charRing.SetActive(true);
        ring.SetActive(false);
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
    }

    IEnumerable throwingCoutdown()
    {
        yield return new WaitForSeconds(0.1f);
        triggerSphereRef.enabled = true;
    }
}
