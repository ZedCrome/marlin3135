using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public Game game;
    public GameManager gameManager;
    public WordBank wordBank;
    public Typer typer;

    private bool isFinished = false;
    
    public float timePassed = 0.0f;
    public TMP_Text timer;
    public void Update()
    {
        if (isFinished)
            return;
        
        if (gameManager.NameIsSet && game.gameIsFull)
            UpdateTimer(); 
        

        int minutes = Mathf.FloorToInt(timePassed / 60.0f);
        int seconds = Mathf.FloorToInt(timePassed - minutes * 60);
        
        
        timer.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    
    public void UpdateTimer()
    {
        if (wordBank.words.Count == 0)
            if (typer.IsWordComplete())
            {
                isFinished = true;
                game.UpdateTime(timePassed);
                return;
            }

        timePassed += Time.deltaTime;
    }
    
}
