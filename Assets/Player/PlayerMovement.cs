using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Public Variables
    public float Speed;
    public float Jump;
    
    // Private Variables
    private float Move;
    private bool isJumping;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(Speed * Move, rb.velocity.y);

        if((Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.W)) && isJumping == false)
        {
            rb.AddForce(new Vector2(rb.velocity.x, Jump));
        }

        if(Input.GetKeyDown(KeyCode.S))
        {
            transform.localScale = transform.localScale + new Vector3(0, -.6f, 0);
        }
        if(Input.GetKeyUp(KeyCode.S))
        {
            transform.localScale = transform.localScale + new Vector3(0, .6f, 0);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Ground"))
        {
            isJumping = true;
        }
    }
}
