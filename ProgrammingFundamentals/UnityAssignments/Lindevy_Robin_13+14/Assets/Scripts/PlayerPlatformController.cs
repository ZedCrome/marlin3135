using UnityEngine;

public class PlayerPlatformController : MonoBehaviour
{
    public float speed = 5;
    public float jumpPower = 10;
    private bool jump = false;
    public bool isGrounded = false;

    Rigidbody2D rb2d;
    Vector2 movement = new Vector2();

    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private static readonly int Speed = Animator.StringToHash("Speed");
    private static readonly int Fall = Animator.StringToHash("Fall");

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //Movement
        float x = Input.GetAxis("Horizontal");

        movement = new Vector2(x * speed, rb2d.velocity.y);

        animator.SetFloat(Speed, Mathf.Abs(x));
        animator.SetFloat(Fall, rb2d.velocity.y);

        if (x < 0)
            spriteRenderer.flipX = true;
        else if (x > 0)
            spriteRenderer.flipX = false;

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            jump = true;
            rb2d.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        }
            
    }

    
    //Update is called every physics interval (0.02s) before physics
    private void FixedUpdate()
    {
        if (jump)
        {
            animator.SetTrigger("Jump");
            jump = false;
        }
        rb2d.velocity = movement;
    }

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        animator.SetTrigger("Landed");
        isGrounded = true;
    }

    
    private void OnTriggerExit2D(Collider2D collision)
    {
        isGrounded = false;
    }
}
