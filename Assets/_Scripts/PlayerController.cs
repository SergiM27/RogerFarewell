using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    public bool isGhost;
    [Header("References")]
    public Transform camTransform;
    public Rigidbody rb;
    public Transform playerRender;
    public GameObject normalPlayer;
    public GameObject ghostPlayer;
    public Animator normalAnim;
    public Animator ghostAnim;

    [Header("Input")]
    private float inputVertical;
    private float inputHorizontal;
    private float jump;
    [Space]
    
    [Header("Movement")]
    
    public float speed;
    public float angularSpeed;
    public float verticalSpeed;
    public float maxJumpSpeed;
    public float maxFallSpeed;
    private Vector2 forward;
    [Space]
    Quaternion targetAngle;
    Vector3 rotationVelocity;
    [Range(0, 0.25f)]public float rotationSmooth;

    [Header("Collision")]
    public GameObject collision;

    
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        
    }
    void Start()
    {
        isGhost = GameManager.isGhost;
        if (isGhost)
        {
            normalPlayer.SetActive(false);
            ghostPlayer.SetActive(true);
        }
        else
        {
            normalPlayer.SetActive(true);
            ghostPlayer.SetActive(false);
        }
    }

    void Update()
    {
        GetInput();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        //lateralMovement
        Vector3 cameraForward = new Vector3(camTransform.forward.x, 0, camTransform.forward.z).normalized;
        Vector3 movement = cameraForward * forward.y + camTransform.right * forward.x;

        //jump

        rb.velocity = movement * speed + Vector3.up * (Mathf.Clamp(rb.velocity.y, -maxFallSpeed, maxJumpSpeed) + jump * Time.fixedDeltaTime * verticalSpeed);
        normalAnim.SetFloat("speed", rb.velocity.magnitude);
        ghostAnim.SetFloat("speed", rb.velocity.magnitude);
        //rotation
        if (movement != Vector3.zero) targetAngle = Quaternion.LookRotation(movement);
        playerRender.rotation = SmoothDampQuaternion(playerRender.rotation, targetAngle, ref rotationVelocity, rotationSmooth);


    }

    

    private void GetInput()
    {
        inputVertical = Input.GetAxis("Vertical");
        inputHorizontal = Input.GetAxis("Horizontal");
        forward = new Vector2(inputHorizontal, inputVertical);
        if (isGhost)
        {
            jump = Input.GetAxis("Jump");
        }
        
    }

    public Quaternion SmoothDampQuaternion(Quaternion current, Quaternion target, ref Vector3 currentVelocity, float smoothTime)
    {
        Vector3 c = current.eulerAngles;
        Vector3 t = target.eulerAngles;
        return Quaternion.Euler(
          Mathf.SmoothDampAngle(c.x, t.x, ref currentVelocity.x, smoothTime),
          Mathf.SmoothDampAngle(c.y, t.y, ref currentVelocity.y, smoothTime),
          Mathf.SmoothDampAngle(c.z, t.z, ref currentVelocity.z, smoothTime)
        );
    }

    private void OnCollisionStay(Collision collision)
    {
        this.collision = collision.gameObject;
    }

    private void OnDisable()
    {
        inputHorizontal = 0;
        inputVertical = 0;
         
    }
}
