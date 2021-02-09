using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Firebase;
using Firebase.Extensions;
using Firebase.Database;
using System.Collections;
using Firebase.Auth;

public class Register : MonoBehaviour
{
    public GameObject loginCanvas;
    public GameObject mainMenu;
    public TMP_InputField regEmail;
    public TMP_InputField regPass;
    void Start()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            if (task.Exception != null)
            {
                Debug.LogError(task.Exception);
            }

            //StartCoroutine(RegUser("test@test.test", "password"));
            //StartCoroutine(SignIn("test@test.test", "password1"));
        });
    }

    public void RegisterUser()
    {
        StartCoroutine(RegUser(regEmail.text, regPass.text));
    }

    public void GoToLoginPage()
    {
        gameObject.SetActive(false);
        loginCanvas.SetActive(true);
    }
    

    private IEnumerator RegUser(string email, string password)
    {
        Debug.Log("Starting Registration");
        var auth = FirebaseAuth.DefaultInstance;
        var regTask = auth.CreateUserWithEmailAndPasswordAsync(email, password);
        yield return new WaitUntil(() => regTask.IsCompleted);

        if (regTask.Exception != null)
            Debug.LogWarning(regTask.Exception);
        else
        {
            Debug.Log("Registration Complete");
            loginCanvas.SetActive(true);
            FindObjectOfType<Login>().UserSignIn(email, password);
            loginCanvas.SetActive(false);
            FindObjectOfType<SaveManager>().SaveNewUser();
            gameObject.SetActive(false);
            mainMenu.SetActive(true);
            
        }
    }
}
