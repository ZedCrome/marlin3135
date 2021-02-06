using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SaveManager : MonoBehaviour
{

    public GameObject player1;

    public GameObject player2;
    // Start is called before the first frame update
    void Start()
    {
        LoadData();
        LoadNames();
        
        
    }

    public void SaveData()
    {
        Debug.Log("Saving...");
        PlayerPrefs.SetFloat("p1-pos-x", player1.transform.position.x);
        PlayerPrefs.SetFloat("p1-pos-y", player1.transform.position.y);
        
        PlayerPrefs.SetFloat("p2-pos-x", player2.transform.position.x);
        PlayerPrefs.SetFloat("p2-pos-y", player2.transform.position.y);
        
    }

    public void LoadData()
    {
        Debug.Log("Loading...");
        Vector3 pos = Vector3.zero;
        pos.x = PlayerPrefs.GetFloat("p1-pos-x");
        pos.y = PlayerPrefs.GetFloat("p1-pos-y");

        player1.transform.position = pos;
        
        pos.y = PlayerPrefs.GetFloat("p2-pos-y");
        pos.x = PlayerPrefs.GetFloat("p2-pos-x");

        player2.transform.position = pos;
    }

    public void LoadNames()
    {
        var nameTagManager = GetComponent<NameTagManager>();

        for (int i = 0; i < nameTagManager.nameTags.Length; i++)
        {
            nameTagManager.nameTags[i].GetComponent<TMP_Text>().text = PlayerPrefs.GetString("p" + (i+1) + "-name");
        }
    }

    public void SaveNames()
    {
        
    }
}
