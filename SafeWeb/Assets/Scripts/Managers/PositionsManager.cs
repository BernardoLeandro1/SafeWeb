using UnityEngine;

public class PositionsManager : MonoBehaviour
{
    public GameObject sala1;
    public GameObject sala2;
    public GameObject sala3;
    public GameObject sala4;
    public GameObject sala5;
    public Transform playerSalaPosition1;
    public Transform playerSalaPosition2;
    public Transform playerSalaPosition3;
    public Transform playerSalaPosition4;
    public Transform playerSalaPosition5;
    public Transform playerCasaPosition1;
    public Transform playerCasaPosition2;
    public Transform playerCasaPosition3;
    public Transform playerCasaPosition4;
    public Transform playerCasaPosition5;

    public LogicManager logicManager;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        logicManager = GetComponent<LogicManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void ChangeDays()
    {
        if (playerSalaPosition1.gameObject.activeSelf)
        {
            playerSalaPosition1.gameObject.SetActive(false);
            playerSalaPosition2.gameObject.SetActive(true);
            playerCasaPosition1.gameObject.SetActive(false);
            playerCasaPosition2.gameObject.SetActive(true);
            sala1.SetActive(false);
            sala2.SetActive(true);
        }
        else if (playerSalaPosition2.gameObject.activeSelf)
        {
            playerSalaPosition2.gameObject.SetActive(false);
            playerSalaPosition3.gameObject.SetActive(true);
            playerCasaPosition2.gameObject.SetActive(false);
            playerCasaPosition3.gameObject.SetActive(true);
            sala2.SetActive(false);
            sala3.SetActive(true);
        }
        else if (playerSalaPosition3.gameObject.activeSelf)
        {
            playerSalaPosition3.gameObject.SetActive(false);
            playerSalaPosition4.gameObject.SetActive(true);
            playerCasaPosition3.gameObject.SetActive(false);
            playerCasaPosition4.gameObject.SetActive(true);
            sala3.SetActive(false);
            sala4.SetActive(true);
        }
        else if (playerSalaPosition4.gameObject.activeSelf)
        {
            playerSalaPosition4.gameObject.SetActive(false);
            playerSalaPosition5.gameObject.SetActive(true);
            playerCasaPosition4.gameObject.SetActive(false);
            playerCasaPosition5.gameObject.SetActive(true);
            sala4.SetActive(false);
            sala5.SetActive(true);
        }
    }
}
