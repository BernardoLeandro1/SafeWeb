using UnityEngine;
using Unity.Cinemachine;

public class LogicManager : MonoBehaviour
{
    private string mode;

    public CinemachineCamera cam;
    private CinemachineCamera activeCam;

    private NodeManager nodeManager;
    public float interactRange;

    private UIManager uIManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        activeCam = cam;
        mode = "free";
        uIManager = GetComponent<UIManager>();
        nodeManager = GetComponent<NodeManager>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void ChangeMode(GameObject interactObj = null){
        if(mode == "free"){
            if(interactObj.gameObject.transform.childCount > 0){
                if(interactObj.gameObject.transform.GetChild(0).GetComponent<CinemachineCamera>()!=null){
                    mode = "lock";
                    activeCam = interactObj.gameObject.transform.GetChild(0).GetComponent<CinemachineCamera>();
                    activeCam.gameObject.SetActive(true);
                    cam.gameObject.SetActive(false);
                    uIManager.ChangeUI();
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                    nodeManager.NextNode();
                }
            }
        }
        else{
            mode = "free";
            activeCam.gameObject.SetActive(false);
            cam.gameObject.SetActive(true);
            activeCam = cam;
            uIManager.ChangeUI();
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    public string GetMode(){
        return mode;
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
