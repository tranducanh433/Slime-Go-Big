using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.HableCurve;

public class PlayerMovement : MonoBehaviour
{
    [Header("Moving")]
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float accelerationTime = 0.1f;
    [SerializeField] float decelerationTime = 0.1f;
    [SerializeField] ParticleSystem movingEffect;
    bool playingMovingEffect = false;

    [Header("Jump")]
    public float jumpForce = 10f;
    [SerializeField] float fallMultiplier = 2.5f;
    [SerializeField] float lowJumpMultiplier = 2f;
    [SerializeField] float minVelocityY = -25;
    [SerializeField] Vector2 groundCheckSize = new Vector2(0.9f, 0.125f);
    public int additionalJump = 0;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] ParticleSystem JumpEffect;

    float currentMinVelocityY = 0;

    [Header("Slimebrella")]
    [SerializeField] Animator slimebrellaAnim;
    [SerializeField] float slimebrellaMinVelocityY = -2f;

    public bool isSlimebrellaActive = false;
    public float slimebrellaDuration = 5f;

    float currentSlimebrellaDuration = 0;

    private Rigidbody2D rb;

    private float moveInput;
    private float smoothMoveVelocity;

    int currentAdditionalJump = 0;

    public int posibleJumpHeight { get
        {
            int force = (int)((jumpForce - 12) / 2);
            return 4 + force + additionalJump;
        } 
    }


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentMinVelocityY = minVelocityY;
    }

    void Update()
    {
        Move();
        Jump();
        SlimebrellaFloating();
        ApplyCustomGravity();
        

    }

    void Move()
    {
        moveInput = Input.GetAxisRaw("Horizontal");

        float targetVelocityX = moveInput * moveSpeed;
        rb.velocity = new Vector2(
            Mathf.SmoothDamp(rb.velocity.x, targetVelocityX, ref smoothMoveVelocity, moveInput != 0 ? accelerationTime : decelerationTime),
            rb.velocity.y
        );

        if (moveInput != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(moveInput), 1, 1);
        }

        // Moving Effect
       
        if(moveInput != 0 && IsGrounded() && !playingMovingEffect)
        {
            movingEffect.Play();
            playingMovingEffect = true;
        }
        else if((moveInput == 0 || !IsGrounded()) && playingMovingEffect)
        {
            movingEffect.Stop();
            playingMovingEffect = false;
        }
    }

    void Jump()
    {
        if (IsGrounded() && Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            JumpEffect.Play();
        }
        else if (!IsGrounded() && Input.GetKeyDown(KeyCode.Space) && currentAdditionalJump > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            currentAdditionalJump--;
            JumpEffect.Play();
        }
        else if (IsGrounded())
        {
            currentAdditionalJump = additionalJump;
        }
    }

    void ApplyCustomGravity()
    {
        if (rb.velocity.y < 0)
        {
            Vector2 velcocity = rb.velocity + Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
            velcocity = new Vector2(velcocity.x, Mathf.Clamp(velcocity.y, currentMinVelocityY, float.MaxValue));
            rb.velocity = velcocity;
            
        }
        else if (rb.velocity.y > 0 && !Input.GetKey(KeyCode.Space))
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }

    void SlimebrellaFloating()
    {
        if (!isSlimebrellaActive) return;

        if(rb.velocity.y < 0 
            && Input.GetKey(KeyCode.Space)
            && currentSlimebrellaDuration > 0)
        {
            if(slimebrellaDuration > 0)
                currentSlimebrellaDuration -= Time.deltaTime;

            currentMinVelocityY = slimebrellaMinVelocityY;


            slimebrellaAnim.SetBool("Appear", true);

            bool have2sLessDuration = currentSlimebrellaDuration <= 2;
            slimebrellaAnim.SetBool("Almost Break", have2sLessDuration);
        }
        else
        {
            currentMinVelocityY = minVelocityY;
            slimebrellaAnim.SetBool("Appear", false);
        }

        if (IsGrounded())
        {
            currentSlimebrellaDuration = slimebrellaDuration;
        }
    }


    public bool IsGrounded()
    {
        return Physics2D.OverlapBox(groundCheck.position, groundCheckSize, 0, groundLayer);
    }


    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(groundCheck.position, groundCheckSize);


        float angleStep = 360f / 32;

        // Start from the initial point on the circle
        Vector3 startPoint = transform.position + Vector3.right * 2;
        Vector3 previousPoint = startPoint;

        for (int i = 1; i <= 32; i++)
        {
            // Calculate the next point on the circle
            float angle = i * angleStep * Mathf.Deg2Rad;
            Vector3 nextPoint = transform.position + new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * 2;

            // Draw a line between the previous point and the next point
            Gizmos.DrawLine(previousPoint, nextPoint);

            // Update the previous point for the next iteration
            previousPoint = nextPoint;
        }
    }
}
