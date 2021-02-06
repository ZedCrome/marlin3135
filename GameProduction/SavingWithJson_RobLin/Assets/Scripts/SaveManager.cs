using System;
using System.IO;
using System.Net;
using System.Text;
using TMPro;
using UnityEngine;

[Serializable]
public class MultiplePlayers
{
    public PlayerInfo[] players;
}


[Serializable]
public class PlayerInfo
{
    public string name;
    public Vector3 position;
}


public class SaveManager : MonoBehaviour
{
    public GameObject player1;
    public GameObject player2;

    void Start()
    {
        //LoadData();
    }

    
    public void SaveData()
    {
        Debug.Log("Saving...");
        //Get player info
        var players = FindObjectsOfType<PlayerMovement>();
        
        var multiplePlayers = new MultiplePlayers();
        multiplePlayers.players = new PlayerInfo[players.Length];
        
        //Put info  in PlayerInfo class
        for (int i = 0; i < players.Length; i++)
        {
            multiplePlayers.players[i] = new PlayerInfo();
            multiplePlayers.players[i].position = players[i].transform.position;
            multiplePlayers.players[i].name = players[i].name;
        }
        
        //Turn class into Json
        string jsonString = JsonUtility.ToJson(multiplePlayers);
        
        
        //save info.
        //PlayerPrefs.SetString("json", jsonString);
        SaveToFile("CarGameSaveFile", jsonString);
        SaveOnline("CarGameSaveFile", jsonString);

    }

    public void LoadData()
    {
        Debug.Log("Loading...");

        //load from player prefs
        //string jsonString = PlayerPrefs.GetString("json");
        
        //load from file
        string jsonString2 = Load("CarGameSaveFile");
        
        //load from server
        string jsonString = LoadOnline("CarGameSaveFile");

        if (jsonString != jsonString2)
        {
            Debug.LogError("Your local save does not match the online verision.");
        }
        
        var multiplePlayers = JsonUtility.FromJson<MultiplePlayers>(jsonString);

        var players = FindObjectsOfType<PlayerMovement>();
        for (int i = 0; i < players.Length; i++)
        {
            players[i].name = multiplePlayers.players[i].name;
            players[i].transform.position = multiplePlayers.players[i].position; 
        }

        var nameTagManager = GetComponent<NameTagManager>();

        for (int i = 0; i < nameTagManager.nameTags.Length; i++)
        {
            nameTagManager.nameTags[i].GetComponent<TMP_Text>().text = multiplePlayers.players[i].name;
        }
    }

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
    
    public string Load(string fileName)
    {
        // Open a stream for the supplied file name as a text file
        using (var stream = File.OpenText(Application.persistentDataPath + "\\" +fileName))
        {
            // Read the entire file and return the result. This assumes that we've written the
            // file in UTF-8
            return stream.ReadToEnd();
        }
    }
    
    
    
    
    


        public string LoadOnline(string name)
        {
            var request = (HttpWebRequest) WebRequest.Create("http://localhost:8080/" + name);
            var response = (HttpWebResponse)request.GetResponse ();

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

        //Saves the playerInfo string on the server.
        public void SaveOnline(string fileName, string saveData)
        {
            //url
            var request = (HttpWebRequest)WebRequest.Create("http://localhost:8080/" + fileName);
            request.ContentType = "application/json";
            request.Method = "PUT";

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(saveData);
                streamWriter.Close();
            }

            var httpResponse = (HttpWebResponse)request.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                Debug.Log(result);
            }
        }
    }

