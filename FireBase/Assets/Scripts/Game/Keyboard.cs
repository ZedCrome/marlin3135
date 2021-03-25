using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keyboard : MonoBehaviour
{
    public Typer typer;
    public string singleLetter;
    
    public void Alphabet(string letter)
    {
        singleLetter = letter;
        typer.EnterLetter(singleLetter);
        Debug.Log(letter);
    }
}
