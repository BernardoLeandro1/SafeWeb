using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class GameSaveManager : MonoBehaviour
{
    public static GameSaveManager Instance { get; private set; }

    public Button continueButton;

    private static string SavePath => Application.persistentDataPath + "/save.json";

    public Dictionary<string, int> scoreSystem = new Dictionary<string, int>();
    public int day = 1;
    public int node = 0;

    
    private void Awake()
    {
        // Singleton pattern: only one instance allowed
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    private void Update()
    {
        // Enable or disable Continue button based on save file
        if (continueButton != null)
        {
            continueButton.interactable = SaveFileExists();
        }
        else
        {
            continueButton = GameObject.Find("Continuar")?.GetComponent<Button>();
        }
        //continueButton.interactable = SaveFileExists();
    }

    public static bool SaveFileExists()
    {
        if (File.Exists(SavePath))
        {
            string json = File.ReadAllText(SavePath);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            if (data.day <= 5)
            {
                return true;
            }
        }
        return false;
    }

    // --- Public Methods ---

    public void NewGame()
    {
        InitializeDefaults();
        day = 1;
        node = 0;
        SceneManager.LoadScene(1); // load your Game scene
    }

    public void LoadExistingGame()
    {
        if (SaveFileExists())
        {
            LoadGame();
            SceneManager.LoadScene(1);
        }
        else
        {
            Debug.Log("No save file found. Starting new game.");
            NewGame();
        }
    }

    public void SaveGame(Dictionary<string,int> scoreSystemToSave, int dayToSave, int nodeToSave)
    {
        SaveData data = new SaveData();
        data.FromDictionary(scoreSystemToSave);
        data.day = dayToSave;
        data.node = nodeToSave;

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(SavePath, json);
        Debug.Log($"Game saved to {SavePath}");
    }

    public void LoadGame()
    {
        if (File.Exists(SavePath))
        {
            string json = File.ReadAllText(SavePath);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            scoreSystem = data.ToDictionary();
            day = data.day;
            node = data.node;
            Debug.Log("Game loaded successfully.");
        }
        else
        {
            Debug.Log("No save file found.");
        }
    }

    private void InitializeDefaults()
    {
        scoreSystem.Clear();
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
}
[System.Serializable]
public class SaveData
{
    public List<string> keys = new List<string>();
    public List<int> values = new List<int>();
    public int day;
    public int node;

    public void FromDictionary(Dictionary<string, int> dict)
    {
        keys.Clear();
        values.Clear();
        foreach (var pair in dict)
        {
            keys.Add(pair.Key);
            values.Add(pair.Value);
        }
    }

    public Dictionary<string, int> ToDictionary()
    {
        Dictionary<string, int> dict = new Dictionary<string, int>();
        for (int i = 0; i < keys.Count; i++)
            dict[keys[i]] = values[i];
        return dict;
    }
}