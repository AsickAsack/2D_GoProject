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
    public Canvas DialogueCanvas;
    public GameObject EndArrow;

    public Coroutine DialogueCo;

    public bool IsAction;

    private void Awake()
    {
        TextAsset temp = Resources.Load("TutorialDialogue") as TextAsset;
        TutorialString = JsonConvert.DeserializeObject<string[]>(temp.ToString());
    }


    public void Panel_OnOff(bool check)
    {
        DialogueCanvas.enabled = check;
    }

    public bool SetDialogue(int index)
    {
        //대화가 끝난경우
        if (DialogueCo == null)
        {
            Panel_OnOff(true);
            CurIndex = index;
            DialogueCo = StartCoroutine(StartDialogue(CurIndex));

            return true;
        }
        else //대화가 끝나지 않은경우
        {
            EndArrow.SetActive(true);
            DialogueText.text = TutorialString[CurIndex];
            StopCoroutine(DialogueCo);
            DialogueCo = null;

            return false;
        }
    }
    //실수 했을경우
    public void SetDialogue(string Dialogue)
    {

        Panel_OnOff(true);
        EndArrow.SetActive(true);
        DialogueText.text = Dialogue;
        TutorialManager.instance.Action_Index--;

        IsAction = true;
    }

    string dialogue;
    IEnumerator StartDialogue(int index)
    {
        EndArrow.SetActive(false);

        dialogue = null;
        int length = TutorialString[index].Length;
        int temp = 0;

        if (TutorialString[index].Contains('`'))
        {
            IsAction = true;
        }


        while (length != 0)
        {
            length--;
            dialogue += FindAction(TutorialString[index][temp++]);
            DialogueText.text = dialogue;

            yield return new WaitForSeconds(0.03f);
        }

        EndArrow.SetActive(true);
        DialogueCo = null;
    }

    public char FindAction(char myChar)
    {
        if (myChar == '`')
        {
            IsAction = true;
            return ' ';
        }
        else
            return myChar;
    }

}
