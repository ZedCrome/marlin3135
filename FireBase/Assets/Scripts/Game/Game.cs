
using System.Net.NetworkInformation;
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
		internal void StartGame(GameInfo newGame, string userID)
		{
			//Stor our game in the class
			game = newGame;
			

			//check if we are player one or two
			if (game.player1 != userID)
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
			if (game.player2 == "" || game.player2 == null)
				status.text = "Waiting for opponent to join";
			else
			{
				gameIsFull = true;
			}
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
			SaveGame();
			ShowWinner();
		}

		public void ShowWinner()
		{
			if (game.player1Time == 0 || game.player1Time == null)
				return;

			if (game.player2Time == 0 || game.player2Time == null)
				return;

			if (game.player1Time > game.player2Time)
				status.text = game.player2DisplayName + "wins the game!";

			if (game.player2Time > game.player1Time)
				status.text = game.player1DisplayName + "wins the game!";

			if (game.player1Time == game.player2Time)
			{
				status.text = "It's a draw! well done.";
			}
		}
		
		public void ResetTime()
		{
			game.player1Time = 0;
			game.player2Time = 0;
			SaveGame();
		}

		void SaveGame()
		{
			//Save our game to the database (so our opponent gets new data)
			StartCoroutine(FireBaseManager.Instance.SaveData("games/" + game.gameID, JsonUtility.ToJson(game)));
		}

}
