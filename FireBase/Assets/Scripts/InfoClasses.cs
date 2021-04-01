using System;

[Serializable]
public class UserInfo
{
    public string UserID;
    public string name;
    public string activeGame;
    public int wins;
    public int losses;
}

[Serializable]
public class GameInfo
{
    public string status;
    public string winner;
    public UserInfo player1;
    public UserInfo player2;
    public string player1DisplayName;
    public string player2DisplayName;
    public float player1Time;
    public float player2Time;
    public string gameID;
}
