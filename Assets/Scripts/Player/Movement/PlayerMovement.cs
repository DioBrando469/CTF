using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] Transform orientation;
    public float speed = 30;
    Vector3 movedirection;
    float vertic;
    float horiz;

    [SerializeField] float groundDrag = 6f;
    [SerializeField] float airDrag = 1f;
    [SerializeField] float gravityMultiplier = 1f;
    [SerializeField] float jumpForce = 5f;
    [SerializeField] float movementMultiplier = 3f;
    [SerializeField] float airMultiplier = 0.4f;
    [SerializeField] float MaxAirSpeed = 10f;
    [SerializeField] float AirStrafeForce = 1f;

    Rigidbody rb;
    [SerializeField] Transform player;
    [Header("Ground Check")]
    [SerializeField] float groundRadius = 0.4f;
    [SerializeField] LayerMask groundMask;
    [SerializeField] float playerHeight = 2f;
    bool isGrounded = true;
    Vector3 slopeMoveDirection;
    RaycastHit slopeHit;

    [Header("Keybinds")]
    [SerializeField] KeyCode jumpKey = KeyCode.Space;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        MyInput();
        ControlDrag();
        isGrounded = Physics.CheckSphere(transform.position - new Vector3(0, 1, 0), groundRadius, groundMask);
        if(Input.GetKeyDown(jumpKey) && isGrounded)
        {
            Jump();
        }
        slopeMoveDirection = Vector3.ProjectOnPlane(movedirection, slopeHit.normal);
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if(isGrounded && !OnSlope())
        {
            rb.AddForce(movedirection.normalized * speed * movementMultiplier, ForceMode.Acceleration);
        }
        else if(isGrounded && OnSlope())
        {
            rb.AddForce(slopeMoveDirection.normalized * speed * movementMultiplier, ForceMode.Acceleration);
        }    
        else
        {
            //rb.AddForce(movedirection.normalized * speed * movementMultiplier * airMultiplier, ForceMode.Acceleration);
            rb.AddForce(transform.up * gravityMultiplier * -1f, ForceMode.Acceleration);
            AirMovement(movedirection.normalized);
        }
    }
    void MyInput()
    {
        float vertic = Input.GetAxis("Vertical");
        float horiz = Input.GetAxis("Horizontal");
        movedirection = orientation.forward * vertic + orientation.right * horiz;
    }
    void ControlDrag()
    {
        if(isGrounded)
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = airDrag;
        }
    }
    void Jump()
    {
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }
    bool OnSlope()
    {
        if(Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight / 2 + 0.1f))
        {
            if(slopeHit.normal != Vector3.up)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }
    void AirMovement(Vector3 vector3)
{
    // project the velocity onto the movevector
    Vector3 projVel = Vector3.Project(GetComponent<Rigidbody>().velocity, vector3);

    // check if the movevector is moving towards or away from the projected velocity
    bool isAway = Vector3.Dot(vector3, projVel) <= 0f;

    // only apply force if moving away from velocity or velocity is below MaxAirSpeed
    if (projVel.magnitude < MaxAirSpeed || isAway)
    {
        // calculate the ideal movement force
        Vector3 vc = vector3.normalized * AirStrafeForce;

        // cap it if it would accelerate beyond MaxAirSpeed directly.
        if (!isAway)
        {
            vc = Vector3.ClampMagnitude(vc, MaxAirSpeed - projVel.magnitude);
        }
        else
        {
            vc = Vector3.ClampMagnitude(vc, MaxAirSpeed + projVel.magnitude);
        }

        // Apply the force
        GetComponent<Rigidbody>().AddForce(vc, ForceMode.VelocityChange);
    }
}
}
