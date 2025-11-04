using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public GameObject dialogBox;
    public TMP_Text nameText;
    public TMP_Text dialogueText;

    public GameObject choiceBox;

    public Button forward;
    public Button backward;

    public Image fadeInStart;

    public GameObject toDoList;

    public GameObject phone;

    public TMP_Text toDoListText;

    public GameObject finalAdviceBox;
    public TMP_Text finalAdviceText;

    public GameObject buttonAdvance;

    public GameObject buttonBack;

    public GameObject optionsMenu;

    private string dialogueChecker = "";
    public bool lineFinish = false;
    private float textSpeed = 0.05f;

    ChoicesPanelManager choicePanel;

    private int help = 0;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        choicePanel = ChoicesPanelManager.instance;

    }

    // Update is called once per frame
    void Update()
    {
        if (optionsMenu.activeSelf && help == 0)
        {
            help = 1;
            buttonAdvance.GetComponent<Button>().interactable = false;
            buttonBack.GetComponent<Button>().interactable = false;
        }
        else if (!optionsMenu.activeSelf && help == 1)
        {
            help = 0;
            buttonAdvance.GetComponent<Button>().interactable = true;
            buttonBack.GetComponent<Button>().interactable = true;
        }
    }

    public void ChangeUI()
    {
        dialogBox.SetActive(!dialogBox.activeSelf);
    }

    public void ShowText(string name, string dialogue)
    {
        lineFinish = false;
        nameText.text = name;
        StopAllCoroutines();
        dialogueText.text = string.Empty;
        StartCoroutine(TypeLine(dialogue));
    }

    public void ShowTextAfterBackwords(string name, string dialogue)
    {
        nameText.text = name;
        dialogueText.text = dialogue;
    }

    IEnumerator TypeLine(string dialogue)
    {
        // check for limits due to dialogue box bug and inserts a \n instead of a space to fix it
        dialogueChecker = dialogue;
        var chars = dialogue.ToCharArray();
        // int charBreak = 72;
        // int i = 0;
        // foreach (char c in chars)
        // {
        //     if (c == char.Parse("\n"))
        //     {
        //         if (i > charBreak)
        //         {
        //             charBreak += i % charBreak;
        //             i = 0;
        //         }
        //         else
        //         {
        //             charBreak += i;
        //             i = 0;
        //         }
        //     }
        //     i++;
        // }
        // while (charBreak < chars.Length)
        // {
        //     if (chars[charBreak] == char.Parse(" "))
        //     {
        //         chars[charBreak] = char.Parse("\n");
        //         charBreak += 72;
        //     }
        //     else
        //     {
        //         charBreak--;
        //     }
        // }
        // add characters to the screen
        foreach (char c in chars)
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
        lineFinish = true;
    }

    public void FinishLine()
    {
        StopAllCoroutines();
        dialogueText.text = dialogueChecker;
        lineFinish = true;
    }

    public void DisplayChoicesPanel(DialogueChoices[] choices)
    {
        StopAllCoroutines();
        ToggleChoiceCanvas(boolean: true);
        StartCoroutine(choicePanel.GenerateChoices(choices));
    }

    public void ToggleChoiceCanvas(bool boolean)
    {
        choiceBox.gameObject.SetActive(boolean);
        forward.interactable = !boolean;
        backward.interactable = !boolean;
    }

    public void DisplayToDoList(string toDo)
    {
        if (toDo == "")
        {
            toDoList.SetActive(true);
        }
        else
        {
            toDoList.SetActive(true);
            toDoListText.text = toDo;
        }
    }

    public void HideToDoList()
    {
        toDoList.SetActive(false);
    }

    public void DisplayPhone()
    { 
        HideToDoList();
        phone.SetActive(true);
    }
    public void HidePhone()
    {
        phone.SetActive(false);
    }
    public void ShowFinalAdvice(string advices)
    {
        finalAdviceBox.SetActive(true);
        finalAdviceText.text = advices;
    }
}
