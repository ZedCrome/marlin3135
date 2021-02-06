using TMPro;
using UnityEngine;

public class Savenames : MonoBehaviour
{
    public TMP_InputField nameOne;
    public TMP_InputField nameTwo;
    void Start()
    {
        nameOne.text = PlayerPrefs.GetString("p1-name");
        nameTwo.text = PlayerPrefs.GetString("p2-name");
    }
    
    public void SavePlayerNames()
    {
        PlayerPrefs.SetString("p1-name", nameOne.text);
        PlayerPrefs.SetString("p2-name", nameTwo.text);
    }
}
