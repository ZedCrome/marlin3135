﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SaveNames : MonoBehaviour
{
    public TMP_InputField name1;
    public TMP_InputField name2;

    // Start is called before the first frame update

    public void SetNames(string[] name)
    {
        name1.text = name[0];
        name2.text = name[1];
    }

    public void SavePlayerNames()
    {
        string[] name = new string[2];
        name[0] = name1.text;
        name[1] = name2.text;
        FindObjectOfType<SaveManager>().SaveNamesToFireBase(name);
    }
}
