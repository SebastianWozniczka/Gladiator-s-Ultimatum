using DG.Tweening;
using Photon.Pun;
using Photon.Realtime;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviourPun
{
    public Transform rightHandObj = null;
    public Transform lookObj = null;
    public PhotonView PV;
    public Button right;
    public Transform sideChild;
    public CharacterController controller;
    public GameObject NetworkController;

    private float playerSpeed = 5.0f;
    private float jumpHeight = 1.5f;
    private float gravityValue = -9.81f;


    private Rigidbody characterRigidBody;
    private Animator animator;

    private bool isFacing = true;
    private float Move;
    private float speed = 5;

    [Header("Input Actions")]
    public InputActionReference moveAction;
    public InputActionReference jumpAction;
    private Vector3 playerVelocity;
    private bool groundedPlayer;


    private void OnEnable()
    {
        moveAction.action.Enable();
        jumpAction.action.Enable();
    }

    private void OnDisable()
    {
        moveAction.action.Disable();
        jumpAction.action.Disable();
    }

    private void Awake()
    {
       animator = GetComponent<Animator>();
       characterRigidBody = GetComponent<Rigidbody>();
       PV = GetComponent<PhotonView>();
    } 

    public void Right()
    {
        animator.SetTrigger("Trigger");
    }
    void Update()
    {
        Movement();
    }

    private void Movement()
    {


        groundedPlayer = controller.isGrounded;

        if (groundedPlayer)
        {
            // Slight downward velocity to keep grounded stable
            if (playerVelocity.y < -2f)
                playerVelocity.y = -2f;
        }

        


        Vector2 input = moveAction.action.ReadValue<Vector2>();
        Vector3 move = new Vector3(input.x, 0, input.y);
        move = Vector3.ClampMagnitude(move, 1f);

        // Apply gravity
        playerVelocity.y += gravityValue * Time.deltaTime;

        // Move
        Vector3 finalMove = move * playerSpeed + Vector3.up * playerVelocity.y;
        controller.Move(finalMove * Time.deltaTime);


        if (move != Vector3.zero)
            transform.forward = move;

       // if (Input.GetKeyDown(KeyCode.RightArrow))
       // {
           // transform.position = Vector3.MoveTowards(transform.position, sideChild.transform.position, 0.5f * Time.deltaTime);
      //  }

        if (groundedPlayer && jumpAction.action.WasPressedThisFrame())
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -2f * gravityValue);
        }


        if (Input.GetKeyDown(KeyCode.F)) 
        {

            animator.SetTrigger("Trigger");
        }
        Move = Input.GetAxis("Horizontal");
       // characterRigidBody.angularVelocity = new Vector2(speed * Move, characterRigidBody.linearVelocity.y);

        if (move != Vector3.zero)
        {
            animator.SetTrigger("Move");
        }
        else
        {
            //animator.SetTrigger("Idle");
        }

        animator.SetFloat("Velocity", Move);


        if (Input.GetKeyDown(KeyCode.Escape))
        {
            NetworkController.SetActive(true);

        }


    }
    void RotateCubesAroundAxes(int v)
    {
        if(isFacing)
        transform.DORotate(new Vector3(0f, v, 0f), 1f);
       // cubes[1].transform.DORotate(new Vector3(0f, 45, 0f), 1f, RotateMode.LocalAxisAdd).SetLoops(-1);
       // cubes[2].transform.DORotate(new Vector3(0f, 0f, 45), 1f, RotateMode.LocalAxisAdd).SetLoops(-1);

    }
}
