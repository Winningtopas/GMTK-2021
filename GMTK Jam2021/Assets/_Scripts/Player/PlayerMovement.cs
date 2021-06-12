using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    [SerializeField]
    private float walkSpeed = 20f;
    [SerializeField]
    private float ballSpeed = 100f;
    [SerializeField]
    private float popVelocity = 100f;

    [SerializeField]
    float maxBallSpeed = 50f;
    [SerializeField]
    private float turnSmoothTime = 0.1f;
    private float turnSmoothvelocity;

    [SerializeField]
    private Transform cam;
    private Rigidbody rb;

    public bool ballMode = false;

    [SerializeField]
    private Collider[] legColliders;
    
    // ground distance stuff
    RaycastHit hit;
    private int layerMask = 1 << 8;
    [SerializeField]
    private float groundDistance = 15f;
    public bool isGround;

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
        for(int i = 0; i < legColliders.Length;  i++)
        {
            legColliders[i].enabled = false;
        }
    }

    public void AcitivateWalkMode()
    {
        ballMode = false;

        for (int i = 0; i < legColliders.Length; i++)
        {
            legColliders[i].enabled = true;
        }

        rb.velocity = Vector3.zero;
        rb.AddExplosionForce(popVelocity, transform.position, 10f, 3.0f, ForceMode.Impulse);
        //rb.AddForce(Vector3.up * popVelocity, ForceMode.Impulse);

        //rb.AddForce(Vector3.up * popVelocity * Time.deltaTime);
    }

    private void WalkMode()
    {
        // turn on leg colliders

        rb.constraints = RigidbodyConstraints.FreezeRotation;

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal, transform.position.y, vertical).normalized;

        if (horizontal != 0 || vertical != 0)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothvelocity, turnSmoothTime);

            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            transform.Translate(Vector3.forward * Time.deltaTime * walkSpeed);
        }
    }

    private void BallMode()
    {
        // TO DO: tween to 0 rotation
        // turn off leg colliders

        rb.constraints = RigidbodyConstraints.None;
        rb.isKinematic = false;

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            //if (Input.GetKeyDown(KeyCode.S))
            //{
            //    rb.AddForce(moveDirection.normalized * ballSpeed * 100 * Time.deltaTime);
            //}

            if (isGrounded() && rb.velocity.magnitude < 50f)
            {
                rb.AddForce(moveDirection.normalized * ballSpeed * 2 * Time.deltaTime);
            }

            if (!isGrounded())
            {
                rb.AddForce(moveDirection.normalized * ballSpeed * Time.deltaTime);
            }
        }
    }
}
