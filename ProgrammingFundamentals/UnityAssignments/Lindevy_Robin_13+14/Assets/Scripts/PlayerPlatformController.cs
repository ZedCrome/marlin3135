using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlatformController : MonoBehaviour
{
    public float speed = 5;
    public float jumpPower = 10;
    public bool isGrounded = false;

    Rigidbody2D rb2d;
    Vector2 movement = new Vector2();

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Movement
        float x = Input.GetAxis("Horizontal");

        movement = new Vector2(x * speed, rb2d.velocity.y);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb2d.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            //rb2d.velocity = new Vector2(rb2d.velocity.x, jumpPower);
        }
    }

    //Update is called every physics interval (0.02s) before physics
    private void FixedUpdate()
    {
        rb2d.velocity = movement;

        if(Physics2D.Raycast(transform.position, -Vector2.up, 0.1f))
        {
            isGrounded = true;
        }
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    isGrounded = true;
    //}

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    isGrounded = false;
    //}
}
