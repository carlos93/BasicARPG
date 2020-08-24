using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    public float jumpHeight = 1.0f;
    public float gravityValue = -9.81f;
    
    private Vector3 playerVelocity;

    public CharacterController controller;
    public Transform cam;

    public float speed = 6.0f;

    public float turnSmoothTime = 0.1f;

    float turnSmoothVelocity;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    bool isGrounded;

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = -5.0f;
        }

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 rotation = new Vector3(horizontal, 0.0f, 0.0f).normalized;
        Vector3 direction = new Vector3(0.0f, 0.0f, vertical).normalized;

        if (rotation.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(rotation.x, rotation.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, 0.5f);
            transform.rotation = Quaternion.Euler(0.0f, angle, 0.0f);
        }

        if (direction.magnitude >= 0.1f)
        {
            if (Input.GetMouseButton(1))
            {
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0.0f, angle, 0.0f);
                Vector3 moveDirection = Quaternion.Euler(0.0f, targetAngle, 0.0f) * Vector3.forward;
                controller.Move(moveDirection.normalized * speed * Time.deltaTime);
            }
            else
            {
                Vector3 moveDirection = transform.rotation * direction;
                controller.Move(moveDirection * speed * Time.deltaTime);
            }
        }

        if (CanJump())
            Jump();

        GravityEffect();
    }

    bool CanJump()
    {
        return Input.GetButtonDown("Jump") && isGrounded;
    }

    void Jump()
    {
        playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
    }

    void GravityEffect()
    {
        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }
}
