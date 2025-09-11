using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("이동 설정")]
    public float walkSpeed = 3f;
    public float runSpeed = 6f;
    public float rotateSpeed = 10f;

    [Header("공격 설정")]
    public float attackDuration = 0.8f;
    public bool canMoveWhileAttacking = false;

    [Header("컴포넌트")]
    public Animator animator;

    private CharacterController CC;
    private Camera PlayerCamera;

    private float currentSpeed;
    private bool isAttacking = false;

    private void Start()
    {
        CC = GetComponent<CharacterController>();
        PlayerCamera = Camera.main;
    }

    private void Update()
    {
        Move();
        UpdateAnimator();
        Attack();
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
    }

    private void Attack()
    {

    }

}
