using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NameTagManager : MonoBehaviour
{
    public GameObject nameTagPrefab;
    public Vector3 offset;
    public GameObject[] players;

    internal GameObject[] nameTags;
    
    // Start is called before the first frame update
    void Awake()
    {
        nameTags = new GameObject[players.Length];

        var parent = GameObject.Find("Canvas").transform;
        
        for (int i = 0; i < nameTags.Length; i++)
        {
            nameTags[i] = Instantiate(nameTagPrefab, parent);
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < players.Length; i++)
        {
            nameTags[i].transform.position = Camera.main.WorldToScreenPoint(players[i].transform.position + offset);
        }
    }
}
