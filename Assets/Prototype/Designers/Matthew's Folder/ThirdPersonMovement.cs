using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;
    private Vector3 playerVelocity;
    public float walkspeed = 6f;
    public float jumpSpeed = 8.0f;
    public float gravity = -9.81f;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    public GameObject mainCamera;
    public GameObject aimCamera;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (controller.isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = 0;
        }
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (Input.GetButtonDown("Jump") && controller.isGrounded)
        {
            playerVelocity.y += Mathf.Sqrt(jumpSpeed * -3.0f * gravity);
        }


        if (direction != Vector3.zero && !Input.GetMouseButton(1))
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            controller.Move(moveDir.normalized * walkspeed * Time.deltaTime);
        }
        playerVelocity.y += gravity * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        if (Input.GetMouseButton(1))
        {
            mainCamera.GetComponent<CinemachineFreeLook>().Priority = 9;
            Vector3 targetAimAngle = new Vector3(Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0f) * 100f * Time.deltaTime;
            targetAimAngle.z = 0f;
            gameObject.transform.GetChild(1).transform.Rotate(targetAimAngle);
        }
        else
        {
            mainCamera.GetComponent<CinemachineFreeLook>().Priority = 10000;
            gameObject.transform.GetChild(1).transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
