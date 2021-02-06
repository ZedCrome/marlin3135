using UnityEngine;

public class PlayerKeyboard : MonoBehaviour
{
    PlayerMovement playerMovement;
    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        var move = new Vector3(x, y, 0);
        
        playerMovement.MovePlayer(move);
    }
}
