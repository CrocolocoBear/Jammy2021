﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrototype : MonoBehaviour
{
    Rigidbody rb;
    public GameObject ring;
    public List<Transform> arms;
    private float velcoity;
    float rotation = 0;
    Vector3 normalVelocityX;
    Vector3 normalVelocityZ;
    Camera cam;
    bool throwing = false;
    Vector3 ringOGPos;
    float throwingSpeed = 50;
    
    private void Awake()
    {
        rb = this.GetComponent<Rigidbody>();
        cam = this.GetComponentInChildren<Camera>();
        ringOGPos = ring.transform.position - cam.transform.position;
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        PlayerInput();
        Camera();

        if(throwing) 
        {
           Throw();
        }
        else
        {
            if (throwingSpeed < 0)
            {
                throwingSpeed = 50;
            }
        }

        Debug.Log(ringOGPos);
    }


    private void PlayerInput()
    {
        Vector3 desiredVelocity = new Vector3(0, 0, 0);
        float speed = 0;

        if (Input.GetButton("Vertical") || Input.GetButton("Horizontal"))
        {
            for (int i = 0; i < arms.Count; i++)
            {
                arms[i].Rotate(200 * Time.deltaTime, 0, 0);
            }
            normalVelocityX = Input.GetAxis("Horizontal") * transform.right;
            normalVelocityZ = Input.GetAxis("Vertical") * transform.forward;
            speed = 5;
        }
        if (Input.GetAxis("Mouse X") != 0)
        {
            rotation += 2 * Input.GetAxis("Mouse X");
        }
        if(Input.GetMouseButtonDown(0))
        {           
            throwing = true;
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
        ring.transform.localPosition += new Vector3(0,0, throwingSpeed) * Time.deltaTime;

        if (Vector3.Distance(ring.transform.position, cam.transform.position) > 20)
        {
            Return();
        }

        if (Vector3.Distance(ring.transform.position, cam.transform.position + (cam.transform.forward * ringOGPos.z) + (cam.transform.up * ringOGPos.y)) <= 0.2)
        {
            ring.transform.position = cam.transform.position + (cam.transform.forward * ringOGPos.z) + (cam.transform.up * ringOGPos.y);
            throwingSpeed *= -1;
            throwing = false;
        }
    }

    private void Return()
    {
        throwingSpeed = -50;
    }

    private void Camera()
    {
        float rotSpeed = -2f;
        cam.transform.Rotate(rotSpeed * Input.GetAxis("Mouse Y"), 0, 0);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Return();
    }
}
