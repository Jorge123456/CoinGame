using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playercontrol : MonoBehaviour
{
    // Start is called before the first frame update
    
    public float speed;
    private Rigidbody2D rb;
    public float jumpforce; 
    private float moveInput;
    private float jumpInput;
    private float previousJumpInput;
    public float jumpsAllowed;
    private float timesJumped;

    private bool isJumping;
    // Start is called before the first frame update
 
 void Start()
    {
        rb = GetComponent < Rigidbody2D>();
        timesJumped = 0; 
        previousJumpInput = 0;
    }
void FixedUpdate() {
    
    moveInput = Input.GetAxis("Horizontal");
    rb.velocity= new Vector2(moveInput*speed, rb.velocity.y);

    jumpInput = Input.GetAxis("Vertical");

    if(jumpInput > 0 && timesJumped < jumpsAllowed && previousJumpInput == 0) {
        rb.velocity= new Vector2(rb.velocity.x, jumpforce);
        timesJumped++;
    }
    previousJumpInput = jumpInput;
}
    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Ground"))
        {
            timesJumped = 0;
        }

        else if(other.gameObject.CompareTag("Coin"))
        {
            Destroy(other.gameObject);
            ScoreManager.instance.increaseScore();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

