using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public class NodeManager : MonoBehaviour
{
    UIManager uIManager;
    LogicManager logicManager;

    MissionManager missionManager;
    List<DialogueNode> dialogueNodes;

    
    int node = 0;
    int lastNode = 0;

    bool firstTime = false;
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
            if(missionManager.IsMissionActive()){
                missionManager.NextNodeMissions();
            }
            else if(node<0){
                uIManager.ShowText("Desenvolvedor de Jogo", "É tudo, por agora!");
                lastNode = dialogueNodes.Count-1;
                firstTime=true;
                
            }
            else if(dialogueNodes[node].ShowChoicePanel!=null){
                uIManager.DisplayChoicesPanel(dialogueNodes[node].ShowChoicePanel.ToArray());
                
            }
            else if(dialogueNodes[node].ShowDialogue.Contains("free mode")){
                if(dialogueNodes[node].AvailableMissions!=null){
                    string toDoList = "";
                    foreach (var number in dialogueNodes[node].AvailableMissions){
                        missionManager.GetMissions()[number].available = true;
                        toDoList += missionManager.GetMissions()[number].Description + "\n";
                    }
                    uIManager.DisplayToDoList(toDoList);
                }
                logicManager.ChangeMode(stringObj: "chair");
                lastNode = dialogueNodes[node].LastNode-1;
                node = dialogueNodes[node].NextNode - 1;
                
            }
            else{
                uIManager.ShowText(dialogueNodes[node].Name, dialogueNodes[node].ShowDialogue);
                lastNode = dialogueNodes[node].LastNode-1;
                node = dialogueNodes[node].NextNode - 1;
            } 
        }
    }

    public void HasChoosen(int index){
        node = index-1;
        Debug.Log("node: " + node);
        lastNode = dialogueNodes[node].LastNode-1;
        uIManager.ToggleChoiceCanvas(false);
        NextNode();
    }

    public void LastNode(){
        //GET info from nodes and put it on screen
        if(!firstTime){
            if(missionManager.IsMissionActive()){
                missionManager.LastNodeMissions();
            }
            else if(dialogueNodes[lastNode].ShowChoicePanel!=null){
                uIManager.ShowTextAfterBackwords(dialogueNodes[dialogueNodes[lastNode].LastNode-1].Name, dialogueNodes[dialogueNodes[lastNode].LastNode-1].ShowDialogue);
                uIManager.DisplayChoicesPanel(dialogueNodes[lastNode].ShowChoicePanel.ToArray());
            }
            else if(dialogueNodes[lastNode].ShowDialogue.Contains("free mode")){
                if(dialogueNodes[lastNode].AvailableMissions!=null){
                    string toDoList = "";
                    foreach (var number in dialogueNodes[lastNode].AvailableMissions){
                        missionManager.GetMissions()[number].available = true;
                        toDoList += missionManager.GetMissions()[number].Description + "\n";
                        //Debug.Log(missions[number].Description);
                    }
                    uIManager.DisplayToDoList(toDoList);
                }
                logicManager.ChangeMode();
                lastNode = dialogueNodes[lastNode].LastNode-1;
                node = dialogueNodes[lastNode].NextNode - 1;
            }
            else if(lastNode >= 0){
                uIManager.ShowTextAfterBackwords(dialogueNodes[lastNode].Name, dialogueNodes[lastNode].ShowDialogue);
                node = dialogueNodes[lastNode].NextNode - 1;
                lastNode = dialogueNodes[lastNode].LastNode-1;
            }
        }
    }
}


public class DialogueNode{
    public int Node{get;set;}
    public string Name{get;set;}
    public string ShowDialogue{get;set;}
    public string DisplayCamera{get;set;}
    public int NextNode{get;set;}

    public int LastNode{get;set;}

    public List<DialogueChoices> ShowChoicePanel { get; set; }

    public List<int> AvailableMissions { get; set; }

}


public class DialogueChoices
{
    public int NextNode { get; private set; }
    public string ShowDialogue { get; private set; }

    public DialogueChoices(int nextNode, string showDialogue)
    {
        this.NextNode = nextNode;
        this.ShowDialogue = showDialogue;
    }
}

