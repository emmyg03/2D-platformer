using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody2D theRB;
    public float jumpForce;
    public float runSpeed;
    private float activeSpeed;

    private bool isGrounded;
    public Transform groundCheckPoint;
    public float groundCheckRadius;
    public LayerMask whatIsGround;
    private bool canDoubleJump;

    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Checks to see if the player is in contact with the ground
        isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, whatIsGround);

        // Initialize default speed
        activeSpeed = moveSpeed;
        // If player is pressing left shift to make sprite run, initialize activeSpeed to the value runSpeed
        if (Input.GetKey(KeyCode.LeftShift))
        {
            activeSpeed = runSpeed;
        }

        theRB.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * activeSpeed, theRB.velocity.y);

        // We only want to know when the player presses the jump button down (space)
        // GetButtonDown --> the moment the key is pressed
        // GetButtonUp --> the moment the key is released
        // GetButton --> in the current frame, is the button being held down
        if (Input.GetButtonDown("Jump"))
        {
            // If the player is on the ground, then allow single jump
            if (isGrounded)
            {
                Jump();
                canDoubleJump = true;
                anim.SetBool("isDoubleJumping", false);
            }
            else // Now, if the player is in the air, allow double jump
            {        
                if (canDoubleJump)
                {
                    Jump();
                    canDoubleJump = false;  // No additional jumps allowed
                    anim.SetBool("isDoubleJumping", true);
                }
            }
        }

        // If the player is moving right, then transform (1,1,1) (aka animations face right) 
        if (theRB.velocity.x > 0)
        {
            transform.localScale = Vector3.one;
        }

        // If the player is moving right, then transform (-1,1,1) (aka flip sprite and animations face left)
        if (theRB.velocity.x < 0)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }

        // Handle animation
        anim.SetFloat("speed", Mathf.Abs(theRB.velocity.x));    // Take the abs value, so that the animation is applied when player moves BOTH left and right
        anim.SetBool("isGrounded", isGrounded);
        anim.SetFloat("ySpeed", theRB.velocity.y);
    }

    void Jump()
    {
        theRB.velocity = new Vector2(theRB.velocity.x, jumpForce);
    }
}
