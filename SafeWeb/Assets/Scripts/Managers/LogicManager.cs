using UnityEngine;
using Unity.Cinemachine;

public class LogicManager : MonoBehaviour
{
    private string mode;

    public CinemachineCamera cam;

    public GameObject player;

    public GameObject casa;

    public GameObject salaAula;

    public GameObject salaArt;

    public GameObject cinema;

    public GameObject jardim;

    public Transform freePlayer;


    public Transform casaPlayer;

    public Transform shoppingPlayer;

    public Transform jardimPlayer;

    public Transform aulaPlayer;
    public Transform quartoPlayer;


    public Transform salaPlayer;
    public Transform artclubPlayer;

    private CinemachineCamera activeCam;

    private NodeManager nodeManager;
    public float interactRange;

    private UIManager uIManager;

    private MissionManager missionManager;

    private CharactersManager charactersManager;

    private GameObject currentMissionObject;

    private bool hasDoneMission = true;

    public bool canGoToBed = false;

    private bool teleport = false;

    private string teleportFrom = "";

    private int day = 1;

    private int freewalk = 0;

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
            if (day == 3 && nodeManager.goShopping == true && teleportFrom.Contains("escola"))
            {
                //casa.SetActive(false);
                //salaArt.SetActive(false);
                salaAula.SetActive(false);
                cinema.SetActive(true);
                jardim.SetActive(false);
                player.transform.position = shoppingPlayer.position;
                nodeManager.NextNode();
                hasDoneMission = true;
                charactersManager.UpdateCharactersPositions(3);
            }
            else if (day == 3 && missionManager.solved == 5 && teleportFrom.Contains("shopping"))
            {
                //casa.SetActive(true);
                //salaArt.SetActive(false);
                salaAula.SetActive(false);
                cinema.SetActive(false);
                jardim.SetActive(false);
                player.transform.position = casaPlayer.position;
                nodeManager.NextNode();
            }
            else if (day == 3 && missionManager.solved == 6 && teleportFrom.Contains("bedroom"))
            {
                canGoToBed = true;
                player.transform.position = quartoPlayer.position;
                nodeManager.NextNode();
            }
            else if (day == 4 && missionManager.solved == 1 && teleportFrom.Contains("escola"))
            {
                //casa.SetActive(false);
                //salaArt.SetActive(false);
                salaAula.SetActive(false);
                cinema.SetActive(false);
                jardim.SetActive(true);
                player.transform.position = jardimPlayer.position;
                hasDoneMission = true;
                nodeManager.NextNode();
            }
            else if (day == 4 && missionManager.solved == 5 && teleportFrom.Contains("jardim"))
            {
                //casa.SetActive(true);
                //salaArt.SetActive(false);
                salaAula.SetActive(false);
                cinema.SetActive(false);
                jardim.SetActive(false);
                player.transform.position = casaPlayer.position;
                nodeManager.NextNode();
            }
            else if (day == 4 && missionManager.solved == 6 && teleportFrom.Contains("bedroom"))
            {
                player.transform.position = quartoPlayer.position;
                nodeManager.NextNode();
            }
            else if (day == 5 && missionManager.solved == 1 && teleportFrom.Contains("escola"))
            {
                //casa.SetActive(false);
                //salaArt.SetActive(true);
                salaAula.SetActive(false);
                cinema.SetActive(false);
                jardim.SetActive(false);
                player.transform.position = artclubPlayer.position;
                nodeManager.NextNode();
            }
            else if (missionManager.solved == 4 && teleportFrom.Contains("escola"))
            {
                //casa.SetActive(true);
                //salaArt.SetActive(false);
                salaAula.SetActive(false);
                cinema.SetActive(false);
                jardim.SetActive(false);
                player.transform.position = casaPlayer.position;
                nodeManager.NextNode();
            }
            else if (missionManager.solved == 0 && teleportFrom.Contains(value: "casa"))
            {
                //casa.SetActive(false);
                //salaArt.SetActive(false);
                salaAula.SetActive(true);
                cinema.SetActive(false);
                jardim.SetActive(false);
                player.transform.position = aulaPlayer.position;
                nodeManager.NextNode();
            }
            else if (missionManager.solved == 0 && teleportFrom.Contains("bedroom"))
            {
                player.transform.position = salaPlayer.position;
                nodeManager.NextNode();
                charactersManager.UpdateCharacters();
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
        Debug.Log(hasDoneMission);
        if (GetMode() == "free")
        {
            if (interactObj != null)
            {
                if (interactObj.gameObject.transform.childCount > 0)
                {
                    if (interactObj.gameObject.transform.GetChild(0).GetComponent<CinemachineCamera>() != null)
                    {
                        if (interactObj.gameObject.GetComponent<MissionIDs>() != null)
                        {
                            if ((currentMissionObject == null || interactObj == currentMissionObject) && hasDoneMission == false && interactObj.gameObject.GetComponent<MissionIDs>().GetMissionID() >= 0)
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
                        else if (interactObj.gameObject.name.Contains("Bed"))
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
                                day++;
                                charactersManager.UpdateCharactersPositions(day);
                                missionManager.solved = 0;
                                charactersManager.HideCharacters();
                                uIManager.fadeInStart.GetComponent<Animator>().SetTrigger("End");
                                if (day == 4)
                                {
                                    charactersManager.UpdateCharacters();
                                }
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
                            if (day == 5 && freewalk == 0 && missionManager.solved == 1)
                            {
                                hasDoneMission = true;
                                freewalk = 1;
                                charactersManager.ChangeCubes(4);
                            }
                            
                            player.GetComponent<Rigidbody>().isKinematic = true;
                            charactersManager.unlockCharacters = true;
                            charactersManager.UpdateCharacters();
                            charactersManager.HideCharacters();
                            charactersManager.ChangeCubes(2);
                            freePlayer.position = player.transform.position;
                            currentMissionObject = null;
                        }
                        else
                        {
                            Debug.Log("Dead end");
                        }

                    }
                    else
                    {
                        if (interactObj.name.Contains("escola") && day == 3 && nodeManager.goShopping == true)
                        {
                            teleport = true;
                            teleportFrom = "escola";
                        }
                        else if (interactObj.name.Contains("shopping") && day == 3 && missionManager.solved == 5)
                        {
                            teleport = true;
                            teleportFrom = "shopping";
                        }
                        else if (interactObj.name.Contains("bedroom") && day == 3 && missionManager.solved == 6)
                        {
                            teleport = true;
                            teleportFrom = "bedroom";
                        }
                        else if (interactObj.name.Contains("escola") && day == 4 && missionManager.solved == 1)
                        {
                            teleport = true;
                            teleportFrom = "escola";
                        }
                        else if (interactObj.name.Contains("jardim") && day == 4 && missionManager.solved == 5)
                        {
                            teleport = true;
                            teleportFrom = "jardim";
                        }
                        else if (interactObj.name.Contains("bedroom") && day == 4 && missionManager.solved == 6)
                        {
                            teleport = true;
                            teleportFrom = "bedroom";
                        }
                        else if (interactObj.name.Contains("escola") && day == 5 && missionManager.solved == 1)
                        {
                            teleport = true;
                            teleportFrom = "escola";
                        }
                        else if (interactObj.name.Contains("escola") && missionManager.solved == 4)
                        {
                            teleport = true;
                            teleportFrom = "escola";
                        }
                        else if (interactObj.name.Contains("casa") && missionManager.solved == 0)
                        {
                            teleport = true;
                            teleportFrom = "casa";
                        }
                        else if (interactObj.name.Contains("bedroom") && missionManager.solved == 0)
                        {
                            teleport = true;
                            teleportFrom = "bedroom";
                        }
                        else if (interactObj.name.Contains("bedroom") && missionManager.solved == 5)
                        {
                            teleport = true;
                            teleportFrom = "bedroom";
                        }
                        else
                        {
                            Debug.Log("Dead end 2");
                        }

                    }
                }
            }
            else
            {

            }

        }
        else
        {
            mode = "free";
            if (stringObj != null)
            {
                player.transform.position = freePlayer.position;
                cam.GetComponent<CinemachinePanTilt>().PanAxis.Value = 0f;
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
        if (day == 3)
        {
            hasDoneMission = false;
            currentMissionObject = null;
        }
        else if (day == 4)
        {
            if (missionManager.GetMissions()[16].available == false && missionManager.GetCurrentMission() != missionManager.GetMissions()[21])
            {
                hasDoneMission = false;
                currentMissionObject = null;
            }
            else if (missionManager.GetCurrentMission() != missionManager.GetMissions()[21])
            {
                charactersManager.ChangeCubes(1);
            }
            
        }
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

    public int GetDay()
    {
        return day;
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
        uIManager.DisplayToDoList("");
        charactersManager.UpdateCharactersPositions(day);
    }
}
