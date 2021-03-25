using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class WordBank : MonoBehaviour
{

    private List<string> originalWords = new List<string>()
    {
        "You", "sunflower", "though", "orange", "virtual-reality", "breakfast", "painting", "music",
        "we're", "function", "unrealengine4", "pokemon", "quite", "don't", "blanket", "swimming", "zebra", "webcam", "doughnut", "parenthasis", "bush", "unity"
    };
    
    public List<string> words = new List<string>();

    private void Awake()
    {
        words.AddRange(originalWords);
        Shuffle(words);
        ToLowerCase(words);
    }


    private void Shuffle(List<string> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int random = Random.Range(i, list.Count);
            string temp = list[i];

            list[i] = list[random];
            list[random] = temp;
        }
    }


    private void ToLowerCase(List<string> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            list[i] = list[i].ToLower();
        }
    }

    public string GetWord()
    {
        string newWord = string.Empty;

        if (words.Count != 0)
        {
            newWord = words.Last();
            words.Remove(newWord);
        }
        
        return newWord;
    }
}
