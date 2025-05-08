using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public class MissionManager : MonoBehaviour
{

    List<Mission> missions;

    Mission currentMission = null;

    UIManager uIManager;
    LogicManager logicManager;

    MissionManager missionManager;
    List<DialogueNode> dialogueNodes;

    
    int node = 0;
    int lastNode = 0;

    void Awake()
    {
        OpenMissionsFile();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        uIManager = GetComponent<UIManager>();
        logicManager = GetComponent<LogicManager>();
        missionManager = GetComponent<MissionManager>();
    }

    public void OpenMissionsFile(){
        StreamReader reader = new StreamReader(Path.Combine(Application.streamingAssetsPath, "missions.json"));
        var json = reader.ReadToEnd();
        missions = JsonConvert.DeserializeObject<List<Mission>>(json);
    }

    public void OpenCurrentMissionsFile(){
        if(currentMission==missions[0]){
            StreamReader reader = new StreamReader(Path.Combine(Application.streamingAssetsPath, "mission1.json"));
            var json = reader.ReadToEnd();
            dialogueNodes = JsonConvert.DeserializeObject<List<DialogueNode>>(json);
        }
        else if(currentMission==missions[1]){
            StreamReader reader = new StreamReader(Path.Combine(Application.streamingAssetsPath, "mission2.json"));
            var json = reader.ReadToEnd();
            dialogueNodes = JsonConvert.DeserializeObject<List<DialogueNode>>(json);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public List<Mission> GetMissions(){
        return missions;
    }

    public void SelectMission(int missionId){
        currentMission = missions[missionId];
        OpenCurrentMissionsFile();
    }

    public bool IsMissionActive(){
        return currentMission != null;
    }


    public void NextNodeMissions(){
        //GET info from nodes and put it on screen  
        if(dialogueNodes[node].ShowChoicePanel!=null){
            uIManager.DisplayChoicesPanel(dialogueNodes[node].ShowChoicePanel.ToArray());
        }
        else if(dialogueNodes[node].ShowDialogue.Contains("end of mission")){
            lastNode = 0;
            node = 0;
            logicManager.MissionComplete();
            uIManager.DisplayToDoList("Volta ao teu lugar");
            logicManager.ChangeMode();
            currentMission = null;
        }
        else if(dialogueNodes[node].ShowDialogue.Contains("free mode")){
            lastNode = dialogueNodes[node].LastNode-1;
            node = dialogueNodes[node].NextNode - 1;
            // mudar isto para passar apenas o que está a seguir a free mode ou algo do género
            //uIManager.DisplayToDoList("Volta ao teu lugar");
            logicManager.ChangeMode();
        }
        else{
            uIManager.ShowText(dialogueNodes[node].Name, dialogueNodes[node].ShowDialogue);
            lastNode = dialogueNodes[node].LastNode-1;
            node = dialogueNodes[node].NextNode - 1;
        } 
    }
    

    public void HasChoosen(int index){
        node = index-1;
        Debug.Log("node: " + node);
        lastNode = dialogueNodes[node].LastNode-1;
        uIManager.ToggleChoiceCanvas(false);
        NextNodeMissions();
    }

    public void LastNodeMissions(){
        //GET info from nodes and put it on screen
        if(dialogueNodes[lastNode].ShowChoicePanel!=null){
            uIManager.ShowTextAfterBackwords(dialogueNodes[dialogueNodes[lastNode].LastNode-1].Name, dialogueNodes[dialogueNodes[lastNode].LastNode-1].ShowDialogue);
            uIManager.DisplayChoicesPanel(dialogueNodes[lastNode].ShowChoicePanel.ToArray());
        }
        else if(lastNode >= 0){
            uIManager.ShowTextAfterBackwords(dialogueNodes[lastNode].Name, dialogueNodes[lastNode].ShowDialogue);
            node = dialogueNodes[lastNode].NextNode - 1;
            lastNode = dialogueNodes[lastNode].LastNode-1;
        }
    }
}
public class Mission
{ 
    public string Name { get; private set; }

    public string Description { get; private set; }

    public bool available { get; set; } = false;

    public Mission(string name, string description){
        this.Name = name;
        this.Description = description;
    }
    

}