using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public Animator animator;
    public CameraController cameraController;
    private Vector3 cameraOffset;
    private float cameraXOffset = 1.5f;
    private float cameraYOffset = 1.5f;
    private float cameraZOffset = -10f;

    float horizontalMovement = 0f;
    public float runSpeed = 40f;
    bool jump = false;

    // Start is called before the first frame update
    void Start()
    {
        cameraOffset = new Vector3(cameraXOffset, 0f, cameraZOffset);
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
            if (horizontalMovement < 0)
            {
                // If left
                cameraOffset.x = -cameraXOffset;
            }
            else if (horizontalMovement > 0)
            {
                // If right
                cameraOffset.x = cameraXOffset;
            }

            // If player has control of the camera, set the offset
            if (cameraController.target == gameObject.transform)
                cameraController.SetOffset(cameraOffset);
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

    public Vector3 CameraOffset
    {
        get
        {
            return cameraOffset;
        }
    }
}
