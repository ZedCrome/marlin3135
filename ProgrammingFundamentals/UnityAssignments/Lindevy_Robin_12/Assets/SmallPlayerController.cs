using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallPlayerController : MonoBehaviour
{
    private Rigidbody2D rb2d;
    public float speed;

    Vector2 move = new Vector2();

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>(); 
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        move = new Vector2(x, y);

        rb2d.AddForce(move * speed);
    }
}
