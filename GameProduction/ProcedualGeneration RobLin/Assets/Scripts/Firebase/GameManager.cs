using Firebase.Auth;
using Firebase.Database;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI status;  //So we can tell the user whats going on
    UserInfo user;                  //ref to our user
    string userID;                  //ref to userID for easy access
    FireBaseManager fbManager;      //ref to FirebaseManager Instance for easy access

    // Start is called before the first frame update
    void Start()
    {
        //Ref for our userID
        userID = FirebaseAuth.DefaultInstance.CurrentUser.UserId;

        //Create ref for our firebase Instance
        fbManager = FireBaseManager.Instance;

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
	private void LoadedUser(string jsonData)
	{
        Log("Processing user data: " + userID);

        //If we cant find any user data we need to create it
        if (jsonData == null || jsonData == "")
		{
            Log("No user data found, creating new user data");

            user = new UserInfo();
            user.activeGame = "";
            StartCoroutine(fbManager.SaveData("users/" + userID, JsonUtility.ToJson(user)));
		}
		else
		{
            //We found user data
            user = JsonUtility.FromJson<UserInfo>(jsonData);
		}

        //We now have a user, lets check if our user have an active game.
        CheckedActiveGame();
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
            var newGame = new GameInfo();
            newGame.player1 = userID;
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
            game.player2 = userID;
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
		}
		else
		{
            GameLoaded(JsonUtility.FromJson<GameInfo>(jsonData));
		}
    }

    private void GameLoaded(GameInfo game)
    {
        Log("Game has been loaded");
        GetComponent<Game>().StartGame(game, userID);
    }
}
