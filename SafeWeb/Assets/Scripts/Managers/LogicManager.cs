using UnityEngine;
using Unity.Cinemachine;

public class LogicManager : MonoBehaviour
{
    private string mode;

    public CinemachineCamera cam;

    public GameObject player;

    public Transform freePlayer;


    public Transform casaPlayer;

    public Transform aulaPlayer;
    public Transform quartoPlayer;


    public Transform salaPlayer;
    private CinemachineCamera activeCam;

    private NodeManager nodeManager;
    public float interactRange;

    private UIManager uIManager;

    private MissionManager missionManager;

    private CharactersManager charactersManager;

    private GameObject currentMissionObject;

    private bool hasDoneMission = true;

    private bool canGoToBed = false;

    private bool teleport = false;

    private string teleportFrom = "";

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        activeCam = cam;
        mode = "lock";
        uIManager = GetComponent<UIManager>();
        nodeManager = GetComponent<NodeManager>();
        missionManager = GetComponent<MissionManager>();
        charactersManager = GetComponent<CharactersManager>();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        cam.GetComponent<CinemachineInputAxisController>().enabled = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (teleport)
        {
            if (missionManager.solved == 4 && teleportFrom.Contains("escola"))
            {
                player.transform.position = casaPlayer.position;
                nodeManager.NextNode();
            }
            else if (missionManager.solved == 0 && teleportFrom.Contains(value: "casa"))
            {
                player.transform.position = aulaPlayer.position;
                nodeManager.NextNode();
            }
            else if (missionManager.solved == 0 && teleportFrom.Contains("bedroom"))
            {
                player.transform.position = salaPlayer.position;
                nodeManager.NextNode();
            }
            else if (missionManager.solved == 5 && teleportFrom.Contains("bedroom"))
            {
                canGoToBed = true;
                player.transform.position = quartoPlayer.position;
                nodeManager.NextNode();
            }
            teleport = false;
        }
        
    }


    public void ChangeMode(GameObject interactObj = null, string stringObj = null)
    {
        if (GetMode() == "free")
        {
            if (interactObj.gameObject.transform.childCount > 0)
            {
                if (interactObj.gameObject.transform.GetChild(0).GetComponent<CinemachineCamera>() != null)
                {
                    if (interactObj.gameObject.GetComponent<MissionIDs>() != null)
                    {
                        if ((currentMissionObject == null || interactObj == currentMissionObject) && hasDoneMission == false)
                        {
                            mode = "lock";
                            activeCam = interactObj.gameObject.transform.GetChild(0).GetComponent<CinemachineCamera>();
                            activeCam.gameObject.SetActive(true);
                            cam.gameObject.SetActive(false);
                            uIManager.ChangeUI();
                            uIManager.HideToDoList();
                            Cursor.lockState = CursorLockMode.None;
                            Cursor.visible = true;
                            missionManager.SelectMission(interactObj.gameObject.GetComponent<MissionIDs>().GetMissionID());
                            if (currentMissionObject == null && !interactObj.name.Contains("door"))
                            {
                                charactersManager.Wave(interactObj.transform.parent.gameObject);
                            }

                            charactersManager.ChangeCubes(0);
                            currentMissionObject = interactObj;
                            missionManager.NextNodeMissions();
                            player.GetComponent<Rigidbody>().isKinematic = true;

                        }

                    }
                    else if (interactObj.gameObject.name.Contains("Bed") )
                    {
                        if (canGoToBed)
                        {
                            mode = "lock";
                            activeCam = interactObj.gameObject.transform.GetChild(0).GetComponent<CinemachineCamera>();
                            activeCam.gameObject.SetActive(true);
                            cam.gameObject.SetActive(false);
                            //uIManager.ChangeUI();
                            uIManager.HideToDoList();
                            Cursor.lockState = CursorLockMode.None;
                            Cursor.visible = true;
                            //nodeManager.NextNode();
                            canGoToBed = false;
                            uIManager.fadeInStart.GetComponent<Animator>().SetTrigger("End");
                        }
                        
                    }
                    else if (hasDoneMission == true)
                    {
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
                        charactersManager.unlockCharacters = true;
                        charactersManager.UpdateCharacters();
                        charactersManager.HideCharacters();
                        charactersManager.ChangeCubes(2);
                        freePlayer.position = player.transform.position;
                        currentMissionObject = null;
                    }

                }
                else
                {
                    Debug.Log(missionManager.solved);
                    //interactObj.GetComponent<Animator>().SetTrigger("Open");
                    if (interactObj.name.Contains("escola") && missionManager.solved == 4)
                    {
                        // player.transform.position = casaPlayer.position;
                        // nodeManager.NextNode();
                        teleport = true;
                        teleportFrom = "escola";
                    }
                    else if (interactObj.name.Contains("casa") && missionManager.solved == 0)
                    {
                        // player.transform.position = aulaPlayer.position;
                        // nodeManager.NextNode();
                        teleport = true;
                        teleportFrom = "casa";
                    }
                    else if (interactObj.name.Contains("bedroom") && missionManager.solved == 0)
                    {
                        // player.transform.position = salaPlayer.position;
                        // nodeManager.NextNode();
                        teleport = true;
                        teleportFrom = "bedroom";
                    }
                    else if (interactObj.name.Contains("bedroom") && missionManager.solved == 5)
                    {
                        // canGoToBed = true;
                        // player.transform.position = quartoPlayer.position;
                        // nodeManager.NextNode();
                        teleport = true;
                        teleportFrom = "bedroom";
                    }
                    
                }
            }
        }
        else
        {
            mode = "free";
            if (stringObj != null)
            {
                player.transform.position = freePlayer.position;
                cam.GetComponent<CinemachinePanTilt>().PanAxis.Value = 180f;
                cam.GetComponent<CinemachinePanTilt>().TiltAxis.Value = 0f;
            }
            cam.GetComponent<CinemachineInputAxisController>().enabled = true;
            activeCam.gameObject.SetActive(false);
            cam.gameObject.SetActive(true);
            activeCam = cam;
            uIManager.ChangeUI();
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            player.GetComponent<Rigidbody>().isKinematic = false;
            charactersManager.DisplayCharacters();
        }
    }

    public string GetMode()
    {
        return mode;
    }

    public void MissionComplete()
    {

        currentMissionObject.gameObject.GetComponent<MissionIDs>().MissionSolved();
        //currentMissionObject.transform.parent.gameObject.SetActive(false);
        hasDoneMission = true;
    }

    public void Interaction()
    {
        if (GetMode() == "free")
        {
            // Interaction ray
            Ray r = new Ray(activeCam.transform.position, activeCam.transform.forward);
            // If the ray catches anything in the range, it will try to interact with it
            if (Physics.Raycast(r, out RaycastHit hitInfo, interactRange))
            {
                if (hitInfo.collider.gameObject != null)
                {
                    Debug.Log(hitInfo.collider.gameObject.name);
                    ChangeMode(hitInfo.collider.gameObject);
                }
            }
        }

    }

    public void ActivatePhone()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        cam.GetComponent<CinemachinePanTilt>().enabled = false;
        mode = "lock";
        player.GetComponent<Rigidbody>().isKinematic = true;
    }
    public void DeactivatePhone()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        cam.GetComponent<CinemachinePanTilt>().enabled = true;
        mode = "free";
        player.GetComponent<Rigidbody>().isKinematic = false;
    }
}
