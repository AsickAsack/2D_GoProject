using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;
using System;
using System.IO;

public class DialogueManager : MonoBehaviour
{
    public int Tutorial_Index;
    public int CurIndex;
    public string[] TutorialString;

    public TMPro.TMP_Text DialogueText;
    public GameObject EndArrow;

    public Canvas MyCanvas;
    public Coroutine DialogueCo;

    public bool IsAction;
    private void Awake()
    {
        TextAsset temp = Resources.Load("TutorialDialogue") as TextAsset;
        TutorialString = JsonConvert.DeserializeObject<string[]>(temp.ToString());

        MyCanvas = this.GetComponent<Canvas>();
    }


    public void Panel_OnOff(bool check)
    {
        MyCanvas.enabled = check;
    }

    public void SetDialogue(int index)
    {
        if(IsAction)
        {
            IsAction = false;
            Panel_OnOff(false);
            TutorialManager.instance.TutorialAction[TutorialManager.instance.Action_Index++]();
        }
        //대화가 끝난경우
        else if (DialogueCo == null)
        {

            Panel_OnOff(true);
            CurIndex = index;
            DialogueCo = StartCoroutine(StartDialogue(CurIndex));
            Tutorial_Index++;


        }
        else //대화가 끝나지 않은경우
        {
            EndArrow.SetActive(true);

            StopCoroutine(DialogueCo);
            DialogueText.text = ChangeString(TutorialString[CurIndex]);
            DialogueCo = null;

            if (TutorialString[CurIndex].Contains('`'))
            {
                IsAction = true;
            }

        }
    }
 

    string dialogue;
    IEnumerator StartDialogue(int index)
    {
        EndArrow.SetActive(false);

        dialogue = null;
        int length = TutorialString[index].Length;
        int temp = 0;    

        while (length != 0)
        {
            length--;
            dialogue += FindAction(TutorialString[index][temp++]);
            DialogueText.text = dialogue;

            yield return new WaitForSeconds(0.03f);
        }

        EndArrow.SetActive(true);
        DialogueCo = null;
         
        if (TutorialString[index].Contains('`'))
        {
            IsAction = true;
        }
        
        
    }

    public char FindAction(char myChar)
    {
        if (myChar == '`')
        {
            return ' ';
        }
        else
            return myChar;
    }

    public string ChangeString(string myString)
    {
        myString = myString.Replace('`',' ');

        return myString;
    }

}
