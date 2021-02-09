﻿using UnityEngine;
using System.IO;
using System.Text;
using System.Net;
using Firebase.Extensions;
using Firebase.Database;
using Firebase.Auth;
using Unity.Mathematics;

public class SaveManager : MonoBehaviour
{

	private int amountOfPlayers = 2;
	// Start is called before the first frame update
	void Start()
	{
		//LoadData();
	}
	
	public void SaveNewUser()
	{
		//Get player info

		//Create holder object that contains multiple players
		var multiplePlayers = new MultiplePlayers();
		multiplePlayers.players = new PlayerInfo[amountOfPlayers];

		//put info in playerinfo class
		for (int i = 0; i < amountOfPlayers; i++)
		{
			multiplePlayers.players[i] = new PlayerInfo();
			multiplePlayers.players[i].Position = new Vector3(i, 0, 0);
			multiplePlayers.players[i].Name = "";
			multiplePlayers.players[i].rotation = new Quaternion(0, 0, 0, 0);
		}

		//turn class into json 
		string jsonString = JsonUtility.ToJson(multiplePlayers);

		//Save the json String (can be done in different ways.
		//PlayerPrefs.SetString("json", jsonString);
		//SaveToFile("CarGameSaveFile", jsonString);
		//SaveOnline("CarGameSaveFile", jsonString);
		SaveToFirebase(jsonString);
	}

	//Collects game data, turns it to json and calls save function
	public void SaveData()
	{
		//Get player info
		var players = FindObjectsOfType<PlayerMovement>();

		//Create holder object that contains multiple players
		var multiplePlayers = new MultiplePlayers();
		multiplePlayers.players = new PlayerInfo[players.Length];

		//put info in playerinfo class
		for (int i = 0; i < players.Length; i++)
		{
			multiplePlayers.players[i] = new PlayerInfo();
			multiplePlayers.players[i].Position = players[i].transform.position;
			multiplePlayers.players[i].Name = players[i].name;
			multiplePlayers.players[i].rotation = players[i].transform.rotation;
		}

		//turn class into json 
		string jsonString = JsonUtility.ToJson(multiplePlayers);

		//Save the json String (can be done in different ways.
		//PlayerPrefs.SetString("json", jsonString);
		//SaveToFile("CarGameSaveFile", jsonString);
		//SaveOnline("CarGameSaveFile", jsonString);
		SaveToFirebase(jsonString);
	}
	
	private void SaveNames(string jsonString, string[] name)
	{
		//Convert our save data to a class
		var multiplePlayers = JsonUtility.FromJson<MultiplePlayers>(jsonString);
		//Find all players in the scene

		//Update Players.
		for (int i = 0; i < multiplePlayers.players.Length; i++)
		{
			multiplePlayers.players[i].Name = name[i];
			multiplePlayers.players[i].Position = multiplePlayers.players[i].Position;
		}

		jsonString = JsonUtility.ToJson(multiplePlayers);
		SaveToFirebase(jsonString);

	}
	
	private void GiveNames(string jsonString)
	{
		//Convert our save data to a class
		var multiplePlayers = JsonUtility.FromJson<MultiplePlayers>(jsonString);
		//Find all players in the scene

		string[] name = new string[multiplePlayers.players.Length];

		//Update Players.
		for (int i = 0; i < multiplePlayers.players.Length; i++)
		{
			name[i] = multiplePlayers.players[i].Name;
		}
		FindObjectOfType<SaveNames>().SetNames(name);
	}

	private void SetCarNames(string jsonString)
	{
		//Convert our save data to a class
		var multiplePlayers = JsonUtility.FromJson<MultiplePlayers>(jsonString);
		//Find all players in the scene

		string[] name = new string[multiplePlayers.players.Length];

		//Update Players.
		for (int i = 0; i < multiplePlayers.players.Length; i++)
		{
			name[i] = multiplePlayers.players[i].Name;
		}
		FindObjectOfType<NameTagManager>().SpawnNameTags(name);
	}

	//Saves to firebase
	private void SaveToFirebase(string data)
	{
		var db = FirebaseDatabase.DefaultInstance;
		var userId = FirebaseAuth.DefaultInstance.CurrentUser.UserId;
		db.RootReference.Child("users").Child(userId).SetRawJsonValueAsync(data);
		//This is done without callback or check that it worked. This is a base example.
	}

	//Selects a way to load data and runs the load state function with that data.
	public void LoadData()
	{
		//Load from firebase, this will call load state once its done
		LoadFromFirebase();

		#region OldSaveExamples       
		//load from player prefs
		//string jsonString = PlayerPrefs.GetString("json");

		//load from file
		//string jsonString2 = Load("CarGameSaveFile");

		//load from server
		//string jsonString = LoadOnline("CarGameSaveFile");

		//Compare if local save is the same as server
		//if (jsonString != jsonString2)
		//{
		//	Debug.LogError("Not the same!!!!");
		//}

		//load using json data.
		//LoadState(jsonString);
		#endregion
	}

	//The function that acctually updates stuff in our game
	private void LoadState(string jsonString)
	{
		//Convert our save data to a class
		var multiplePlayers = JsonUtility.FromJson<MultiplePlayers>(jsonString);
		//Find all players in the scene
		var players = FindObjectsOfType<PlayerMovement>();

		string[] name = new string[2];
		
		//Update Players.
		for (int i = 0; i < players.Length; i++)
		{
			name[i] = multiplePlayers.players[i].Name;
			players[i].name = multiplePlayers.players[i].Name;
			players[i].transform.position = multiplePlayers.players[i].Position;
			players[i].transform.rotation = multiplePlayers.players[i].rotation;
		}

		//Tell our nametag manager to update our nametags.
		FindObjectOfType<NameTagManager>().UpdateNameTags(name);
	}

	//Load data from firebase
	private void LoadFromFirebase()
	{
		var db = FirebaseDatabase.DefaultInstance;
		var userId = FirebaseAuth.DefaultInstance.CurrentUser.UserId;
		var dataTask = db.RootReference.Child("users").Child(userId).GetValueAsync().ContinueWithOnMainThread(task =>
		{
			if (task.Exception != null)
			{
				Debug.LogError(task.Exception);
			}

			DataSnapshot snap = task.Result;
			//Update the game with our loaded data
			LoadState(snap.GetRawJsonValue());
		});
	}
	
	public void SaveNamesToFireBase(string[] name)
	{
		var db = FirebaseDatabase.DefaultInstance;
		var userId = FirebaseAuth.DefaultInstance.CurrentUser.UserId;
		var dataTask = db.RootReference.Child("users").Child(userId).GetValueAsync().ContinueWithOnMainThread(task =>
		{
			if (task.Exception != null)
			{
				Debug.LogError(task.Exception);
			}

			DataSnapshot snap = task.Result;
			//Update the game with our loaded data
			SaveNames(snap.GetRawJsonValue(),name);
		});
	}

	public void LoadNamesFromFirebase()
	{
		var db = FirebaseDatabase.DefaultInstance;
		var userId = FirebaseAuth.DefaultInstance.CurrentUser.UserId;
		var dataTask = db.RootReference.Child("users").Child(userId).GetValueAsync().ContinueWithOnMainThread(task =>
		{
			if (task.Exception != null)
			{
				Debug.LogError(task.Exception);
			}

			DataSnapshot snap = task.Result;
			//Update the game with our loaded data
			GiveNames(snap.GetRawJsonValue());
		});
	}

	public void SetNamesToCars()
	{
		var db = FirebaseDatabase.DefaultInstance;
		var userId = FirebaseAuth.DefaultInstance.CurrentUser.UserId;
		var dataTask = db.RootReference.Child("users").Child(userId).GetValueAsync().ContinueWithOnMainThread(task =>
		{
			if (task.Exception != null)
			{
				Debug.LogError(task.Exception);
			}

			DataSnapshot snap = task.Result;
			//Update the game with our loaded data
			SetCarNames(snap.GetRawJsonValue());
		});
	}

	
	
	#region OldSaveAlternatives
	//Save to local file
	public void SaveToFile(string fileName, string jsonString)
	{
		// Open a file in write mode. This will create the file if it's missing.
		// It is assumed that the path already exists.
		using (var stream = File.OpenWrite(Application.persistentDataPath + "\\" + fileName))
		{
			// Truncate the file if it exists (we want to overwrite the file)
			stream.SetLength(0);

			// Convert the string into bytes. Assume that the character-encoding is UTF8.
			// Do you not know what encoding you have? Then you have UTF-8
			var bytes = Encoding.UTF8.GetBytes(jsonString);

			// Write the bytes to the hard-drive
			stream.Write(bytes, 0, bytes.Length);

			// The "using" statement will automatically close the stream after we leave
			// the scope - this is VERY important
		}
	}

	//Loads from local savefile
	public string Load(string fileName)
	{
		// Open a stream for the supplied file name as a text file
		using (var stream = File.OpenText(Application.persistentDataPath + "\\" + fileName))
		{
			// Read the entire file and return the result. This assumes that we've written the
			// file in UTF-8
			return stream.ReadToEnd();
		}
	}

	//Loads from JsonSlave
	public string LoadOnline(string name)
	{
		var request = (HttpWebRequest)WebRequest.Create("http://localhost:8080/" + name);
		var response = (HttpWebResponse)request.GetResponse();

		// Open a stream to the server so we can read the response data it sent back from our GET request
		using (var stream = response.GetResponseStream())
		{
			using (var reader = new StreamReader(stream))
			{
				// Read the entire body as a string
				var body = reader.ReadToEnd();

				return body;
			}
		}
	}

	//Saves the playerInfo string on the server (JsonSlave).
	public void SaveOnline(string fileName, string saveData)
	{
		//url
		var request = (HttpWebRequest)WebRequest.Create("http://localhost:8080/" + fileName);
		request.ContentType = "application/json";
		request.Method = "PUT";

		using (var streamWriter = new StreamWriter(request.GetRequestStream()))
		{
			streamWriter.Write(saveData);
		}

		var httpResponse = (HttpWebResponse)request.GetResponse();
		using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
		{
			var result = streamReader.ReadToEnd();
		}
	}
#endregion
}
