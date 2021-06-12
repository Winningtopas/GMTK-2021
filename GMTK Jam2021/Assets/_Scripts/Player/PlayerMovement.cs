using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    [SerializeField]
    private float currentSpeed = 6f;
    [SerializeField]
    private float turnSmoothTime = 0.1f;
    private float turnSmoothvelocity;

    [SerializeField]
    private Transform cam;
    private Rigidbody rb;

    public bool ballMode = false;

    RaycastHit hit;
    private int layerMask = 1 << 8;
    [SerializeField]
    private float groundDistance = 15f;

    public bool isGround;
    public float value;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private bool isGrounded()
    {
        isGround = Physics.Raycast(transform.position, Vector3.down, out hit, groundDistance, layerMask);
        return Physics.Raycast(transform.position, Vector3.down, out hit, groundDistance, layerMask);
    }

    private void Update()
    {
        Ground();
    }

    private void Ground()
    {
        Debug.DrawRay(transform.position, Vector3.down * groundDistance, Color.red);
    }

    private void FixedUpdate()
    {
        if (ballMode)
        {
            BallMode();
        }
        else
        {
            WalkMode();
        }
    }

    public void AcitivateBallMode()
    {
        ballMode = true;
    }

    private void WalkMode()
    {
        rb.isKinematic = true;
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothvelocity, turnSmoothTime);

            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDirection.normalized * currentSpeed * Time.deltaTime);
        }
    }

    private void BallMode()
    {
        rb.isKinematic = false;

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            //    float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothvelocity, turnSmoothTime);

            //    transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            //controller.Move(moveDirection.normalized * currentSpeed * Time.deltaTime);
            //    Debug.Log(moveDirection.normalized * currentSpeed * Time.deltaTime);

            if (isGrounded())
            {
                rb.AddForce(moveDirection.normalized * currentSpeed * value * 2 * Time.deltaTime);
            }
            else
            {
                rb.AddForce(moveDirection.normalized * currentSpeed * value * Time.deltaTime);
            }
        }
    }
}
