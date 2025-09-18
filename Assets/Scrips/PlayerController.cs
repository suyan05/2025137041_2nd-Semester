using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("이동 설정")]
    public float walkSpeed = 3f;
    public float runSpeed = 6f;
    public float rotateSpeed = 10f;

    [Header("점프설정")]
    public float jumpHeight = 2.0f;
    public float gravity = -9.8f;
    public float landingDuration = 0.3f;

    [Header("공격 설정")]
    public float attackDuration = 0.8f;
    public bool canMoveWhileAttacking = false;

    [Header("컴포넌트")]
    public Animator animator;

    private CharacterController CC;
    private Camera PlayerCamera;

    //현재 상태 값
    private float currentSpeed;
    private bool isAttacking = false;
    private bool isLanding = false;
    private float landingTimer;

    private Vector3 velocity;
    private bool isGrounded;
    private bool wasGrounded;
    private float attackTimer;
    
    private void Start()
    {
        CC = GetComponent<CharacterController>();
        PlayerCamera = Camera.main;
    }

    private void Update()
    {
        CheckGrounded();
        Move();
        Attack();
        UpdateAnimator();
        HandleJump();
    }

    private void Move()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        if (h != 0 || v != 0)
        {
            Vector3 cameraFoward = PlayerCamera.transform.forward;
            Vector3 cameraRight = PlayerCamera.transform.right;
            cameraFoward.y = 0;
            cameraRight.y = 0;
            cameraFoward.Normalize();
            cameraRight.Normalize();

            Vector3 moveDir = cameraFoward * v + cameraRight * h;

            if(Input.GetKey(KeyCode.LeftShift))
            {
                currentSpeed = runSpeed;
            }
            else
            {
                currentSpeed = walkSpeed;
            }

            CC.Move(moveDir * currentSpeed * Time.deltaTime);

            Quaternion targetRotation = Quaternion.LookRotation(moveDir);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
        }

        else
        {
            currentSpeed = 0;
            
        }
    }

    private void UpdateAnimator()
    {
        float animatorSpeed = Mathf.Clamp01(currentSpeed / runSpeed);
        animator.SetFloat("Speed", animatorSpeed);
        animator.SetBool("IsGround",isGrounded);

        bool isFalling = !isGrounded && velocity.y < -0.1f;
        animator.SetBool("IsFalling",isFalling);
        animator.SetBool("IsLanding",isLanding);
    }

    private void CheckGrounded()
    {
        wasGrounded = isGrounded;
        isGrounded = CC.isGrounded;
        
        if (!isGrounded & wasGrounded)
        {
            Debug.Log("떨어지기 시작");
        }
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2.0f;

            if (!wasGrounded && animator != null)
            {
                isLanding = true;
                landingTimer = landingDuration;
            }
        }
    }

    private void HandleJump()
    {
        if (!isGrounded)
        {
            velocity.y += gravity * Time.deltaTime;
        }
        CC.Move(velocity * Time.deltaTime);
    }

    private void Attack()
    {

    }

}
