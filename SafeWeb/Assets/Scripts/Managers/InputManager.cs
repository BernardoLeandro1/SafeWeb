using UnityEngine;

public class InputManager : MonoBehaviour
{
    private LogicManager logicManager;

    private NodeManager nodeManager;

    private UIManager uIManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        logicManager = GetComponent<LogicManager>();
        nodeManager = GetComponent<NodeManager>();
        uIManager = GetComponent<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E)){
            Debug.Log(logicManager.GetMode());
            if (logicManager.GetMode() == "free")
            {
                logicManager.Interaction();
            }
        }
        else if(Input.GetKeyDown(KeyCode.Escape)){
            if(logicManager.GetMode()=="lock"){
                logicManager.ChangeMode(stringObj:"chair");
            }
        }
    }

    public void Advance(){
        Debug.Log("lineFinish: " + uIManager.lineFinish);
        if(uIManager.lineFinish){
            nodeManager.NextNode();
        }
        else {
            uIManager.FinishLine();
        }
        
    }

    public void Backtrack(){
        Debug.Log("lineFinish: " + uIManager.lineFinish);
        if(uIManager.lineFinish){
            nodeManager.LastNode();
        }
        else {
            uIManager.FinishLine();
        }
    }
}
