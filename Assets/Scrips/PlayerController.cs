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
    
    private bool isUIMode = false;

    private void Start()
    {
        CC = GetComponent<CharacterController>();
        PlayerCamera = Camera.main;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            ToggeleCursorlock();
        }

        if(!isUIMode)
        {
            CheckGrounded();
            Landing();
            Move();
            UpdateAnimator();
            Attack();
            Jump();
        }
    }

    private void Move()
    {
        if((isAttacking&&!canMoveWhileAttacking)||isLanding)
        {
            currentSpeed = 0;
            return;
        }

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

    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);

            if(animator != null)
            {
                animator.SetTrigger("JumpTrigger");
            }
        }

        if (!isGrounded)
        {
            velocity.y += gravity * Time.deltaTime;
        }
        CC.Move(velocity * Time.deltaTime);
    }

    private void Landing()
    {
        if(isLanding)
        {
            landingTimer -= Time.deltaTime;

            if(landingTimer < 0)
            {
                isLanding = false;
            }
        }
    }

    private void Attack()
    {
        if(isAttacking)
        {
            attackTimer -= Time.deltaTime;

            if(attackTimer <= 0)
            {
                isAttacking = false;
            }
        }

        if (Input.GetMouseButtonDown(0) && !isAttacking)
        {
            isAttacking = true;
            attackTimer = attackDuration;

            if(animator!= null)
            {
                animator.SetTrigger("AttackTrigger");
            }
        }
    }

    public void SetCurrLock(bool lockCursor)
    {
        if(lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            isUIMode = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            isUIMode = true;
        }
    }

    public void ToggeleCursorlock()
    {
        bool ShouldLock = Cursor.lockState != CursorLockMode.Locked;
        SetCurrLock(ShouldLock);
    }

    public void SetUIMode(bool uiMode)
    {        
        SetCurrLock(!uiMode);
    }
}
