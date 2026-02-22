using DG.Tweening;
using Photon.Pun;
using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviourPun
{
    public Transform rightHandObj = null;
    public Transform lookObj = null;
    public PhotonView PV;
    public Button right;

    private Rigidbody characterRigidBody;
    private Animator animator;

    private bool isFacing = true;
    private float Move;
    private float speed = 5;

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

        if (Input.GetKeyDown(KeyCode.F)) 
        {
            animator.SetTrigger("Trigger");
        }
        Move = Input.GetAxis("Horizontal");
        characterRigidBody.angularVelocity = new Vector2(speed * Move, characterRigidBody.linearVelocity.y);

        if (Move != 0)
        {
            animator.SetTrigger("Move");
        }
        else
        {
            animator.SetTrigger("Idle");
        }

        animator.SetFloat("Velocity", Move);

    }
    void RotateCubesAroundAxes(int v)
    {
        if(isFacing)
        transform.DORotate(new Vector3(0f, v, 0f), 1f);
       // cubes[1].transform.DORotate(new Vector3(0f, 45, 0f), 1f, RotateMode.LocalAxisAdd).SetLoops(-1);
       // cubes[2].transform.DORotate(new Vector3(0f, 0f, 45), 1f, RotateMode.LocalAxisAdd).SetLoops(-1);

    }
}
