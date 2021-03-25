using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Firebase.Database;
using UnityEngine.UI;
using System;

public class Game : MonoBehaviour
{
	public TMP_Text promptText;
	public char[] promptChars;
	public TMP_InputField playerInput;
	
	
	public TextMeshProUGUI status;				//So we can tell the user whats going on
		public TextMeshProUGUI player1UI;			//P1 name
		public TextMeshProUGUI player2UI;			//P2 name

		public TextMeshProUGUI player1scoreUI;		//P1 time
		public TextMeshProUGUI player2scoreUI;		//P2 time

		public TextMeshProUGUI gameStatus;			//how the round went
		public TextMeshProUGUI nextRoundStatus;		//current status for the new round

		bool player2 = false;						//if we are player 1 or 2
		GameInfo game;								//Holder for our game info


		public void Start()
		{
			promptText.text = "This is an example text..!#";
			for (int i = 0; i < promptText.text.Length - 1; i++)
			{
				promptChars[i] = promptText.text[i];
				Debug.Log(promptChars[i]);
			}
		}

		//Setup for our game
		internal void StartGame(GameInfo newGame, string userID)
		{
			//Stor our game in the class
			game = newGame;

			//check if we are player one or two
			if (game.player1 != userID)
				player2 = true;
			
			//make sure we have move lists.
			// if (game.p1moves == null)
			// 	game.p1moves = new List<Move>();
			//
			// if (game.p2moves == null)
			// 	game.p2moves = new List<Move>();

			//Set player names
			player1UI.text = game.player1;
			player2UI.text = game.player2;

			//If we don't have a player 2.
			if (game.player2 == "" || game.player2 == null)
				status.text = "Waiting for opponent to join";

			//Update our buttons
			UpdateButtons();

			//Listen to changes in our opponent moves:
			if (player2)
				FirebaseDatabase.DefaultInstance.GetReference("games/" + game.gameID).Child("p1moves").ValueChanged += UpdateGame;
			else
				FirebaseDatabase.DefaultInstance.GetReference("games/" + game.gameID).Child("p2moves").ValueChanged += UpdateGame;
		}

		//Will run when our game opponent makes a move
		public void UpdateGame(object sender, ValueChangedEventArgs args)
		{
			status.text = "updating game";

			if (args.DatabaseError != null)
			{
				Debug.LogError(args.DatabaseError.Message);
				return;
			}
			string jsonData = args.Snapshot.GetRawJsonValue();
			
			//// BEGIN UGLY HACK

			//convert our data to a list of moves
			//This can probebly be done in a much better way
			jsonData = jsonData.TrimStart('[');
			jsonData = jsonData.TrimEnd(']');
			var values = jsonData.Split(',');
			List<Move> moves = new List<Move>();
			foreach (var item in values)
				moves.Add((Move)int.Parse(item));

			//// END UGLY HACK
			

			//if we are player 2, we get the moves for player 1 and vice versa
			// if (player2)
			// 	game.p1moves = moves;
			// else
			// 	game.p2moves = moves;

			UpdateButtons();
			DisplayResults();
		}

		private void DisplayResults()
		{
			status.text = "displaying results";

			//calculate the number of completed rounds, lowest common amount of rounds
			// int rounds = Mathf.Min(game.p1moves.Count, game.p2moves.Count);
			int p1score = 0;
			int p2score = 0;

			//Calculate scores
			// for (int i = 0; i < rounds; i++)
			// {
			// 	p1score += CheckRound(game.p1moves[i], game.p2moves[i]);
			// 	p2score += CheckRound(game.p2moves[i], game.p1moves[i]);
			// }

			//show scores in the UI
			player1scoreUI.text = "Score: " + p1score;
			player2scoreUI.text = "Score: " + p2score;

			//Show what happend
			//gameStatus.text = game.p1moves[rounds - 1].ToString() + " vs " + game.p2moves[rounds - 1].ToString();

			//Tell the user who won
			//If they are the same, it's a draw
			// if (game.p1moves[rounds - 1] == game.p2moves[rounds - 1])
			// {
			// 	//add a new lite to the game status, with more info.
			// 	gameStatus.text += "\n the round is a draw!";
			// }
			// else
			// {
			// 	if (player2)
			// 	{
			// 		//check last round, who won. If you did't win and it's not a draw, you lost.
			// 		if (CheckRound(game.p2moves[rounds -1], game.p1moves[rounds - 1]) == 1)
			// 			gameStatus.text += "\n You won the round!";
			// 		else
			// 			gameStatus.text += "\n You lost the round!";
			// 	}
			// 	else
			// 	{
			// 		if (CheckRound(game.p1moves[rounds - 1], game.p2moves[rounds - 1]) == 1)
			// 			gameStatus.text += "\n You won the round!";
			// 		else
			// 			gameStatus.text += "\n You lost the round!";
			// 	}
			// }

			//Show status for the next round.
			// if (player2)
			// {
			// 	if (game.p1moves.Count > game.p2moves.Count)
			// 		nextRoundStatus.text = "Opponent is waiting for your move";
			// 	else if (game.p1moves.Count == game.p2moves.Count)
			// 		nextRoundStatus.text = "New Round, make your move!";
			// 	else
			// 		nextRoundStatus.text = "Waiting for opponent...";
			// }
			// else
			// {
			// 	if (game.p2moves.Count > game.p1moves.Count)
			// 		nextRoundStatus.text = "Opponent is waiting for your move";
			// 	else if (game.p1moves.Count == game.p2moves.Count)
			// 		nextRoundStatus.text = "New Round, make your move!";
			// 	else
			// 		nextRoundStatus.text = "Waiting for opponent...";
			// }

			//Check for victory
			//These are mainly getting started comments for you to work with
			if (p1score >= 5)
			{
				//player 1 wins the game!

				//Set game status to completed
				game.status = "completed";

				//If the other person has marked game as completed, we can remove it!
			}
			//Same check but for player 2.
		}

		//Return 1 if we win, 0 if it's a draw or loss.
		//This can be done super cool with bit-shift stuff.
		private int CheckRound(Move move1, Move move2)
		{
			if (move1 == Move.Rock && move2 == Move.Scissor)
				return 1;
			else if (move1 == Move.Scissor && move2 == Move.Paper)
				return 1;
			else if (move1 == Move.Paper && move2 == Move.Rock)
				return 1;

			return 0;
		}

		private void UpdateButtons()
		{
			//turn off all buttons
			foreach (var item in gameButtons)
				item.interactable = false;

			//Turn them back on if its our turn and we are player 1
			// if (!player2 && game.p1moves.Count <= game.p2moves.Count)
			// 	foreach (var button in gameButtons)
			// 		button.interactable = true;
			//
			// //Turn them back on if its our turn and we are player 2
			// if (player2 && game.p2moves.Count <= game.p1moves.Count)
			// 	foreach (var button in gameButtons)
			// 		button.interactable = true;
		}

		//Gets called when we select a move
		public void MakeMove(int input)
		{
			//convert to Move enum
			var newMove = (Move)input;

			//Add our move to the list
			// if (player2)
			// 	game.p2moves.Add(newMove);
			// else
			// 	game.p1moves.Add(newMove);

			//Update our game
			UpdateButtons();
			DisplayResults();

			//Save our game to firebase
			SaveGame();
		}

		void SaveGame()
		{
			//Save our game to the database (so our opponent gets new data)
			StartCoroutine(FireBaseManager.Instance.SaveData("games/" + game.gameID, JsonUtility.ToJson(game)));
		}

		//remove our listeners when we go back into the menu or exits the game
		//(otherwise we will get errors and strange behaviours)
		public void OnDestroy()
		{
			if (player2)
				FirebaseDatabase.DefaultInstance.GetReference("games/" + game.gameID).Child("p1moves").ValueChanged -= UpdateGame;
			else
				FirebaseDatabase.DefaultInstance.GetReference("games/" + game.gameID).Child("p2moves").ValueChanged -= UpdateGame;
		}

}
