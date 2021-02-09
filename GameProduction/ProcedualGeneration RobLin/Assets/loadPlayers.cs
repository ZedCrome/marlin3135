using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class loadPlayers : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<SaveManager>().LoadData();        
    }

}
