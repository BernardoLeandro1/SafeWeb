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


    public GameObject cubejoao;
    public GameObject cubemaria;
    public GameObject cubeantonio;
    public GameObject cubeana;

    public GameObject cubeback;


    public GameObject characters;

    public bool unlockCharacters = false;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

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
        if (joao.GetComponentInChildren<MissionIDs>().isSolved())
        {
            joao.SetActive(false);
            cubejoao.SetActive(false);
            maria.SetActive(false);
            maria.GetComponentInChildren<MissionIDs>().MissionSolved();
            cubemaria.SetActive(false);
            
        }
        if (maria.GetComponentInChildren<MissionIDs>().isSolved())
        {
            joao.SetActive(false);
            joao.GetComponentInChildren<MissionIDs>().MissionSolved();
            cubejoao.SetActive(false);
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
            cubeback.SetActive(true);
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
}
