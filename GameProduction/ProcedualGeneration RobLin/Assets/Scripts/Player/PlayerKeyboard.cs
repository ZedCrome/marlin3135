using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKeyboard : MonoBehaviour
{
    PlayerMovement pm;

    // Start is called before the first frame update
    void Start()
    {
        pm = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y;
        if (Input.GetKeyDown(KeyCode.Space))
            y = 1;
        else
            y = 0;

        var move = new Vector3(x, y, 0);

        pm.MovePlayer(move);
    }
}
