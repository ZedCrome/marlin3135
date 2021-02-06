using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void MovePlayer(Vector3 movement)
    {
        movement = movement.normalized;

        if (movement.sqrMagnitude > 0.01)
        {
            transform.up = movement;
            transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z);

            rb.velocity = transform.up * speed;
            
        }else
        {
            //rb.velocity = Vector3.zero;
        }
    }
}
