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
        else{
            uIManager.ShowText(dialogueNodes[node].Name, dialogueNodes[node].ShowDialogue);
            lastNode = dialogueNodes[node].LastNode-1;
            node = dialogueNodes[node].NextNode - 1;
        } 
    }
    public void LastNode(){
        //GET info from nodes and put it on screen
        Debug.Log(lastNode);
        if(lastNode >= 0){
            uIManager.ShowText(dialogueNodes[lastNode].Name, dialogueNodes[lastNode].ShowDialogue);
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
}