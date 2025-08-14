using UnityEngine;
using UnityEngine.InputSystem;
// need for all things relating to new input system

public class FPController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float gravity = -9.81f;
    public float jumpHeight = 1.5f;

    [Header("Look Settings")]
    public Transform cameraTransform;
    public float lookSensitivity = 2f;
    public float verticalLookLimit = 90f;
    private CharacterController controller;
    private Vector2 moveInput;
    private Vector2 lookInput;
    private Vector3 velocity;
    private float verticalRotation = 0f;

    [Header("Shooting")]
    public GameObject bulletPrefab;
    public Transform gunPoint;

    [Header ("Crouch Settings")]
    public float crouchHeight = 1f;
    public float standHeight = 2f;
    public float crouchSpeed = 2.5f;
    private float originalMoveSpeed;

    [Header("Pickup Settings")]
    public float pickupRange =3f;
    public Transform holdPoint;
    private PickUpObject heldObject;

    [Header ("Throw Settings")]
    public float throwForce = 10f;
    public float throwUpwardBoost = 1f;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        originalMoveSpeed = moveSpeed;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        HandleMovement();
        HandleLook();

        if (heldObject != null)
        {
            heldObject.MoveToHoldPoint(holdPoint.position);
        }
    }
    

    public void OnMovement(InputAction.CallbackContext context) 
    {
        moveInput = context.ReadValue<Vector2>();
    }
    public void OnLook(InputAction.CallbackContext context)
    {
        lookInput = context.ReadValue<Vector2>();
    }

    public void onJump(InputAction.CallbackContext context)
    {
        if (context.performed && controller.isGrounded)
        {
            velocity.y  = Mathf.Sqrt (jumpHeight * -2f * gravity);
        }
    }

    public void OnShoot(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Shoot();
        }
    }

    public void onCrouch(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            controller.height = crouchHeight;
            moveSpeed = crouchSpeed;
        }
        else if (context.canceled)
        {
            controller.height = standHeight;
            moveSpeed = originalMoveSpeed;
        }
    }


    private void Shoot()
    {
        if (bulletPrefab != null && gunPoint != null)
        {
            GameObject bullet = Instantiate(bulletPrefab, gunPoint.position, gunPoint.rotation);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.AddForce(gunPoint.forward * 1000f);
                Destroy(bullet, 3);
            }
        }
    }

    public void HandleMovement()
    {
        Vector3 move = transform.right * moveInput.x + transform.forward *
        moveInput.y;
        controller.Move(move * moveSpeed * Time.deltaTime);

        if (controller.isGrounded && velocity.y < 0)
        velocity.y = -2f;
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    public void HandleLook()
    {
        float mouseX = lookInput.x * lookSensitivity;
        float mouseY = lookInput.y * lookSensitivity;
        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -verticalLookLimit,
         verticalLookLimit);
        cameraTransform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }

    public void OnPickUp(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        if (heldObject == null)
        {
            Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);
            if (Physics.Raycast (ray, out RaycastHit hit, pickupRange))
            {
                PickUpObject pickUp = hit.collider.GetComponent<PickUpObject>();
                if (pickUp != null)
                {
                    pickUp.PickUp(holdPoint);
                    heldObject = pickUp;
                }
            }

        }

        else
        {
            heldObject.Drop();
            heldObject = null;
        }
    }

    public void OnThrow(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        if (heldObject == null) return;

        Vector3 dir = cameraTransform.forward;
        Vector3 impulse = dir * throwForce + Vector3.up * throwUpwardBoost;

        heldObject.Throw(impulse);
        heldObject = null;

    }


}

