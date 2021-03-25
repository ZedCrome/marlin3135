using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    private LevelManager levelManager;

    private void Start()
    {
        levelManager = LevelManager.instance;
    }

    public void StartTheGame()
    {
        gameObject.SetActive(false);
        levelManager.LoadScene("Car-Maze");
    }
}
