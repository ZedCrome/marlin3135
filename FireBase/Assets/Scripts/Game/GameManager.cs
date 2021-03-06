﻿using System.Collections;
using System.Drawing;
using System.Threading.Tasks;
using Firebase.Auth;
using Firebase.Database;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
	
	[Header("Game")]
	string userID;                  //ref to userID for easy access
	UserInfo user;                  //ref to our user
	FireBaseManager fbManager;      //ref to FirebaseManager Instance for easy access
	public GameInfo newGame;
	public Game game;
	private FirebaseDatabase db;
	
	public TextMeshProUGUI status;

	[Header("NameSelection")]
	public TMP_InputField name;
    public GameObject nameCanvas;
	public GameObject gameCanvas;
    public bool NameIsSet = false;

    private bool p1Loaded = false;
    private bool p1Updated = false;

    private bool p2Loaded = false;
    private bool p2Updated = false;
    

    void Start()
    {
	    //Ref for our userID
	    userID = FirebaseAuth.DefaultInstance.CurrentUser.UserId;
	    
	    
	    //Create ref for our firebase Instance
	    fbManager = FireBaseManager.Instance;
	    StartCoroutine(fbManager.SaveData("users/" + userID, JsonUtility.ToJson(user)));
	    //Tell the user what's happening
	    Log("Loading data for: " + userID);
	    //Load userInfo
	    StartCoroutine(fbManager.LoadData("users/" + userID, LoadedUser));
    }

    //prints info to the console and the user
	private void Log(string message)
	{
        status.text = message;
        Debug.Log(message);
	}

	//process the user data
	public void LoadedUser(string jsonData)
	{
        Log("Processing user data: " + userID);

        //If we cant find any user data we need to create it
        if (jsonData == null || jsonData == "")
		{
            Log("No user data found, creating new user data");

            user = new UserInfo();
            user.activeGame = "";
            user.UserID = userID;
            user.name = "";
            user.wins = 0;
            user.losses = 0;
            StartCoroutine(fbManager.SaveData("users/" + userID, JsonUtility.ToJson(user)));
		}
		else
		{
            //We found user data
            user = JsonUtility.FromJson<UserInfo>(jsonData);
		}

        name.text = user.name;
	}

	private void CheckedActiveGame()
	{
        //Does our user doesn't have an active game?
		if (user.activeGame == "" || user.activeGame == null)
		{
            //Start the new game process
            Log("No active game for the user, look for a game");
            StartCoroutine(fbManager.CheckForGame("games/", NewGameLoaded));
        }
		else
		{
            //We already have a game, load it
            Log("Loading Game: " + user.activeGame);
            StartCoroutine(fbManager.LoadData("games/" + user.activeGame, GameLoaded));
        }
	}

	private void NewGameLoaded(string jsonData)
	{
        //We couldn't find a new game to join
		if (jsonData == "" || jsonData == null || jsonData == "{}")
		{
            //Create a unique ID for the new game
            string key = FirebaseDatabase.DefaultInstance.RootReference.Child("games/").Push().Key;
            string path = "games/" + key;

            //Create game structure
			newGame = new GameInfo();
            newGame.player1 = user;
            newGame.player1.wins = user.wins;
            newGame.player1.losses = user.losses;
            newGame.player1DisplayName = user.name;
            newGame.player2DisplayName = "";
            newGame.status = "new";
            newGame.gameID = key;
            
            //Save our new game
            StartCoroutine(fbManager.SaveData(path, JsonUtility.ToJson(newGame)));

            Log("Creating new game: " + key);

            //add the key to our active games
            user.activeGame = key;
            StartCoroutine(fbManager.SaveData("users/" + userID, JsonUtility.ToJson(user)));

            GameLoaded(newGame);
        }
        else
		{
            //We found a game, lets join it
            var game = JsonUtility.FromJson<GameInfo>(jsonData);
            
            //Update the game
            game.player2 = user;
            game.player2.wins = user.wins;
            game.player2.losses = user.losses;
            game.player2DisplayName = user.name;
            game.status = "full";
            StartCoroutine(fbManager.SaveData("games/" + game.gameID, JsonUtility.ToJson(game)));

            //Update the user
            user.activeGame = game.gameID;
            StartCoroutine(fbManager.SaveData("users/" + userID, JsonUtility.ToJson(user)));

            GameLoaded(game);
        }
    }

    private void GameLoaded(string jsonData)
    {
        Debug.Log(jsonData);

		if (jsonData == null || jsonData == "")
		{
            Log("no game data");
            Debug.LogError("Error while loading game data");
            user.activeGame = "";
            CheckedActiveGame();
		}
		else
		{
            GameLoaded(JsonUtility.FromJson<GameInfo>(jsonData));
		}
    }

    private void GameLoaded(GameInfo game)
    {
        Log("Game has been loaded");
        GetComponent<Game>().StartGame(game, user);

        if (newGame.status == "new")
        {
	        Log("Waiting for an opponent.");
        }
    }

    public void SaveName()
    {
	    user.name = name.text;
	    StartCoroutine(fbManager.SaveData("users/" + userID, JsonUtility.ToJson(user)));
	    if (!game.player2)
	    {
		    newGame.player1DisplayName = name.text;
	    }
	    
	    if (game.player2)
	    {
		    newGame.player2DisplayName = name.text;
	    }
    
	    nameCanvas.SetActive(false);
	    gameCanvas.SetActive(true);
	    NameIsSet = true;
	    CheckedActiveGame();
    }
    
    
    public IEnumerator SaveData(string path, string data, FireBaseManager.OnSaveDelegate onSaveDelegate = null)
    {
        var dataTask = db.RootReference.Child(path).SetRawJsonValueAsync(data);
        yield return new WaitUntil(() => dataTask.IsCompleted);

        if (dataTask.Exception != null)
            Debug.LogWarning(dataTask.Exception);

        if (onSaveDelegate != null)
        {
            onSaveDelegate();
        }
    }
}
