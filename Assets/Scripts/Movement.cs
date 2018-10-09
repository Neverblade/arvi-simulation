﻿using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Movement : MonoBehaviour {

    public float movementSpeed = 5.0f;
    public float sensitivity = 2.0f;

    private Camera mainCamera;
    private CharacterController characterController = null;
    private float rotationX = 0.0f;
    private float rotationY = 0.0f;
    private const float clampAngleDegrees = 80.0f;

    void Start() {
        mainCamera = GetComponent<Camera>();
        characterController = GetComponent<CharacterController>();
        Vector3 rotation = mainCamera.transform.localRotation.eulerAngles;
        rotationX = rotation.x;
        rotationY = rotation.y;
    }

    void LateUpdate() {
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0)) {
            SetCursorLock(true);
        }
        else if (Input.GetKeyDown(KeyCode.Escape)) {
            SetCursorLock(false);
        }
#endif

        // Update the rotation.
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = -Input.GetAxis("Mouse Y");
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) {
            // Note that multi-touch control is not supported on mobile devices.
            mouseX = 0.0f;
            mouseY = 0.0f;
        }
        rotationX += sensitivity * mouseY;
        rotationY += sensitivity * mouseX;
        rotationX = Mathf.Clamp(rotationX, -clampAngleDegrees, clampAngleDegrees);
        mainCamera.transform.localRotation = Quaternion.Euler(rotationX, rotationY, 0.0f);

        // Update the position.
        float movementX = Input.GetAxis("Horizontal");
        float movementY = Input.GetAxis("Vertical");
        Vector3 movementDirection = new Vector3(movementX, 0.0f, movementY);
        movementDirection = mainCamera.transform.localRotation * movementDirection;
        movementDirection.y = 0.0f;
        characterController.Move(movementSpeed * movementDirection * Time.deltaTime);
    }

    // Sets the cursor lock for first-person control.
    private void SetCursorLock(bool lockCursor) {
        if (lockCursor) {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
