using System;
using UnityEngine;

public class PlayerActor : MonoBehaviour
{
    public Animator animator;
    public Rigidbody2D rigidBody;
    public SpriteRenderer spriteRenderer;

    public float speed = 40f;
    public float jump = 30f;
    public float moveDuringJumpScale = 0.5f;
    public float groundedDistance = 0.1f;
    
    private bool _isGrounded = true;
    
    private static readonly int Moving = Animator.StringToHash("IsRunning");
    private static readonly int Midair = Animator.StringToHash("IsJumping");
        
    protected void FixedUpdate()
    {
        // Horizontal movement
        var horizontalControl = Input.GetAxisRaw("Horizontal");
        var transformRight = transform.right;
        var horizontalMovement = transformRight * (horizontalControl * speed);
        if (!_isGrounded)
            horizontalMovement *= moveDuringJumpScale;
        rigidBody.AddForce(horizontalMovement);
        var isMoving = rigidBody.velocity.magnitude != 0;
        animator.SetBool(Moving, isMoving);

        // Make it face the correct direction
        if (horizontalMovement.magnitude != 0)
        {
            var facingRight = Vector3.Angle(horizontalMovement, transformRight) < 90;
            var newScale = transform.localScale;
            newScale.x = Math.Abs(newScale.x) * (facingRight ? 1 : -1);
            transform.localScale = newScale;
        }
    }

    private void Update()
    {
        // Vertical movement
        var bounds = spriteRenderer.bounds;
        var rayPosition = bounds.center;
        rayPosition.y -= bounds.extents.y + groundedDistance*2;
        
        _isGrounded = Physics2D.Raycast(rayPosition, -transform.up, 
            groundedDistance);
        
        if (Input.GetButtonDown("Jump") && _isGrounded)
            rigidBody.AddForce(transform.up * jump, ForceMode2D.Impulse);
        
        animator.SetBool(Midair, !_isGrounded);
    }
}