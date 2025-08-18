using UnityEngine;

public class PositionsManager : MonoBehaviour
{
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
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
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
        }
        else if (playerSalaPosition2.gameObject.activeSelf)
        {
            playerSalaPosition2.gameObject.SetActive(false);
            playerSalaPosition3.gameObject.SetActive(true);
            playerCasaPosition2.gameObject.SetActive(false);
            playerCasaPosition3.gameObject.SetActive(true);
        }
        else if (playerSalaPosition3.gameObject.activeSelf)
        {
            playerSalaPosition3.gameObject.SetActive(false);
            playerSalaPosition4.gameObject.SetActive(true);
            playerCasaPosition3.gameObject.SetActive(false);
            playerCasaPosition4.gameObject.SetActive(true);
        }
        else if (playerSalaPosition4.gameObject.activeSelf)
        {
            playerSalaPosition4.gameObject.SetActive(false);
            playerSalaPosition5.gameObject.SetActive(true);
            playerCasaPosition4.gameObject.SetActive(false);
            playerCasaPosition5.gameObject.SetActive(true);
        }
    }
}
