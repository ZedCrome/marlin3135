using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

public class RandomTextSelector : MonoBehaviour
{
    [SerializeField] private TMP_Text Prompt;
    
    private string red = "<color=red>";
    private string noColor = "</color>";

    
    public TMP_InputField InputField;
    private string InputFieldText = null;

    List<char> promptChars = new List<char>();
    List<char> writtenChars = new List<char>();
    
    // Start is called before the first frame update
    void Start()
    {
        Prompt.text = "This is a test sentence.";
        
    }

    private void Update()
    {
        if (!Input.anyKeyDown) return;
        if (Input.GetMouseButtonDown(1) || Input.GetMouseButtonUp(1)) return;

        if (InputField.text != InputFieldText)
        {
            if (Input.GetKeyDown(KeyCode.Backspace))
            {
                writtenChars.RemoveAt(writtenChars.Count - 1);
                writtenChars.RemoveRange(writtenChars.Count - 1, 1);

            }

            Debug.Log(writtenChars.Count);
            
            InputFieldText = InputField.text;
            writtenChars.Add(InputFieldText.Last());
        }

        

        

        for (int i = 0; i < writtenChars.Count; i++)
        {
            
        }
    }
    
    
    
    
    
    
    
    
    
}
