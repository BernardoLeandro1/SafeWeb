using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public class NodeManager : MonoBehaviour
{
    UIManager uIManager;
    LogicManager logicManager;

    MissionManager missionManager;

    CharactersManager charactersManager;

    ScoreManager scoreManager;
    List<DialogueNode> dialogueNodes;

    
    int node = 0;
    int lastNode = 0;
    //int conta = 0;
    bool firstTime = false;
    public bool goShopping = false;

    public bool phoneUnlocked = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    PhoneManager phoneManager;

    void Awake()
    {
        OpenNodeFile();
    }

    void Start()
    {
        uIManager = GetComponent<UIManager>();
        logicManager = GetComponent<LogicManager>();
        missionManager = GetComponent<MissionManager>();
        charactersManager = GetComponent<CharactersManager>();
        phoneManager = GetComponent<PhoneManager>();
        scoreManager = GetComponent<ScoreManager>();
        //NextNode();
    }

    // Update is called once per frame
    void Update()
    {

    }


    public int getNode()
    {
        return node;
    }
    
    public void setNode(int savedNode)
    {
        node = savedNode;
    }

    public void OpenNodeFile(){
        StreamReader reader = new StreamReader(Path.Combine(Application.streamingAssetsPath, "narrativa.json"));
        var json = reader.ReadToEnd();
        dialogueNodes = JsonConvert.DeserializeObject<List<DialogueNode>>(json);
    }

    

    public void NextNode(){
        //GET info from nodes and put it on screen
        if(!firstTime){
            if (missionManager.IsMissionActive())
            {
                missionManager.NextNodeMissions();
            }
            else if (node < 0)
            {
                scoreManager.FinalAdvice();

                lastNode = dialogueNodes.Count - 1;
                firstTime = true;

            }
            else if (dialogueNodes[node].ShowChoicePanel != null)
            {
                uIManager.DisplayChoicesPanel(dialogueNodes[node].ShowChoicePanel.ToArray());

            }
            else if (dialogueNodes[node].ShowDialogue.Contains("free mode"))
            {
                if (dialogueNodes[node].AvailableMissions != null)
                {
                    string toDoList = "";
                    foreach (var number in dialogueNodes[node].AvailableMissions)
                    {
                        missionManager.GetMissions()[number].available = true;
                        toDoList += missionManager.GetMissions()[number].Description + "\n";
                    }
                    uIManager.DisplayToDoList(toDoList);
                }
                else
                {
                    string toDoList = "";
                    foreach (var mission in missionManager.GetMissions())
                    {
                        if (mission.available == true)
                        {
                            toDoList += mission.Description + "\n";
                        }
                        //Debug.Log(missions[number].Description);
                    }
                    uIManager.DisplayToDoList(toDoList);
                }
                if (dialogueNodes[node].ToDo != null)
                {
                    if (dialogueNodes[node].ToDo.Contains("missions"))
                    {
                        string toDoList = "";
                        foreach (var mission in missionManager.GetMissions())
                        {
                            if (mission.available == true)
                            {
                                toDoList += mission.Description + "\n";
                            }
                            //Debug.Log(missions[number].Description);
                        }
                        uIManager.DisplayToDoList(toDoList);
                    }
                    else
                    {
                        uIManager.DisplayToDoList(dialogueNodes[node].ToDo);
                    }
                }
                if (dialogueNodes[node].Trigger != null)
                {
                    if (dialogueNodes[node].Trigger.Contains("hideCharsJardim"))
                    {
                        charactersManager.day4CharsJardim.SetActive(false);
                        charactersManager.ShowCharacters();
                    }
                    else if (dialogueNodes[node].Trigger.Contains("hideChars"))
                    {
                        charactersManager.HideCharacters2();
                    }
                    else if (dialogueNodes[node].Trigger.Contains("canGoToBed"))
                    {
                        logicManager.canGoToBed = true;
                    }

                    else if (dialogueNodes[node].Trigger.Contains("showdad"))
                    {
                        charactersManager.ShowParents();
                    }
                    if (dialogueNodes[node].Trigger.Contains("posts"))
                    {
                        phoneManager.ShowPosts();
                    }
                    if (dialogueNodes[node].Trigger.Contains("messages"))
                    {
                        phoneManager.ShowMessages();
                    }
                    if (dialogueNodes[node].Trigger.Contains("requests"))
                    {
                        phoneManager.ShowRequests();
                    }
                }
                if (dialogueNodes[node].ShowDialogue.Contains("free mode v2"))
                {
                    logicManager.ChangeMode();
                }
                else
                {
                    logicManager.ChangeMode(stringObj: "chair");
                    
                }
                lastNode = dialogueNodes[node].LastNode - 1;
                node = dialogueNodes[node].NextNode - 1;

            }
            else if (dialogueNodes[node].ShowDialogue.Contains("continua free"))
            {
                if (dialogueNodes[node].ToDo != null)
                {
                    uIManager.DisplayToDoList(dialogueNodes[node].ToDo);
                }

                else if (dialogueNodes[node].Trigger != null) {
                    if (dialogueNodes[node].Trigger.Contains("canGoToBed"))
                    {
                        logicManager.canGoToBed = true;
                    }
                }
                
                
                lastNode = dialogueNodes[node].LastNode - 1;
                node = dialogueNodes[node].NextNode - 1;
            }
            else
            {
                if (dialogueNodes[node].Trigger != null)
                {
                    if (dialogueNodes[node].Trigger.Contains("characters"))
                    {
                        charactersManager.ShowCharacters();
                    }
                    else if (dialogueNodes[node].Trigger.Contains("shopping"))
                    {
                        goShopping = true;
                    }
                    else if (dialogueNodes[node].Trigger.Contains("phone"))
                    {
                        phoneUnlocked = true;
                    }
                    else if (dialogueNodes[node].Trigger.Contains("showdad"))
                    {
                        charactersManager.ShowParents();
                    }
                    if (dialogueNodes[node].Trigger.Contains("hideChars"))
                    {
                        charactersManager.HideCharacters2();
                    }
                    
                }
                uIManager.ShowText(dialogueNodes[node].Name, dialogueNodes[node].ShowDialogue);
                lastNode = dialogueNodes[node].LastNode - 1;
                node = dialogueNodes[node].NextNode - 1;
            } 
        }
    }

    public void HasChoosen(int index){
        if (missionManager.IsMissionActive())
        {   
            missionManager.UpdateNodes(index - 1, dialogueNodes[node].LastNode - 1);
            uIManager.ToggleChoiceCanvas(false);
            missionManager.NextNodeMissions();
        }
        else
        {
            node = index-1;
            Debug.Log("node: " + node);
            lastNode = dialogueNodes[node].LastNode-1;
            uIManager.ToggleChoiceCanvas(false);
            NextNode();
        }
    }

    public void UpdateScores(int score)
    {
        missionManager.UpdatePaths(score);
    }

    public void LastNode()
    {
        //GET info from nodes and put it on screen
        if (!firstTime)
        {
            if (missionManager.IsMissionActive())
            {
                missionManager.LastNodeMissions();
            }
            else if (dialogueNodes[lastNode].ShowChoicePanel != null)
            {
                uIManager.ShowTextAfterBackwords(dialogueNodes[dialogueNodes[lastNode].LastNode - 1].Name, dialogueNodes[dialogueNodes[lastNode].LastNode - 1].ShowDialogue);
                uIManager.DisplayChoicesPanel(dialogueNodes[lastNode].ShowChoicePanel.ToArray());
            }
            
            else if (lastNode >= 0 && !dialogueNodes[lastNode].ShowDialogue.Contains("free"))
            {
                uIManager.ShowTextAfterBackwords(dialogueNodes[lastNode].Name, dialogueNodes[lastNode].ShowDialogue);
                node = dialogueNodes[lastNode].NextNode - 1;
                lastNode = dialogueNodes[lastNode].LastNode - 1;
            }
        }
    }
}


public class DialogueNode
{
    public int Node { get; set; }
    public string Name { get; set; }
    public string ShowDialogue { get; set; }
    public string DisplayCamera { get; set; }
    public string ToDo { get; set; }
    public int NextNode { get; set; }

    public int LastNode { get; set; }

    public string CheckCond { get; set; }

    public List<DialogueChoices> ShowChoicePanel { get; set; }

    public List<int> AvailableMissions { get; set; }

    public string Trigger { get; set; }


    
    

}


public class DialogueChoices
{
    public int NextNode { get; private set; }
    public string ShowDialogue { get; private set; }

    public int Score { get; private set; } = 0;

    public string Reference { get; private set; } = "";


    public DialogueChoices(int nextNode, string showDialogue, int score = 0, string reference = "")
    {
        this.NextNode = nextNode;
        this.ShowDialogue = showDialogue;
        this.Score = score;
        this.Reference = reference;
    }
}

