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
    public Coroutine DialogueCo;

    private void Awake()
    {
        TextAsset temp = Resources.Load("TutorialDialogue") as TextAsset;
        TutorialString = JsonConvert.DeserializeObject<string[]>(temp.ToString());
    }

    public bool SetDialogue(int index)
    {
        if (DialogueCo == null)
        {
            CurIndex = index;
            DialogueCo = StartCoroutine(StartDialogue(CurIndex));

            return true;
        }
        else
        {
            DialogueText.text = TutorialString[CurIndex];
            StopCoroutine(DialogueCo);
            DialogueCo = null;

            return false;
        }
    }

    string dialogue;
    IEnumerator StartDialogue(int index)
    {
        dialogue = null;
        int length = TutorialString[index].Length;
        int temp = 0;
        

        while (length != 0)
        {
            length--;
            dialogue += TutorialString[index][temp++];
            DialogueText.text = dialogue;

            yield return new WaitForSeconds(0.03f);
        }

        DialogueCo = null;
    }


}
