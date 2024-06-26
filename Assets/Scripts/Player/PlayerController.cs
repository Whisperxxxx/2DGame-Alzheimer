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
    [SerializeField]
    //GameObject ChangePrefab; // The animation prefab of changeform

    public AudioSource jumpAudio, runAudio1, runAudio2, changeAudio, cellAudio, spikeAudio;

    private float childSpeed = 4f;
    private float oldSpeed = 1f;
    float collisionRadius = 0.5f; // Radius for the ground check collision
    private bool isGround;
    public int jumpCount = 0;
    private Rigidbody2D rb;
    private bool isHurt;
    public bool isChange;
    public bool isOld = true; // Used to change the player form
    public bool isDead = false;
    private bool shouldChange = false; // Check if need to change form after falling
    public bool interactCell = false;
    public bool haveKey = false;
    
    // Returns the appropriate speed based on the current form
    private float CurrentSpeed
    {
        get { return isOld ? oldSpeed : childSpeed; }
    }

    // Returns the appropriate Animator based on the current form
    private Animator CurrentAnimator
    {
        get { return isOld ? oldSprite.GetComponent<Animator>() : childSprite.GetComponent<Animator>(); }
    }

    private Vector2 CurrentBottomOffset
    {
        get { return isOld ? oldSprite.GetComponent<OldController>().bottomOffset : childSprite.GetComponent<ChildController>().bottomOffset; }
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
        if (!isChange && !isDead)
        {
            Jump();
        }
        if (interactCell && !isChange)
        {
            InteractCell();
        }
    }

    private void FixedUpdate()
    {
        SwitchAnim();
        isGround = Physics2D.OverlapCircle((Vector2)transform.position + CurrentBottomOffset, collisionRadius, ground);    // Check if the player is on the ground
        if (!isDead && !isChange)
        {
            GroundMovement();
        }
        else if (isDead || isChange)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }

    }

    void GroundMovement()
    {
        // Movement
        float horizontalMove = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(horizontalMove * CurrentSpeed, rb.velocity.y);

        if (horizontalMove != 0 && isGround)
        {
            // Determine which audio to play based on if the player is old or not
            AudioSource runAudio = isOld ? runAudio1 : runAudio2;

            // If the run audio is not playing, play it
            if (!runAudio.isPlaying)
            {
                runAudio.Play();
            }
        }
        else
        {
            // If horizontal movement stops, stop the run audio if it's playing
            if (runAudio1.isPlaying)
            {
                runAudio1.Stop();
            }
            if (runAudio2.isPlaying)
            {
                runAudio2.Stop();
            }
        }

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
            jumpAudio.Play();
        }
    }

    void SwitchAnim()
    {
        // Update animation parameters
        Animator animator = CurrentAnimator; 
        animator.SetFloat("running", Mathf.Abs(rb.velocity.x));
        if (!isDead)
        {
            if (isGround)
            {
                animator.SetBool("falling", false);

                // Transform to the old if there is no jumpCount;
                if (shouldChange)
                {
                    changeAudio.Play();
                    isChange = true;
                    isOld = true;
                    shouldChange = false; // Reset the statement
                                          //ChangeForm(true);
                                          //Instantiate(ChangePrefab, rb.transform.position + new Vector3(0, 1.5f, 0), rb.transform.rotation, rb.transform);
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
            if (isHurt)
            {
                animator.SetBool("hurting", true);
            }
            else
            {
                animator.SetBool("hurting", false);

            }
            if (isChange)
            {
                animator.SetTrigger("change");
            }

        }
        else
        {
            animator.SetTrigger("death");
        }
    }

    void InteractCell()
    {
        if (isOld)
        {
            GameUIManager.Instance.UpdateTime();
            changeAudio.Play();
            isChange = true;
            interactCell = false;
            //ChangeForm(false);
            //Instantiate(ChangePrefab, rb.transform.position + new Vector3(0, 1.5f, 0), rb.transform.rotation, rb.transform);
        }
        else
        {
            GameUIManager.Instance.UpdateTime();
            cellAudio.Play();
            jumpCount += 1;
            shouldChange = false;
            isHurt = true;
            interactCell = false;
        }
  
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Hazard"))
        {
            spikeAudio.Play();
            isDead = true;
        }
    }

    // Change the player's form
    public void ChangeForm(bool becomeOld)
    {
        if (becomeOld)
        {
            oldSprite.SetActive(true);
            childSprite.SetActive(false);
            jumpCount = 0;
            isChange = false;
        }
        else
        {
            oldSprite.SetActive(false);
            childSprite.SetActive(true);
            isOld = false;
            jumpCount += 1;
            isChange = false;
        }
    }

    public void OverHurt()
    {
        isHurt = false;
    }

    void OnDrawGizmos()
    {
        // Draw a gizmo at the position of the ground check
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere((Vector2)transform.position + CurrentBottomOffset, collisionRadius);
    }

}
