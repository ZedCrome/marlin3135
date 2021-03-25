using Firebase.Auth;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Firebase;
using Firebase.Extensions;
using Button = UnityEngine.UI.Button;

public class FireBaseLogin : MonoBehaviour
{
	//Shows the user whats happening
	public TextMeshProUGUI outputText;

	//The button to enter the lobby
	public Button playButton;

	//Login fields
	public TMP_Text email;
	public TMP_Text password;

	//public GameManager gameManager;

	private void Start()
	{
		//Runs in the first scene of the game
		FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
		{
			if (task.Exception != null)
			{
				Debug.LogError(task.Exception);
			}
		});

		//Disable button untill we have logged in
		playButton.interactable = false;

		//make sure we load the logged in user information when we click the button
		playButton.onClick.AddListener(() => SceneManager.LoadScene(1));
		
	}

	//Connect the login button to this function
	public void Login(int user = 0)
	{
		//if we send a number to the function, log in as a test user
		if (user != 0)
		{
			StartCoroutine(SignIn("test" + user + "@test.test", "password"));
			return;
		}

		//log in using the set credential
		StartCoroutine(SignIn(email.text, password.text));
	}

	private IEnumerator SignIn(string email, string password)
	{
		Debug.Log("Attempting to log in");
		outputText.text = "Attempting to log in";
		var auth = FirebaseAuth.DefaultInstance;
		var loginTask = auth.SignInWithEmailAndPasswordAsync(email, password);

		//TODO: Show loading animation

		yield return new WaitUntil(() => loginTask.IsCompleted);

		//TODO: remove loading animation

		if (loginTask.Exception != null)
		{
			//Show any errors
			Debug.LogWarning(loginTask.Exception);
			outputText.text = loginTask.Exception.InnerExceptions[0].InnerException.Message;
		}
		else
		{
			Debug.Log("login completed");
		}

		//Show the email of our logged in user
		outputText.text = "Logged in as: " + loginTask.Result.Email;

		//Activate the play button once we have logged in
		playButton.interactable = true;
	}

	public void Register()
	{
		//log in using the set credential
		StartCoroutine(RegUser(email.text, password.text));
	}

	private IEnumerator RegUser(string email, string password)
	{
		Debug.Log("Starting Registration");
		outputText.text = "Starting Registration";
         var auth = FirebaseAuth.DefaultInstance;
         var regTask = auth.CreateUserWithEmailAndPasswordAsync(email, password);
         yield return new WaitUntil(() => regTask.IsCompleted);

         if (regTask.Exception != null)
             Debug.LogWarning(regTask.Exception);
         Debug.Log("Registration completed");
         outputText.text = "Registration completed";
	}
}
