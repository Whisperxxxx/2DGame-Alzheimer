using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerController : UnitySingleton<PlayerController>
{
    [SerializeField]
    float jumpForce = 14;
    [SerializeField]
    LayerMask ground;
    [SerializeField]
    GameObject oldSprite; 
    [SerializeField]
    GameObject childSprite;

    private float childSpeed = 4f;
    private float oldSpeed = 1f;
    float collisionRadius = 0.5f; // Radius for the ground check collision
    private bool isGround;
    public int jumpCount = 0;
    private Rigidbody2D rb;
    public bool isHurt;
    private bool isOld = true; // Used to change the player form
    private bool isDead = false;
    private bool shouldChange = false; // Check if need to change form after falling


    // Returns the appropriate speed based on the current form
    private float CurrentSpeed
    {
        get { return isOld ? oldSpeed : childSpeed; }
    }

    // Returns the appropriate Animator based on the current form
    private Animator CurrentAnimator
    {
        get
        {
            return isOld ? oldSprite.GetComponent<Animator>() : childSprite.GetComponent<Animator>();
        }
    }

    private Vector2 CurrentBottomOffset
    {
        get
        {
            return isOld ? oldSprite.GetComponent<OldController>().bottomOffset : childSprite.GetComponent<ChildController>().bottomOffset;
        }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Initial the player is old
        oldSprite.SetActive(true);
        childSprite.SetActive(false);
    }


    void Update()
    {
        if (!isHurt && !isDead)
        {
            Jump();
        }
    }

    private void FixedUpdate()
    {
        SwitchAnim();
        isGround = Physics2D.OverlapCircle((Vector2)transform.position + CurrentBottomOffset, collisionRadius, ground);    // Check if the player is on the ground
        if (!isDead && !isHurt)
        {
            GroundMovement();
        }
        else if (isDead || isHurt)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }

    }

    void GroundMovement()
    {
        // Movement
        float horizontalMove = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(horizontalMove * CurrentSpeed, rb.velocity.y);

        // Flip player sprite based on the direction of movement
        if (horizontalMove != 0)
        {
            transform.localScale = new Vector3(horizontalMove, 1, 1);
        }
    }

    void Jump()
    {
        // Check if the condition can jump
        if (Input.GetButtonDown("Jump") && jumpCount > 0 && isGround)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpCount--;

        }
    }

    void SwitchAnim()
    {
        // Update animation parameters
        Animator animator = CurrentAnimator; 
        animator.SetFloat("running", Mathf.Abs(rb.velocity.x));
        if (isGround)
        {
            animator.SetBool("falling", false);

            // Transform to the old if there is no jumpCount;
            if (shouldChange) // 
            {
                animator.SetTrigger("change");
                isHurt = true;
                shouldChange = false; // Reset the statement
            }
        }
        else if (!isGround && rb.velocity.y > 0)
        {
            animator.SetBool("jumping", true);
        }
        else if (rb.velocity.y < 0)
        {
            animator.SetBool("jumping", false);
            animator.SetBool("falling", true);
            // Check if the form should be changed;
            if (jumpCount <= 0 && !isOld) // only child can call ChangeForm(true)
            {
                shouldChange = true;
            }
        }
        if (isDead)
        {
            animator.SetTrigger("death");
            YTEventManager.Instance.TriggerEvent(EventStrings.GAME_OVER);
            // StartCoroutine(RestartGame());
         
        }
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if collide with nerve cells
        if (other.gameObject.CompareTag("Cell"))
        {
            Destroy(other.gameObject);
            if(isOld)
            {
                isHurt = true;
                CurrentAnimator.SetTrigger("change"); // Transform to the child

            }
            else
            {
                jumpCount += 1;
                shouldChange = false;
                CurrentAnimator.SetBool("hurting", true); ;
            }
        }
        else if (other.gameObject.CompareTag("Hazard"))
        {
            isDead = true;
        }
    }

    public void ChangeForm(bool becomeOld)
    {
        // Change the player's form
        if (becomeOld)
        {
            oldSprite.SetActive(true);
            childSprite.SetActive(false);
            isOld = true;
            jumpCount = 0;
            isHurt = false;
        }
        else
        {
            oldSprite.SetActive(false);
            childSprite.SetActive(true);
            isOld = false;
            jumpCount += 1;
            isHurt = false;
        }
    }

    public void OverHurt()
    {
        CurrentAnimator.SetBool("hurting", false); ;
    }

    void OnDrawGizmos()
    {
        // Draw a gizmo at the position of the ground check
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere((Vector2)transform.position + CurrentBottomOffset, collisionRadius);
    }

    /*
    IEnumerator RestartGame()
    {
        // Wait the animation of Death
        yield return new WaitForSeconds(2);

        // Restart the game
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    */
}
