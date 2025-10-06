using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private string reference = "";

    private int value = 0;

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
        scoreSystem.Add("jaime", 0);
        scoreSystem.Add("link", 0);
        scoreSystem.Add("post", 0);
        scoreSystem.Add("password", 0);
        scoreSystem.Add("wifi", 0);
        scoreSystem.Add("dicas1", 0);
        scoreSystem.Add("dicas2", 0);
        scoreSystem.Add("dicas3", 0);
        scoreSystem.Add("dicas4", 0);
        scoreSystem.Add("dicas5", 0);
        scoreSystem.Add("dicas6", 0);
        scoreSystem.Add("jaimelink", 0);
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
        foreach (var person in scoreSystem.Keys)
        {
            Debug.Log("score: " + person + " - " + scoreSystem[person]);
        }
        reference = name;
        value = score;
    }

    public void RetractScore()
    {
        if (reference != "" && value != 0)
        {
            if (reference.Contains("_"))
            {
                foreach (var item in reference.Split('_'))
                {
                    if (scoreSystem.ContainsKey(item))
                    {
                        scoreSystem[item] -= value;
                    }
                }
            }
            else if (name.Contains("todos"))
            {
                scoreSystem["maria"] -= value;
                scoreSystem["ana"] -= value;
                scoreSystem["antonio"] -= value;
                scoreSystem["joao"] -= value;
                scoreSystem["pai"] -= value;
            }
            else if (scoreSystem.ContainsKey(reference))
            {
                scoreSystem[reference] -= value;
            }
            foreach (var person in scoreSystem.Keys)
            {
                Debug.Log("score: " + person + " - " + scoreSystem[person]);
            }
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
