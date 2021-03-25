using UnityEngine;
using TMPro;

public class Typer : MonoBehaviour
{
    public WordBank wordBank;
    public TMP_Text wordOutPut;

    private string remainingWord;
    private string currentWord;
    
    // Start is called before the first frame update
    void Start()
    {
        SetCurrentWord();
    }

    
    // Update is called once per frame
    void Update()
    {
        CheckInput();
    }


    private void SetCurrentWord()
    {
        currentWord = wordBank.GetWord();
        SetRemainingWord(currentWord);
    }


    private void SetRemainingWord(string word)
    {
        remainingWord = word;
        wordOutPut.text = remainingWord;
    }


    private void CheckInput()
    {
        if (Input.anyKeyDown)
        {
            string keysPressed = Input.inputString;

            if (keysPressed.Length == 1)
                EnterLetter(keysPressed);
        }
    }


    public void EnterLetter(string letter)
    {
        if (IsCorrectLetter(letter))
        {
            RemoveLetter();

            if (IsWordComplete())
                SetCurrentWord();
        }
    }


    private bool IsCorrectLetter(string letter)
    {
        return remainingWord.IndexOf(letter) == 0;
    }


    private void RemoveLetter()
    {
        string newWord = remainingWord.Remove(0, 1);
        SetRemainingWord(newWord);
    }


    public bool IsWordComplete()
    {
        return remainingWord.Length == 0;
    }


}
