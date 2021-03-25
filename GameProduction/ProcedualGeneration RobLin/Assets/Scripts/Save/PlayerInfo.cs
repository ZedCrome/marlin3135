using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class UserInfo
{
    public string name;
    public string activeGame;
}

[Serializable]
public class GameInfo
{
    public string status;
    public string player1;
    public string player2;
    public string gameID;
    public float player1Time;
    public float player2Time;
}

public enum Move
{
    Rock,
    Paper,
    Scissor,
}