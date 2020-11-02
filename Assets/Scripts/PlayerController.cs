using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private float jumpHeight = 1.0f;
    private float gravityValue = -9.81f;

    [SerializeField]
    private bool debug = true;

    [SerializeField]
    private Animator animatorController = default;

    [SerializeField]
    private float rotateSpeed = 60f;

    [SerializeField]
    private float walkingSpeed = 1f;
    
    [SerializeField]
    private float runningSpeed = 2f;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
       // groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        float run = Input.GetAxis("Fire1");

        bool isRotating = (horizontal != 0f);
        bool isMoving = (vertical != 0f);
        bool isRunning = (run != 0f);

        animatorController?.SetBool("IsWalking", isRotating || isMoving);
        animatorController?.SetBool("IsRunning", isRunning);

        if (isRotating)
        {
            transform.Rotate(Vector3.up, Mathf.Sign(horizontal) * rotateSpeed * Time.deltaTime);
        }

        if(isMoving)
        {
            //Vector3 move = transform.forward * vertical;
            //float movementSpeed = (isRunning) ? runningSpeed : walkingSpeed;

            //transform.Translate(Mathf.Sign(vertical) * transform.forward * Time.deltaTime * movementSpeed);
            float movementSpeed = (isRunning) ? runningSpeed : walkingSpeed;
            GetComponent<Rigidbody>().MovePosition(transform.position + Mathf.Sign(vertical) * transform.forward * Time.deltaTime * movementSpeed);
        }

        ShowDebug();

        //if (move != Vector3.zero)
        //{
        //    gameObject.transform.forward = move;
        //}

        //// Changes the height position of the player..
        //if (Input.GetButtonDown("Jump") && groundedPlayer)
        //{
        //    playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        //}

        //playerVelocity.y += gravityValue * Time.deltaTime;
        //controller.Move(playerVelocity * Time.deltaTime);
    }

    private void ShowDebug()
    {
        if(debug)
        {
            Vector3 playerMiddle = transform.position + transform.up;

            Debug.DrawRay(playerMiddle, transform.forward * 2f, Color.green);
        }
    }
}