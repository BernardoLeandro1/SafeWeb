using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class StartGame : MonoBehaviour
{
    public NodeManager nodeManager;
    public UIManager uIManager;
    public ScoreManager scoreManager;
    public LogicManager logicManager;

    public CharactersManager charactersManager;

    private int help = 0;
   

    public void SaveGame()
    {
        GameSaveManager.Instance.SaveGame(scoreManager.GetScoreSystem(), logicManager.GetDay(), nodeManager.getNode());
    }

    // Optional: Manual save & load (for testing in buttons or editor)
    // [ContextMenu("Save Game")]
    // public void SaveGameContext() => SaveGame();

    // [ContextMenu("Load Game")]
    // public void LoadGameContext() => LoadGame();

    public void GameStart()
    {
        if (help == 0)
        {
            scoreManager.SetScoreSystem(GameSaveManager.Instance.scoreSystem);
            nodeManager.setNode(GameSaveManager.Instance.node);
            logicManager.SetDay(GameSaveManager.Instance.day);
            charactersManager.loadCharacters(GameSaveManager.Instance.day - 1);
            charactersManager.UpdateCharactersPositions(GameSaveManager.Instance.day);
            uIManager.ChangeUI();
            nodeManager.NextNode();
            help = 1;
        }
        else
        {
            uIManager.ChangeUI();
            nodeManager.NextNode();
        }
        
    }

    public void DayStart()
    {
        uIManager.ChangeUI();
        nodeManager.NextNode();
        
    }
}