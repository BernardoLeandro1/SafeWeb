using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    public GameObject dialogBox;
    public TMP_Text nameText;
    public TMP_Text dialogueText;

    private string dialogueChecker = "";
    public bool lineFinish = false;
    private float textSpeed = 0.05f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeUI(){
        dialogBox.SetActive(!dialogBox.activeSelf);
    }

    public void ShowText(string name, string dialogue){
        lineFinish = false;
        nameText.text = name;
        StopAllCoroutines();
        dialogueText.text = string.Empty;
        StartCoroutine(TypeLine(dialogue));
    }

    IEnumerator TypeLine(string dialogue)
    {
        // check for limits due to dialogue box bug and inserts a \n instead of a space to fix it
        dialogueChecker = dialogue;
        var chars = dialogue.ToCharArray();
        int charBreak = 72;
        int i = 0;
        foreach (char c in chars)
        {
            if (c == char.Parse("\n")){
                if(i>charBreak){
                    charBreak += i%charBreak;
                    i=0;
                }
                else{
                    charBreak += i;
                    i=0;
                }
            }
            i++;
        }
        while(charBreak <= chars.Length){
            if(chars[charBreak] == char.Parse(" ")){
                chars[charBreak] = char.Parse("\n");
                charBreak += 72;
            }
            else{
                charBreak--;
            }
        }
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
}
