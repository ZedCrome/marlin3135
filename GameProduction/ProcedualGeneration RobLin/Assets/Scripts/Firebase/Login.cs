// using UnityEngine;
// using TMPro;
// using Firebase;
// using Firebase.Extensions;
// using System.Collections;
// using Firebase.Auth;
//
// public class Login : MonoBehaviour
// {
//     public GameObject mainMenu;
//     public TMP_InputField loginEmail;
//     public TMP_InputField loginPass;
//     private LevelManager levelManager;
//     
//     void Start()
//     {
//         FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
//         {
//             if (task.Exception != null)
//             {
//                 Debug.LogError(task.Exception);
//             }
//         });
//
//         levelManager = LevelManager.instance;
//     }
//     
//     public void UserSignIn()
//     {
//         StartCoroutine(SignIn(loginEmail.text, loginPass.text));
//     }
//     
//     public void UserSignIn(string email, string password)
//     {
//         StartCoroutine(SignIn(email, password));
//     }
//     
//     private IEnumerator SignIn(string email, string password)
//     {
//         Debug.Log("Atempting to log in");
//         var auth = FirebaseAuth.DefaultInstance;
//         var loginTask = auth.SignInWithEmailAndPasswordAsync(email, password);
//
//         //Show loading animation
//
//         yield return new WaitUntil(() => loginTask.IsCompleted);
//
//         //remove loading animation
//
//         if (loginTask.Exception != null)
//             Debug.LogWarning(loginTask.Exception);
//         else
//         {
//             Debug.Log("login completed");
//             gameObject.SetActive(false);
//             FindObjectOfType<SaveManager>().LoadNamesFromFirebase();
//             mainMenu.SetActive(true);
//         }
//     }
//     
//     
// }
