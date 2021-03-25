// using System;
// using System.Collections.Generic;
// using UnityEngine;
// using System.IO;
// using System.Text;
// using System.Net;
// using Firebase.Extensions;
// using Firebase.Database;
// using Firebase.Auth;
// using Unity.Mathematics;
//
// public class SaveManager : MonoBehaviour
// {
//
// 	private int amountOfPlayers = 2;
//
// 	void Start()
// 	{
// 		//LoadData();
// 	}
//
// 	public void CreateGame()
// 	{
// 		Debug.Log("trying to create game..");
// 		var newGame = new MultipleGames();
// 		newGame.game = new GameInfo();
// 		newGame.game.players = new PlayerInfo[amountOfPlayers];
// 		
//
// 		for (int i = 0; i < amountOfPlayers; i++)
// 		{
// 			newGame.game.players[i] = new PlayerInfo();
// 		}
//
// 		var jsonString = JsonUtility.ToJson(newGame);
// 		Debug.Log(jsonString);
//
// 		SaveGameToFirebase(jsonString);
// 	}
//
// 	public void SaveNewUser()
// 	{
// 		//Get player info
//
// 		//Create holder object that contains multiple players
// 		var multiplePlayers = new MultiplePlayers();
// 		multiplePlayers.playerInfo = new PlayerInfo[amountOfPlayers];
//
// 		//put info in playerinfo class
// 		for (int i = 0; i < amountOfPlayers; i++)
// 		{
// 			multiplePlayers.playerInfo[i] = new PlayerInfo();
// 		}
//
// 		//turn class into json 
// 		string jsonString = JsonUtility.ToJson(multiplePlayers);
// 		Debug.Log(jsonString);
//
// 		SaveUserToFirebase(jsonString);
// 	}
//
//
// 	//Collects game data, turns it to json and calls save function
// 	public void SaveData()
// 	{
// 		//Get player info
// 		var players = FindObjectsOfType<PlayerMovement>();
//
// 		//Create holder object that contains multiple players
// 		var multiplePlayers = new MultiplePlayers();
// 		multiplePlayers.playerInfo = new PlayerInfo[players.Length];
//
// 		//put info in playerinfo class
// 		for (int i = 0; i < amountOfPlayers; i++)
// 		{
// 			multiplePlayers.playerInfo[i] = new PlayerInfo();
// 			multiplePlayers.playerInfo[i].name = players[i].name;
// 		}
//
// 		//turn class into json 
// 		string jsonString = JsonUtility.ToJson(multiplePlayers);
//
// 		SaveUserToFirebase(jsonString);
// 	}
//
//
// 	private void SaveNames(string jsonString, string[] name)
// 	{
// 		//Convert our save data to a class
// 		var multiplePlayers = JsonUtility.FromJson<MultiplePlayers>(jsonString);
// 		//Find all players in the scene
//
// 		//Update Players.
// 		for (int i = 0; i < multiplePlayers.playerInfo.Length; i++)
// 		{
// 			multiplePlayers.playerInfo[i].name = name[i];
// 			//multiplePlayers.players[i].Position = multiplePlayers.players[i].Position;
// 		}
//
// 		jsonString = JsonUtility.ToJson(multiplePlayers);
// 		SaveUserToFirebase(jsonString);
// 	}
//
//
// 	private void GiveNames(string jsonString)
// 	{
// 		//Convert our save data to a class
// 		var multiplePlayers = JsonUtility.FromJson<MultiplePlayers>(jsonString);
// 		//Find all players in the scene
//
// 		string[] name = new string[multiplePlayers.playerInfo.Length];
//
// 		//Update Players.
// 		for (int i = 0; i < multiplePlayers.playerInfo.Length; i++)
// 		{
// 			name[i] = multiplePlayers.playerInfo[i].name;
// 		}
//
// 		FindObjectOfType<SaveNames>().SetNames(name);
// 	}
//
//
// 	private void SetCarNames(string jsonString)
// 	{
// 		//Convert our save data to a class
// 		var multiplePlayers = JsonUtility.FromJson<MultiplePlayers>(jsonString);
// 		//Find all players in the scene
//
// 		string[] name = new string[multiplePlayers.playerInfo.Length];
//
// 		//Update Players.
// 		for (int i = 0; i < multiplePlayers.playerInfo.Length; i++)
// 		{
// 			name[i] = multiplePlayers.playerInfo[i].name;
// 		}
//
// 		FindObjectOfType<NameTagManager>().SpawnNameTags(name);
// 	}
//
//
// 	//Saves to firebase
// 	private void SaveUserToFirebase(string data)
// 	{
// 		var db = FirebaseDatabase.DefaultInstance;
// 		var userId = FirebaseAuth.DefaultInstance.CurrentUser.UserId;
// 		db.RootReference.Child("users").Child(userId).SetRawJsonValueAsync(data);
// 		//This is done without callback or check that it worked. This is a base example.
// 	}
//
// 	private void SaveGameToFirebase(string data)
// 	{
// 		if (data == String.Empty || data == null)
// 		{
// 			Debug.LogError("the data string is empty.");
// 		}
//
// 		var db = FirebaseDatabase.DefaultInstance;
// 		var userId = FirebaseAuth.DefaultInstance.CurrentUser.UserId;
// 		db.RootReference.Child("games").SetRawJsonValueAsync(data);
// 	}
//
//
// 	//Selects a way to load data and runs the load state function with that data.
// 	public void LoadData()
// 	{
// 		//Load from firebase, this will call load state once its done
// 		LoadFromFirebase();
//
// 		#region OldSaveExamples
//
// 		//load from player prefs
// 		//string jsonString = PlayerPrefs.GetString("json");
//
// 		//load from file
// 		//string jsonString2 = Load("CarGameSaveFile");
//
// 		//load from server
// 		//string jsonString = LoadOnline("CarGameSaveFile");
//
// 		//Compare if local save is the same as server
// 		//if (jsonString != jsonString2)
// 		//{
// 		//	Debug.LogError("Not the same!!!!");
// 		//}
//
// 		//load using json data.
// 		//LoadState(jsonString);
//
// 		#endregion
// 	}
//
//
// 	//The function that acctually updates stuff in our game
// 	private void LoadState(string jsonString)
// 	{
// 		//Convert our save data to a class
// 		var multiplePlayers = JsonUtility.FromJson<MultiplePlayers>(jsonString);
// 		//Find all players in the scene
// 		var players = FindObjectsOfType<PlayerMovement>();
//
// 		string[] name = new string[2];
//
// 		//Update Players.
// 		for (int i = 0; i < players.Length; i++)
// 		{
// 			name[i] = multiplePlayers.playerInfo[i].name;
// 			players[i].name = multiplePlayers.playerInfo[i].name;
// 			// players[i].transform.position = multiplePlayers.players[i].Position;
// 			// players[i].transform.rotation = multiplePlayers.players[i].rotation;
// 		}
//
// 		//Tell our nametag manager to update our nametags.
// 		FindObjectOfType<NameTagManager>().UpdateNameTags(name);
// 	}
//
//
// 	//Load data from firebase
// 	private void LoadFromFirebase()
// 	{
// 		var db = FirebaseDatabase.DefaultInstance;
// 		var userId = FirebaseAuth.DefaultInstance.CurrentUser.UserId;
// 		var dataTask = db.RootReference.Child("users").Child(userId).GetValueAsync().ContinueWithOnMainThread(
// 			task =>
// 			{
// 				if (task.Exception != null)
// 				{
// 					Debug.LogError(task.Exception);
// 				}
//
// 				DataSnapshot snap = task.Result;
// 				//Update the game with our loaded data
// 				LoadState(snap.GetRawJsonValue());
// 			});
// 	}
//
//
// 	public void SaveNamesToFireBase(string[] name)
// 	{
// 		var db = FirebaseDatabase.DefaultInstance;
// 		var userId = FirebaseAuth.DefaultInstance.CurrentUser.UserId;
// 		var dataTask = db.RootReference.Child("users").Child(userId).GetValueAsync().ContinueWithOnMainThread(
// 			task =>
// 			{
// 				if (task.Exception != null)
// 				{
// 					Debug.LogError(task.Exception);
// 				}
//
// 				DataSnapshot snap = task.Result;
// 				//Update the game with our loaded data
// 				SaveNames(snap.GetRawJsonValue(), name);
// 			});
// 	}
//
//
// 	public void LoadNamesFromFirebase()
// 	{
// 		var db = FirebaseDatabase.DefaultInstance;
// 		var userId = FirebaseAuth.DefaultInstance.CurrentUser.UserId;
// 		var dataTask = db.RootReference.Child("users").Child(userId).GetValueAsync().ContinueWithOnMainThread(
// 			task =>
// 			{
// 				if (task.Exception != null)
// 				{
// 					Debug.LogError(task.Exception);
// 				}
//
// 				DataSnapshot snap = task.Result;
// 				//Update the game with our loaded data
// 				GiveNames(snap.GetRawJsonValue());
// 			});
// 	}
//
//
// 	public void SetNamesToCars()
// 	{
// 		var db = FirebaseDatabase.DefaultInstance;
// 		var userId = FirebaseAuth.DefaultInstance.CurrentUser.UserId;
// 		var dataTask = db.RootReference.Child("users").Child(userId).GetValueAsync().ContinueWithOnMainThread(
// 			task =>
// 			{
// 				if (task.Exception != null)
// 				{
// 					Debug.LogError(task.Exception);
// 				}
//
// 				DataSnapshot snap = task.Result;
// 				//Update the game with our loaded data
// 				SetCarNames(snap.GetRawJsonValue());
// 			});
// 	}
// }

