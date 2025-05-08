using UnityEngine;
using Unity.Cinemachine;

public class LogicManager : MonoBehaviour
{
    private string mode;

    public CinemachineCamera cam;

    public GameObject player;

    public Transform freePlayer;
    private CinemachineCamera activeCam;

    private NodeManager nodeManager;
    public float interactRange;

    private UIManager uIManager;

    private MissionManager missionManager;

    private GameObject currentMissionObject;

    private bool hasDoneMission = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        activeCam = cam;
        mode = "lock";
        uIManager = GetComponent<UIManager>();
        nodeManager = GetComponent<NodeManager>();
        missionManager = GetComponent<MissionManager>();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void ChangeMode(GameObject interactObj = null, string stringObj = null){
        if(GetMode() == "free"){
            if(interactObj.gameObject.transform.childCount > 0){
                if(interactObj.gameObject.transform.GetChild(0).GetComponent<CinemachineCamera>()!=null){
                    if(interactObj.gameObject.GetComponent<MissionIDs>()!=null){
                        if(currentMissionObject == null || interactObj == currentMissionObject){
                            mode = "lock";
                            activeCam = interactObj.gameObject.transform.GetChild(0).GetComponent<CinemachineCamera>();
                            activeCam.gameObject.SetActive(true);
                            cam.gameObject.SetActive(false);
                            uIManager.ChangeUI();
                            uIManager.HideToDoList();
                            Cursor.lockState = CursorLockMode.None;
                            Cursor.visible = true;
                            missionManager.SelectMission(interactObj.gameObject.GetComponent<MissionIDs>().GetMissionID());
                            currentMissionObject = interactObj;
                            missionManager.NextNodeMissions();
                            player.GetComponent<Rigidbody>().isKinematic = true;
                        }
                        
                    }
                    else if (hasDoneMission==true){
                        mode = "lock";
                        activeCam = interactObj.gameObject.transform.GetChild(0).GetComponent<CinemachineCamera>();
                        activeCam.gameObject.SetActive(true);
                        cam.gameObject.SetActive(false);
                        uIManager.ChangeUI();
                        uIManager.HideToDoList();
                        Cursor.lockState = CursorLockMode.None;
                        Cursor.visible = true;
                        nodeManager.NextNode();
                        hasDoneMission = false;
                        player.GetComponent<Rigidbody>().isKinematic = true;
                    }
                    
                }
            }
        }
        else{
            mode = "free";
            if(stringObj != null){
                player.transform.position = freePlayer.position;
                cam.GetComponent<CinemachinePanTilt>().PanAxis.Value=0f;
                cam.GetComponent<CinemachinePanTilt>().TiltAxis.Value=0f;
            }
            activeCam.gameObject.SetActive(false);
            cam.gameObject.SetActive(true);
            activeCam = cam; 
            uIManager.ChangeUI();
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            player.GetComponent<Rigidbody>().isKinematic = false;
            
        }
    }

    public string GetMode(){
        return mode;
    }

    public void MissionComplete() {
        currentMissionObject.gameObject.GetComponent<MissionIDs>().MissionSolved();
        currentMissionObject = null;
        hasDoneMission = true;
    }

    public void Interaction(){
        if(GetMode()=="free"){
            // Interaction ray
            Ray r = new Ray(activeCam.transform.position, activeCam.transform.forward);
            // If the ray catches anything in the range, it will try to interact with it
            if (Physics.Raycast(r, out RaycastHit hitInfo, interactRange)){
                if (hitInfo.collider.gameObject!=null){
                    Debug.Log(hitInfo.collider.gameObject.name );
                    ChangeMode(hitInfo.collider.gameObject);
                }            
            }
        }
        
    }
}
