using UnityEngine;

public class animationStateController : MonoBehaviour
{
    Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Check if any of the movement keys (WASD) are being held
        bool isMoving = Input.GetKey("w") || Input.GetKey("a") || Input.GetKey("s") || Input.GetKey("d");
        
        // Check if player is holding left shift for run
        bool isRunning = isMoving && Input.GetKey(KeyCode.LeftShift);
        
        // Check if player is holding space for glide
        bool isGliding = Input.GetKey(KeyCode.Space); 
        
        //update animation parameters in animator
        animator.SetBool("isWalking", isMoving);
        animator.SetBool("isRunning", isRunning);
        animator.SetBool("isGliding", isGliding);

        //trigger jump animation if space pressed
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetTrigger("isJumping");
        }
    }
}
    
