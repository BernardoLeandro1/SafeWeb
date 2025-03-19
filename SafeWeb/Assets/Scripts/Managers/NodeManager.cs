using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public class NodeManager : MonoBehaviour
{
    UIManager uIManager;
    List<DialogueNode> dialogueNodes;
    int node = 0;
    int lastNode = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Awake()
    {
        OpenNodeFile();
    }

    void Start()
    {
        uIManager = GetComponent<UIManager>();
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
        if(node<0){
            uIManager.ShowText("Desenvolvedor de Jogo", "É tudo, por agora!");
            lastNode = dialogueNodes.Count-1;
            
        }
        else if(dialogueNodes[node].ShowChoicePanel!=null){
            // List<DialogueChoices> choices2 = new ();
            // foreach(var choice in dialogueNodes[node].ShowChoicePanel)
            // {
            //     if(choice.NextNode==0){       
            //         var _choice = new DialogueChoices(choice.NextNode+1, choice.ShowDialogue);
            //         choices2.Add(_choice);
            //     }
            //     else{
            //         var _choice = new DialogueChoices(choice.NextNode, choice.ShowDialogue);
            //         choices2.Add(_choice);
            //     }
                
            // }
            // DialogueChoices[] choices = dialogueNodes[node].ShowChoicePanel.ToArray();
            uIManager.DisplayChoicesPanel(dialogueNodes[node].ShowChoicePanel.ToArray());
            
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
        NextNode();
    }

    public void LastNode(){
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


public class DialogueNode{
    public int Node{get;set;}
    public string Name{get;set;}
    public string ShowDialogue{get;set;}
    public string DisplayCamera{get;set;}
    public int NextNode{get;set;}

    public int LastNode{get;set;}

    public List<DialogueChoices> ShowChoicePanel { get; set; }

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