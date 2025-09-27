using Unity.VisualScripting;
using UnityEditor.Timeline.Actions;
using UnityEngine;

public class CharactersManager : MonoBehaviour
{
    public GameObject joao;

    public GameObject maria;

    public GameObject antonio;

    public GameObject ana;

    public GameObject mae;
    public GameObject pai;

    public GameObject day3Chars;
    public GameObject day4Chars;
    public GameObject day4CharsJardim;

    public GameObject cubejoao;
    public GameObject cubemaria;
    public GameObject cubeantonio;
    public GameObject cubeana;

    public GameObject cubeback;


    public GameObject characters;

    public bool unlockCharacters = false;

    public Transform joaoday2;
    public Transform joaoday3;
    public Transform joaoday4;
    public Transform joaoday5;
    public Transform anaday2;
    public Transform anaday3;
    public Transform anaday4;
    public Transform anaday5;
    public Transform mariaday2;
    public Transform mariaday3;
    public Transform mariaday4;
    public Transform mariaday5;
    public Transform antonioday2;
    public Transform antonioday3;
    public Transform antonioday4;
    public Transform antonioday5;

    LogicManager logicManager;

    private int joint = 0;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        logicManager = GetComponent<LogicManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void HideCharacters()
    {
        characters.SetActive(false);
    }


    public void DisplayCharacters()
    {
        if (unlockCharacters)
        {
            characters.SetActive(true);
        }
    }

    public void UpdateCharacters()
    {

        ChangeCubes(3);
        if (joao.GetComponentInChildren<MissionIDs>().isSolved() )
        {
            joao.SetActive(false);
            cubejoao.SetActive(false);
            if (logicManager.GetDay() == 1 && joint == 0)
            {
                maria.SetActive(false);
                maria.GetComponentInChildren<MissionIDs>().MissionSolved();
                cubemaria.SetActive(false);
                joint = 1;
            }

        }
        if (maria.GetComponentInChildren<MissionIDs>().isSolved())
        {
            if (logicManager.GetDay() == 1 && joint==0)
            {
                joao.SetActive(false);
                joao.GetComponentInChildren<MissionIDs>().MissionSolved();
                cubejoao.SetActive(false);
                joint = 1;
            }
            maria.SetActive(false);
            cubemaria.SetActive(false);
        }
        if (ana.GetComponentInChildren<MissionIDs>().isSolved())
        {
            ana.SetActive(false);
            cubeana.SetActive(false);
        }
        if (antonio.GetComponentInChildren<MissionIDs>().isSolved())
        {
            antonio.SetActive(false);
            cubeantonio.SetActive(false);
        }
        if (joao.GetComponentInChildren<MissionIDs>().isSolved() && maria.GetComponentInChildren<MissionIDs>().isSolved() && ana.GetComponentInChildren<MissionIDs>().isSolved() && antonio.GetComponentInChildren<MissionIDs>().isSolved() && logicManager.GetDay() == 3)
        {
            day3Chars.SetActive(true);
        }
        else {
            day3Chars.SetActive(false);
        }
        if (logicManager.GetDay() == 4 && !(joao.GetComponentInChildren<MissionIDs>().isSolved() && maria.GetComponentInChildren<MissionIDs>().isSolved() && ana.GetComponentInChildren<MissionIDs>().isSolved() && antonio.GetComponentInChildren<MissionIDs>().isSolved()))
        {
            day4Chars.SetActive(true);
        }
        else
        {
            day4Chars.SetActive(false);
        }
        if (joao.GetComponentInChildren<MissionIDs>().isSolved() && maria.GetComponentInChildren<MissionIDs>().isSolved() && ana.GetComponentInChildren<MissionIDs>().isSolved() && antonio.GetComponentInChildren<MissionIDs>().isSolved())
        {
            pai.SetActive(true);
            mae.SetActive(true);
        }
        if (pai.GetComponentInChildren<MissionIDs>().isSolved())
        {
            pai.SetActive(false);
            mae.SetActive(false);
        }
    }


    public void ChangeCubes(int i)
    {
        if (i == 0)
        {
            cubemaria.SetActive(false);
            cubejoao.SetActive(false);
            cubeana.SetActive(false);
            cubeantonio.SetActive(false);
            cubeback.SetActive(false);
        }
        else if (i == 1)
        {
            if (logicManager.GetDay() != 4)
            {
                cubeback.SetActive(true);
            }
        }
        else if (i == 2)
        {
            cubeback.SetActive(false);
        }
        else if (i == 3)
        {
            if (!joao.GetComponentInChildren<MissionIDs>().isSolved())
            {
                cubejoao.SetActive(true);
                cubemaria.SetActive(true);

            }
            if (!maria.GetComponentInChildren<MissionIDs>().isSolved())
            {
                cubejoao.SetActive(true);
                cubemaria.SetActive(true);
            }
            if (!ana.GetComponentInChildren<MissionIDs>().isSolved())
            {
                cubeana.SetActive(true);
            }
            if (!antonio.GetComponentInChildren<MissionIDs>().isSolved())
            {
                cubeantonio.SetActive(true);
            }
        }

    }


    public void Wave(GameObject character)
    {
        //Debug.Log(character.gameObject.name);
        if (character.gameObject.name.Contains("joao") || character.gameObject.name.Contains("maria"))
        {
            joao.GetComponent<Animator>().SetTrigger("Wave");
            maria.GetComponent<Animator>().SetTrigger("Wave");
        }
        else if (character.gameObject.name.Contains("dad"))
        {
            pai.GetComponent<Animator>().SetTrigger("Wave");
            mae.GetComponent<Animator>().SetTrigger("Wave");
        }
        else
        {
            character.GetComponent<Animator>().SetTrigger("Wave");
        }
    }

    public void UpdateCharactersPositions(int day)
    {
        if (day == 2)
        {
            joao.transform.position = joaoday2.position;
            ana.transform.position = anaday2.position;
            maria.transform.position = mariaday2.position;
            antonio.transform.position = antonioday2.position;
            joao.transform.rotation = joaoday2.rotation;
            ana.transform.rotation = anaday2.rotation;
            maria.transform.rotation = mariaday2.rotation;
            antonio.transform.rotation = antonioday2.rotation;
            NewDay();
        }
        else if (day == 3)
        {
            joao.transform.position = joaoday3.position;
            ana.transform.position = anaday3.position;
            maria.transform.position = mariaday3.position;
            antonio.transform.position = antonioday3.position;
            joao.transform.rotation = joaoday3.rotation;
            ana.transform.rotation = anaday3.rotation;
            maria.transform.rotation = mariaday3.rotation;
            antonio.transform.rotation = antonioday3.rotation;
            NewDay();
        }
        else if (day == 4)
        {
            joao.transform.position = joaoday4.position;
            ana.transform.position = anaday4.position;
            maria.transform.position = mariaday4.position;
            antonio.transform.position = antonioday4.position;
            joao.transform.rotation = joaoday4.rotation;
            ana.transform.rotation = anaday4.rotation;
            maria.transform.rotation = mariaday4.rotation;
            antonio.transform.rotation = antonioday4.rotation;
            NewDay();
        }
        else if (day == 5)
        {
            joao.transform.position = joaoday5.position;
            ana.transform.position = anaday5.position;
            maria.transform.position = mariaday5.position;
            antonio.transform.position = antonioday5.position;
            joao.transform.rotation = joaoday5.rotation;
            ana.transform.rotation = anaday5.rotation;
            maria.transform.rotation = mariaday5.rotation;
            antonio.transform.rotation = antonioday5.rotation;
            NewDay();
        }
    }

    public void NewDay()
    {
        pai.SetActive(false);
        mae.SetActive(false);
        joao.GetComponentInChildren<MissionIDs>().ResetMissions();
        ana.GetComponentInChildren<MissionIDs>().ResetMissions();
        maria.GetComponentInChildren<MissionIDs>().ResetMissions();
        antonio.GetComponentInChildren<MissionIDs>().ResetMissions();
        pai.GetComponentInChildren<MissionIDs>().ResetMissions();
    }

    public void ShowCharacters()
    {
        joao.SetActive(true);
        ana.SetActive(true);
        maria.SetActive(true);
        antonio.SetActive(true);
        pai.SetActive(true);
        mae.SetActive(true);
    }
}
