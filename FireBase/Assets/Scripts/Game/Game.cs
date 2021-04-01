
using System;
using System.Net.NetworkInformation;
using Firebase.Database;
using UnityEngine;
using TMPro;


public class Game : MonoBehaviour
{
	public TextMeshProUGUI status;				//So we can tell the user whats going on
	public TextMeshProUGUI player1UI;			//P1 name
	public TextMeshProUGUI player2UI;			//P2 name

	public TextMeshProUGUI player1scoreUI;		//P1 time
	public TextMeshProUGUI player2scoreUI;		//P2 time

	public bool player2 = false;				//if we are player 1 or 2
	public bool gameIsFull = false;
	public bool gameIsOver;
	GameInfo game;								//Holder for our game info

	UserInfo user;

	private FireBaseManager fbManager;
	public GameManager gameManager;
	string userID;



		public void Start()
		{
			//Create ref for our firebase Instance
			fbManager = FireBaseManager.Instance;
		}

		//Setup for our game
		internal void StartGame(GameInfo newGame, UserInfo user)
		{
			//Stor our game in the class
			game = newGame;
			this.user = user;

			//check if we are player one or two
			if (user.UserID != game.player1.UserID)
				player2 = true;
			
			//Set player names
			player1UI.text = newGame.player1DisplayName;
			player2UI.text = newGame.player2DisplayName;

			if (newGame.player1Time != null || newGame.player1Time != 0)
			{
				int minutes = Mathf.FloorToInt(newGame.player1Time / 60.0f);
				int seconds = Mathf.FloorToInt(newGame.player1Time - minutes * 60);
				
				player1scoreUI.text = string.Format("{0:00}:{1:00}", minutes, seconds);
			}
			
			if (newGame.player2Time != null || newGame.player2Time != 0)
			{
				int minutes = Mathf.FloorToInt(newGame.player2Time / 60.0f);
				int seconds = Mathf.FloorToInt(newGame.player2Time - minutes * 60);
				
				player2scoreUI.text = string.Format("{0:00}:{1:00}", minutes, seconds);
			}
			

			//If we don't have a player 2.
			if (game.player2 == null)
				status.text = "Waiting for opponent to join";
			else
			{
				gameIsFull = true;
			}

			FirebaseDatabase.DefaultInstance.GetReference("games/" + game.gameID).ValueChanged += UpdateGame;
			SaveGame();
		}

		private void UpdateGame(object sender, ValueChangedEventArgs e)
		{
			if (e.DatabaseError != null)
			{
				Debug.LogError(e.DatabaseError.Message);
				return;
			}

			string jsonData = e.Snapshot.GetRawJsonValue();
			game = JsonUtility.FromJson<GameInfo>(jsonData);

			if (player2)
			{
				user.wins = game.player2.wins;
				user.losses = game.player2.losses;
			}
			else
			{
				user.wins = game.player1.wins;
				user.losses = game.player1.losses;
			}

			if (game.status == "completed")
			{
				if (game.winner == "" || game.winner == null)
				{
					status.text = "It's a draw! well done.";
				}
				else
				{
					status.text = game.winner + " wins the game!";
				}
			}
			
			
			StartCoroutine(FireBaseManager.Instance.SaveData("users/" + user.UserID, JsonUtility.ToJson(user)));
		}


		public void UpdateTime(float time)
		{
			var newTime = time;

			int minutes = Mathf.FloorToInt(newTime / 60.0f);
			int seconds = Mathf.FloorToInt(newTime - minutes * 60);
		
		
			if (player2)
			{
				game.player2Time = newTime;
				player2scoreUI.text = string.Format("{0:00}:{1:00}", minutes, seconds);
			}
			else
			{
				game.player1Time = newTime;
				player1scoreUI.text = string.Format("{0:00}:{1:00}", minutes, seconds);
			}
			ShowWinner();
			SaveGame();
		}

		public void ShowWinner()
		{
			if (game.player1Time == 0)
				return;

			if (game.player2Time == 0)
				return;

			game.status = "completed";

			if (game.player1Time > game.player2Time)
			{
				game.player2.wins += 1;
				game.player1.losses += 1;
				game.winner = game.player2.name;
			}
			
			if (game.player2Time > game.player1Time)
			{
				game.player1.wins += 1;
				game.player2.losses += 1;
				game.winner = game.player1.name;
			}
		}

		
		public void SaveGame()
		{
			//Save our game to the database (so our opponent gets new data)
			StartCoroutine(FireBaseManager.Instance.SaveData("games/" + game.gameID, JsonUtility.ToJson(game)));
		}


		public void OnDestroy()
		{
			FirebaseDatabase.DefaultInstance.GetReference("games/" + game.gameID).ValueChanged -= UpdateGame;
		}
}
