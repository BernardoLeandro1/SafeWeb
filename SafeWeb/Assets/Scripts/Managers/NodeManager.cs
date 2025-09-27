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
    List<DialogueNode> dialogueNodes;

    
    int node = 57;
    int lastNode = 0;
    //int conta = 0;
    bool firstTime = false;
    public bool goShopping = false;

    public bool phoneUnlocked = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

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
        //NextNode();
    }

    // Update is called once per frame
    void Update()
    {
        
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
                uIManager.ShowText("Desenvolvedor de Jogo", "É tudo, por agora!");
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
                }if (dialogueNodes[node].Trigger != null)
                {
                    if (dialogueNodes[node].Trigger.Contains("hideCharsJardim"))
                    {
                        charactersManager.day4CharsJardim.SetActive(false);
                        charactersManager.ShowCharacters();
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
                }
                uIManager.ShowText(dialogueNodes[node].Name, dialogueNodes[node].ShowDialogue);
                lastNode = dialogueNodes[node].LastNode - 1;
                node = dialogueNodes[node].NextNode - 1;
                // if (dialogueNodes[node].CheckCond != null)
                // {
                //     if (dialogueNodes[node].CheckCond.Contains("conta"))
                //     {
                //         if (conta == 1)
                //         {
                //             node = 4;
                //             lastNode = 3;
                //         }
                //         else if (conta == 0)
                //         {
                //             node = 10;
                //             lastNode = 3;
                //         }
                //     }
                //     else if (dialogueNodes[node].CheckCond.Contains("conta1"))
                //     {
                //         conta = 1;
                //     }
                //     if (dialogueNodes[node].CheckCond.Contains("conta0"))
                //     {
                //         conta = 0;
                //     }
                // }
                
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
            else if (dialogueNodes[lastNode].ShowDialogue.Contains("free mode"))
            {
                if (dialogueNodes[lastNode].AvailableMissions != null)
                {
                    string toDoList = "";
                    foreach (var number in dialogueNodes[lastNode].AvailableMissions)
                    {
                        missionManager.GetMissions()[number].available = true;
                        toDoList += missionManager.GetMissions()[number].Description + "\n";
                        //Debug.Log(missions[number].Description);
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
                logicManager.ChangeMode();
                lastNode = dialogueNodes[lastNode].LastNode - 1;
                node = dialogueNodes[lastNode].NextNode - 1;
            }
            else if (lastNode >= 0)
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

