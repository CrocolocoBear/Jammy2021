using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrototype : MonoBehaviour
{
    Rigidbody rb;
    public List<Transform> arms;
    private float velcoity;
    float rotation = 0;
    Vector3 normalVelocityX;
    Vector3 normalVelocityZ;
    Camera cam;

    private void Awake()
    {
        rb = this.GetComponent<Rigidbody>();
        cam = this.GetComponentInChildren<Camera>();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        PlayerInput();
        Camera();
    }


    private void PlayerInput()
    {
        Vector3 desiredVelocity = new Vector3(0,0,0);
        float speed = 0;

        //if(Input.GetButton("Vertical"))
        //{
        //    desiredVelocity.z = 5 * Input.GetAxis("Vertical");
        //}
        //if (Input.GetButton("Horizontal"))
        //{
        //    desiredVelocity.x = 5 * Input.GetAxis("Horizontal");
        //}
        if (Input.GetButton("Vertical") || Input.GetButton("Horizontal"))
        {
            for(int i = 0; i < arms.Count; i++)
            {
                arms[i].Rotate(200 * Time.deltaTime,0,0);
            }
            normalVelocityX = Input.GetAxis("Horizontal") * transform.right;
            normalVelocityZ = Input.GetAxis("Vertical") * transform.forward;
            speed = 5;
        }
        if(Input.GetAxis("Mouse X") != 0)
        {
            rotation += 2 * Input.GetAxis("Mouse X");
        }
        
        desiredVelocity = (normalVelocityX + normalVelocityZ) * speed;
        Move(desiredVelocity, rotation);
        Debug.Log(desiredVelocity);
    }

    private void Move(Vector3 vel, float r)
    {
        rb.velocity = vel;
        rb.rotation = Quaternion.Euler(0, r, 0);
    }
    
    private void Camera()
    {
        float rotSpeed = -2f;
        cam.transform.Rotate(rotSpeed * Input.GetAxis("Mouse Y"), 0, 0);
    }
}
