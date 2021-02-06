using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Savenames : MonoBehaviour
{
    public TMP_InputField nameOne;

    public TMP_InputField nameTwo;
    // Start is called before the first frame update
    void Start()
    {
        nameOne.text = PlayerPrefs.GetString("p1-name");
        nameTwo.text = PlayerPrefs.GetString("p2-name");
    }

    // Update is called once per frame
    public void SavePlayerNames()
    {
        PlayerPrefs.SetString("p1-name", nameOne.text);
        PlayerPrefs.SetString("p2-name", nameTwo.text);
    }
}
