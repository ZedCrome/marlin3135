using UnityEngine;

public class PlayerMouse : MonoBehaviour
{
    PlayerMovement playerMovement;
    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }
    
    void Update()
    {
        Vector3 move = Vector3.zero;
        if (Input.GetMouseButton(0))
        {
            Vector3 mouse = Input.mousePosition;  
            move = mouse - Camera.main.WorldToScreenPoint(transform.position);
            move = move;
        }

        if (move.sqrMagnitude < 1000)
        {
            move = Vector3.zero;    
        }
        
        playerMovement.MovePlayer(move);
    }
}
