using System;

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
    public string player1DisplayName;
    public string player2;
    public string player2DisplayName;
    public string gameID;
    public float player1Time;
    public float player2Time;
}
