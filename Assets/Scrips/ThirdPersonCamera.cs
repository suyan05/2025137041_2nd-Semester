using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThirdPersonCamera : MonoBehaviour
{
    [Header("타겟 설정")]
    public Transform target;

    [Header("카메라 거리 설정")]
    public float distance = 8f;
    public float height = 5f;

    [Header("마우스 설정")]
    public float mouseSensitivity = 5f;
    public float minVerticalAngle = -30;
    public float maxVerticalAngle = 60f;

    [Header("부드러움 설정")]
    public float positionSmoothTime = 0.2f;
    public float rotationSmoothTime = 0.1f;

    private float horizontalAngle = 0f;
    private float verticalAngle = 0f;

    private Vector3 currentVelocity;
    private Vector3 currentposition;
    private Quaternion currentRotation;

    private void Start()
    {
        if(target == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if(player != null)
            {
                target = player.transform;
            }
        }

        currentposition = transform.position;
        currentRotation = transform.rotation;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleCursor();
        }
    }

    private void LateUpdate()
    {
        if(target == null) return;
        HandleMouseInput();
        UpdateCameraSmooth();
    }

    private void HandleMouseInput()
    {
        if (Cursor.lockState != CursorLockMode.Locked) return;

        float MouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float MouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        horizontalAngle += MouseX;
        verticalAngle -= MouseY;

        verticalAngle = Mathf.Clamp(verticalAngle, minVerticalAngle, maxVerticalAngle);
    }

    private void UpdateCameraSmooth()
    {
        Quaternion rotation = Quaternion.Euler(verticalAngle, horizontalAngle, 0);
        Vector3 rotateOffset = rotation * new Vector3(0, 0, -distance);
        Vector3 targetPostion = target.position + rotateOffset;

        Vector3 lookatarget = target.position + Vector3.up * height;
        Quaternion targetRotation = Quaternion.LookRotation(lookatarget - targetPostion);

        currentposition = Vector3.SmoothDamp(currentposition, targetPostion, ref currentVelocity, positionSmoothTime);

        currentRotation = Quaternion.Slerp(currentRotation, targetRotation, Time.deltaTime / rotationSmoothTime);

        transform.position = currentposition;
        transform.rotation = currentRotation;
    }

    private void ToggleCursor()
    {
        if(Cursor.lockState==CursorLockMode.Locked)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
