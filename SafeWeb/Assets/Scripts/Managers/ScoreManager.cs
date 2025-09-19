using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{

    private Dictionary<string, int> scoreSystem = new Dictionary<string, int>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        scoreSystem.Add("maria", 25);
        scoreSystem.Add("ana", 25);
        scoreSystem.Add("antonio", 25);
        scoreSystem.Add("joao", 25);
        scoreSystem.Add("pai", 25);
        scoreSystem.Add("conta", 0);
        scoreSystem.Add("clara", 0);
        scoreSystem.Add("link", 0);
        scoreSystem.Add("post", 0);
        scoreSystem.Add("password", 0);
        scoreSystem.Add("wifi", 0);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddScore(string name, int score)
    {
        if (name.Contains("_"))
        {
            foreach (var item in name.Split('_'))
            {
                AddScore(item, score);
            }
        }
        else if (name.Contains("todos"))
        {
            AddScore("maria", score);
            AddScore("ana", score);
            AddScore("antonio", score);
            AddScore("joao", score);
            AddScore("pai", score);
        }
        else if (scoreSystem.ContainsKey(name))
        {
            scoreSystem[name] += score;
        }
        foreach (var item in scoreSystem)
        {
            Debug.Log(item.Key + ": " + item.Value);
        }
    }

    public Dictionary<string, int> GetScoreSystem()
    {
        return scoreSystem;
    }

    public int GetScore(string reference)
    {
        if (scoreSystem.ContainsKey(reference))
        {
            return scoreSystem[reference];
        }
        return 0;
    }
}
