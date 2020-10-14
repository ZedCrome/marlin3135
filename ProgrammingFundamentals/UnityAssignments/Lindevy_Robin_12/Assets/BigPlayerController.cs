using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigPlayerController : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb2d;

    Vector3 move = new Vector3();

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");


        move = new Vector3(x, y, 0);


        transform.Translate(move * speed * Time.deltaTime);
    }
}
