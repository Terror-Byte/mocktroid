using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public Animator animator;
    public CameraController cameraController;

    float horizontalMovement = 0f;
    public float runSpeed = 40f;
    bool jump = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal") * runSpeed;

        animator.SetFloat("Speed", Mathf.Abs(horizontalMovement));

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
            animator.SetBool("IsJumping", true);
        }

        if (horizontalMovement != 0 && cameraController)
        {
            float cameraXOffset = 1.5f;
            if (horizontalMovement < 0)
            {
                // If left
                cameraController.offset.x = -cameraXOffset;
            }
            else if (horizontalMovement > 0)
            {
                // If right
                cameraController.offset.x = cameraXOffset;
            }
        }
    }

    private void FixedUpdate()
    {
        controller.Move(horizontalMovement * Time.fixedDeltaTime, false, jump);
        jump = false;
    }

    public void OnLanding()
    {
        animator.SetBool("IsJumping", false);
        // jump = false;
    }
}
