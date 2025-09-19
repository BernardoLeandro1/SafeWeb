using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using UnityEngine;

public class MissionManager : MonoBehaviour
{

    List<Mission> missions;

    Mission currentMission = null;

    UIManager uIManager;
    LogicManager logicManager;

    PhoneManager phoneManager;

    ScoreManager scoreManager;

    CharactersManager charactersManager;

    NodeManager nodeManager;

    List<DialogueNode> dialogueNodes;


    int node = 0;
    int lastNode = 0;

    public bool isWaiting = false;

    public int solved = 0;


    void Awake()
    {
        OpenMissionsFile();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        uIManager = GetComponent<UIManager>();
        logicManager = GetComponent<LogicManager>();
        phoneManager = GetComponent<PhoneManager>();
        charactersManager = GetComponent<CharactersManager>();
        nodeManager = GetComponent<NodeManager>();
        scoreManager = GetComponent<ScoreManager>();
    }

    public void OpenMissionsFile()
    {
        StreamReader reader = new StreamReader(Path.Combine(Application.streamingAssetsPath, "missions.json"));
        var json = reader.ReadToEnd();
        missions = JsonConvert.DeserializeObject<List<Mission>>(json);
    }

    public void OpenCurrentMissionsFile()
    {
        if (currentMission == missions[0])
        {
            StreamReader reader = new StreamReader(Path.Combine(Application.streamingAssetsPath, "mission1.json"));
            var json = reader.ReadToEnd();
            dialogueNodes = JsonConvert.DeserializeObject<List<DialogueNode>>(json);
        }
        else if (currentMission == missions[1])
        {
            StreamReader reader = new StreamReader(Path.Combine(Application.streamingAssetsPath, "mission2.json"));
            var json = reader.ReadToEnd();
            dialogueNodes = JsonConvert.DeserializeObject<List<DialogueNode>>(json);
        }
        else if (currentMission == missions[2])
        {
            StreamReader reader = new StreamReader(Path.Combine(Application.streamingAssetsPath, "mission3.json"));
            var json = reader.ReadToEnd();
            dialogueNodes = JsonConvert.DeserializeObject<List<DialogueNode>>(json);
        }
        else if (currentMission == missions[3])
        {
            StreamReader reader = new StreamReader(Path.Combine(Application.streamingAssetsPath, "mission4.json"));
            var json = reader.ReadToEnd();
            dialogueNodes = JsonConvert.DeserializeObject<List<DialogueNode>>(json);
        }
        else if (currentMission == missions[4])
        {
            StreamReader reader = new StreamReader(Path.Combine(Application.streamingAssetsPath, "mission5.json"));
            var json = reader.ReadToEnd();
            dialogueNodes = JsonConvert.DeserializeObject<List<DialogueNode>>(json);
        }
        else if (currentMission == missions[5])
        {
            StreamReader reader = new StreamReader(Path.Combine(Application.streamingAssetsPath, "mission6.json"));
            var json = reader.ReadToEnd();
            dialogueNodes = JsonConvert.DeserializeObject<List<DialogueNode>>(json);
        }
        else if (currentMission == missions[6])
        {
            StreamReader reader = new StreamReader(Path.Combine(Application.streamingAssetsPath, "mission7.json"));
            var json = reader.ReadToEnd();
            dialogueNodes = JsonConvert.DeserializeObject<List<DialogueNode>>(json);
        }
        else if (currentMission == missions[7])
        {
            StreamReader reader = new StreamReader(Path.Combine(Application.streamingAssetsPath, "mission8.json"));
            var json = reader.ReadToEnd();
            dialogueNodes = JsonConvert.DeserializeObject<List<DialogueNode>>(json);
        }
        else if (currentMission == missions[8])
        {
            StreamReader reader = new StreamReader(Path.Combine(Application.streamingAssetsPath, "mission9.json"));
            var json = reader.ReadToEnd();
            dialogueNodes = JsonConvert.DeserializeObject<List<DialogueNode>>(json);
        }
        else if (currentMission == missions[9])
        {
            StreamReader reader = new StreamReader(Path.Combine(Application.streamingAssetsPath, "mission10.json"));
            var json = reader.ReadToEnd();
            dialogueNodes = JsonConvert.DeserializeObject<List<DialogueNode>>(json);
        }
        else if (currentMission == missions[10])
        {
            StreamReader reader = new StreamReader(Path.Combine(Application.streamingAssetsPath, "mission11.json"));
            var json = reader.ReadToEnd();
            dialogueNodes = JsonConvert.DeserializeObject<List<DialogueNode>>(json);
        }
        else if (currentMission == missions[11])
        {
            StreamReader reader = new StreamReader(Path.Combine(Application.streamingAssetsPath, "mission12.json"));
            var json = reader.ReadToEnd();
            dialogueNodes = JsonConvert.DeserializeObject<List<DialogueNode>>(json);
        }
        else if (currentMission == missions[12])
        {
            StreamReader reader = new StreamReader(Path.Combine(Application.streamingAssetsPath, "mission13.json"));
            var json = reader.ReadToEnd();
            dialogueNodes = JsonConvert.DeserializeObject<List<DialogueNode>>(json);
        }
        else if (currentMission == missions[13])
        {
            StreamReader reader = new StreamReader(Path.Combine(Application.streamingAssetsPath, "mission14.json"));
            var json = reader.ReadToEnd();
            dialogueNodes = JsonConvert.DeserializeObject<List<DialogueNode>>(json);
        }
        else if (currentMission == missions[14])
        {
            StreamReader reader = new StreamReader(Path.Combine(Application.streamingAssetsPath, "mission15.json"));
            var json = reader.ReadToEnd();
            dialogueNodes = JsonConvert.DeserializeObject<List<DialogueNode>>(json);
        }
        else if (currentMission == missions[15])
        {
            StreamReader reader = new StreamReader(Path.Combine(Application.streamingAssetsPath, "mission16.json"));
            var json = reader.ReadToEnd();
            dialogueNodes = JsonConvert.DeserializeObject<List<DialogueNode>>(json);
        }

    }

    // Update is called once per frame
    void Update()
    {

    }

    public List<Mission> GetMissions()
    {
        return missions;
    }

    
    public Mission GetCurrentMission()
    {
        return currentMission;
    }

    public List<DialogueNode> GetMissionNodes()
    {
        return dialogueNodes;
    }

    public int GetCurrentNode()
    {
        return node;
    }

    public void SelectMission(int missionId)
    {
        currentMission = missions[missionId];
        OpenCurrentMissionsFile();
        MissionOnGoing();
    }

    public bool IsMissionActive()
    {
        return currentMission != null;
    }


    public void NextNodeMissions()
    {
        //GET info from nodes and put it on screen  
        if (dialogueNodes[node].ShowChoicePanel != null)
        {
            uIManager.DisplayChoicesPanel(dialogueNodes[node].ShowChoicePanel.ToArray());
        }
        else if (dialogueNodes[node].ShowDialogue.Contains("end of mission"))
        {
            lastNode = 0;
            node = 0;
            isWaiting = false;
            logicManager.MissionComplete();
            charactersManager.ChangeCubes(1);
            if (currentMission.Id != 4 && currentMission.Id != 9)
            {
                if (logicManager.GetDay() != 3)
                {
                    uIManager.DisplayToDoList("Volta ao teu lugar. (E)");
                }
                
                logicManager.ChangeMode();
                if (currentMission.Id == 0 || currentMission.Id == 1)
                {
                    missions[0].available = false;
                    missions[1].available = false;
                    solved += 2;
                }
                else
                {
                    currentMission.available = false;
                    solved += 1;
                }
                currentMission = null;
                if (logicManager.GetDay() == 3)
                {
                    charactersManager.ChangeCubes(3);
                    nodeManager.NextNode();
                }
            }
            else
            {
                currentMission.available = false;
                currentMission = null;
                nodeManager.NextNode();
                solved += 1;
            }



        }
        else if (dialogueNodes[node].ShowDialogue.Contains("free mode"))
        {
            if (dialogueNodes[node].ToDo != null)
            {
                uIManager.DisplayToDoList(dialogueNodes[node].ToDo.Split(".")[0]);
                if (dialogueNodes[node].ToDo.Contains("Aceita ou recusa"))
                {
                    phoneManager.ShowRequests();
                }
            }
            if (dialogueNodes[node].Trigger != null)
            {
                Debug.Log("AIAI: " + dialogueNodes[node].Trigger);
                if (dialogueNodes[node].Trigger.Contains("posts"))
                {
                    phoneManager.ShowPosts();
                }
                else if (dialogueNodes[node].Trigger.Contains("messages"))
                {
                    phoneManager.ShowMessages();
                }
                else if (dialogueNodes[node].Trigger.Contains("requests"))
                {
                    phoneManager.ShowRequests();
                }
                else if (dialogueNodes[node].Trigger.Contains("phone"))
                {
                    nodeManager.phoneUnlocked = true;
                }
                
            }
            lastNode = dialogueNodes[node].LastNode - 1;
            node = dialogueNodes[node].NextNode - 1;
            // mudar isto para passar apenas o que está a seguir a free mode ou algo do género
            //uIManager.DisplayToDoList("Volta ao teu lugar");
            logicManager.ChangeMode();
            MissionOnGoing();
        }
        else
        {
            uIManager.ShowText(dialogueNodes[node].Name, dialogueNodes[node].ShowDialogue);
            if (dialogueNodes[node].CheckCond != null)
            {
                if (dialogueNodes[node].CheckCond.Contains("conta1"))
                {
                    scoreManager.AddScore("conta", 1);
                    node = dialogueNodes[node].NextNode - 1;
                    lastNode = dialogueNodes[node].LastNode - 1;
                }
                else if (dialogueNodes[node].CheckCond.Contains("conta0"))
                {
                    scoreManager.AddScore("conta", 0);
                    node = dialogueNodes[node].NextNode - 1;
                    lastNode = dialogueNodes[node].LastNode - 1;
                }
                else if (dialogueNodes[node].CheckCond.Contains("conta"))
                {
                    if (scoreManager.GetScore("conta") == 0)
                    {
                        node = 4;
                        lastNode = 3;
                    }
                    else if (scoreManager.GetScore("conta") == 1)
                    {
                        node = 11;
                        lastNode = 3;
                    }
                }
                else if (dialogueNodes[node].CheckCond.Contains("clara"))
                {
                    if (phoneManager.GetFriends().Contains("Clara"))
                    {
                        node = 9;
                        lastNode = 8;
                    }
                    else
                    {
                        node = 15;
                        lastNode = 8;
                    }
                }
                else if (dialogueNodes[node].CheckCond.Contains("link"))
                {
                    if (scoreManager.GetScore("link") == 1)
                    {
                        node = 12;
                        lastNode = 11;
                    }
                    else
                    {
                        node = 13;
                        lastNode = 11;
                    }
                }
                else if (dialogueNodes[node].CheckCond.Contains("post"))
                {
                    if (scoreManager.GetScore("post") == -3)
                    {
                        node = 5;
                        lastNode = 4;
                    }
                    else if (scoreManager.GetScore("post") == -1)
                    {
                        node = 7;
                        lastNode = 4;
                    }
                    else
                    {
                        node = 9;
                        lastNode = 4;
                    }
                }
                else if (dialogueNodes[node].CheckCond.Contains("password"))
                {
                    if (Regex.IsMatch(phoneManager.GetPassword(), @"^[a-z]+$"))
                    {
                        node = 5;
                        lastNode = 4;
                        scoreManager.AddScore("password", 1);
                    }
                    else if (Regex.IsMatch(phoneManager.GetPassword(), @"^[a-zA-Z]+$"))
                    {
                        node = 5;
                        lastNode = 4;
                        scoreManager.AddScore("password", 2);
                    }
                    else if (Regex.IsMatch(phoneManager.GetPassword(), @"^[a-zA-Z0-9]+$"))
                    {
                        node = 6;
                        lastNode = 4;
                        scoreManager.AddScore("password", 3);
                    }
                    else if (Regex.IsMatch(phoneManager.GetPassword(), @"^[a-zA-Z0-9\W_]+$"))
                    {
                        node = 6;
                        lastNode = 4;
                        scoreManager.AddScore("password", 4);
                    }
                    else
                    {
                        Debug.Log("Password Error");
                        node = 9;
                        lastNode = 4;
                    }
                }
                else if (dialogueNodes[node].CheckCond.Contains("wifi"))
                {
                    if (scoreManager.GetScore("wifi") < 0 && scoreManager.GetScore("password") < 3 )
                    {
                        node = 1;
                        lastNode = 0;
                    }
                    else
                    {
                        node = 7;
                        lastNode = 0;
                    }
                }
            }
            else
            {
                node = dialogueNodes[node].NextNode - 1;
                lastNode = dialogueNodes[node].LastNode - 1;
            }
        }
    }

    

    public void UpdatePaths(int score)
    {
        if (currentMission.Id == 8)
        {
            scoreManager.AddScore("post", score);
        }
    }

    public void UpdateNodes(int node1, int lastNode1)
    {
        node = node1;
        lastNode = lastNode1;
    }

    public void LastNodeMissions()
    {
        //GET info from nodes and put it on screen
        if (dialogueNodes[lastNode].ShowChoicePanel != null)
        {
            uIManager.ShowTextAfterBackwords(dialogueNodes[dialogueNodes[lastNode].LastNode - 1].Name, dialogueNodes[dialogueNodes[lastNode].LastNode - 1].ShowDialogue);
            uIManager.DisplayChoicesPanel(dialogueNodes[lastNode].ShowChoicePanel.ToArray());
        }
        else if (lastNode >= 0)
        {
            uIManager.ShowTextAfterBackwords(dialogueNodes[lastNode].Name, dialogueNodes[lastNode].ShowDialogue);

            if (dialogueNodes[node].CheckCond != null)
            {
                if (dialogueNodes[node].CheckCond.Contains("conta1"))
                {
                    node = dialogueNodes[lastNode].NextNode - 1;
                    lastNode = dialogueNodes[lastNode].LastNode - 1;
                }
                else if (dialogueNodes[node].CheckCond.Contains("conta0"))
                {
                    node = dialogueNodes[lastNode].NextNode - 1;
                    lastNode = dialogueNodes[lastNode].LastNode - 1;
                }
                else if (dialogueNodes[node].CheckCond.Contains("conta"))
                {
                    if (scoreManager.GetScore("conta") == 0)
                    {
                        node = 4;
                        lastNode = 3;
                    }
                    else if (scoreManager.GetScore("conta") == 1)
                    {
                        node = 11;
                        lastNode = 3;
                    }
                }
                else if (dialogueNodes[node].CheckCond.Contains("clara"))
                {
                    if (phoneManager.GetFriends().Contains("Clara"))
                    {
                        scoreManager.AddScore("clara", 1);
                        node = 9;
                        lastNode = 8;
                    }
                    else
                    {
                        scoreManager.AddScore("clara", 0);
                        node = 15;
                        lastNode = 8;
                    }
                }
                else if (dialogueNodes[node].CheckCond.Contains("link"))
                {
                    if (scoreManager.GetScore("link") == 1)
                    {
                        node = 12;
                        lastNode = 11;
                    }
                    else
                    {
                        node = 13;
                        lastNode = 11;
                    }
                }
                else if (dialogueNodes[node].CheckCond.Contains("post"))
                {
                    if (scoreManager.GetScore("post") == -3)
                    {
                        node = 5;
                        lastNode = 4;
                    }
                    else if (scoreManager.GetScore("post") == -1)
                    {
                        node = 7;
                        lastNode = 4;
                    }
                    else
                    {
                        node = 9;
                        lastNode = 4;
                    }
                }
                else if (dialogueNodes[node].CheckCond.Contains("password"))
                {
                    if (Regex.IsMatch(phoneManager.GetPassword(), @"^[a-z]+$"))
                    {
                        node = 5;
                        lastNode = 4;
                        scoreManager.AddScore("password", 1);
                    }
                    else if (Regex.IsMatch(phoneManager.GetPassword(), @"^[a-zA-Z]+$"))
                    {
                        node = 5;
                        lastNode = 4;
                        scoreManager.AddScore("password", 2);
                    }
                    else if (Regex.IsMatch(phoneManager.GetPassword(), @"^[a-zA-Z0-9]+$"))
                    {
                        node = 6;
                        lastNode = 4;
                        scoreManager.AddScore("password", 3);
                    }
                    else if (Regex.IsMatch(phoneManager.GetPassword(), @"^[a-zA-Z0-9\W_]+$"))
                    {
                        node = 6;
                        lastNode = 4;
                        scoreManager.AddScore("password", 4);
                    }
                    else
                    {
                        Debug.Log("Password Error");
                        node = 9;
                        lastNode = 4;
                    }
                }
                else if (dialogueNodes[node].CheckCond.Contains("wifi"))
                {
                    if (scoreManager.GetScore("wifi") < 0 && scoreManager.GetScore("password") < 3 )
                    {
                        node = 1;
                        lastNode = 0;
                    }
                    else
                    {
                        node = 7;
                        lastNode = 0;
                    }
                }
            }
            else
            {
                node = dialogueNodes[lastNode].NextNode - 1;
                lastNode = dialogueNodes[lastNode].LastNode - 1;
            }
        }
    }


    public void MissionOnGoing()
    {
        if (currentMission == missions[0] || currentMission == missions[1])
        {
            isWaiting = true;
        }
        else
        {
            isWaiting = false;
        }
    } 

}
public class Mission
{ 
    public int Id { get; private set; }
    public string Name { get; private set; }

    public string Description { get; private set; }

    public bool available { get; set; } = false;

    public Mission(int id ,string name, string description){
        this.Id = id;
        this.Name = name;
        this.Description = description;
    }
    

}