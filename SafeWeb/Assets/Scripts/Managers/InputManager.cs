using UnityEngine;

public class InputManager : MonoBehaviour
{
    private LogicManager logicManager;

    private NodeManager nodeManager;

    private UIManager uIManager;

    private MissionManager missionManager;

    private PhoneManager phoneManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        logicManager = GetComponent<LogicManager>();
        nodeManager = GetComponent<NodeManager>();
        uIManager = GetComponent<UIManager>();
        missionManager = GetComponent<MissionManager>();
        phoneManager = GetComponent<PhoneManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (logicManager.GetMode() == "free" && missionManager.isWaiting==false)
            {
                logicManager.Interaction();
            }
        }
        // else if (Input.GetKeyDown(KeyCode.Escape))
        // {
        //     if (logicManager.GetMode() == "lock")
        //     {
        //         logicManager.ChangeMode(stringObj: "chair");
        //     }
        // }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            if (uIManager.phone.activeSelf == true && phoneManager.registButton.activeSelf == false)
            {
                uIManager.HidePhone();
                logicManager.DeactivatePhone();
            }
            else if (Cursor.lockState == CursorLockMode.Locked && nodeManager.phoneUnlocked == true)
            {
                uIManager.DisplayPhone();
                logicManager.ActivatePhone();
            }
           
        }
    }

    public void Advance(){
        if(uIManager.lineFinish){
            nodeManager.NextNode();
        }
        else {
            uIManager.FinishLine();
        }
        
    }

    public void Backtrack(){
        if(uIManager.lineFinish){
            nodeManager.LastNode();
        }
        else {
            uIManager.FinishLine();
        }
    }
}
