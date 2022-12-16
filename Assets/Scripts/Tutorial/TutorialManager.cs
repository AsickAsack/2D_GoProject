using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager instance;

    public DialoguePanel dialoguePanel;
    public DialogueManager MyManager;

    public int Tutorial_index = 0;
    public UnityAction[] TutorialAction;

    public int TutorialClick_index = 0;
    public UnityAction[] TutorialClickAction;

    public Canvas FocusCanvas;
    public RectTransform FocusOBJ;

    RectTransform TargetRect;
    public Transform OrgParent;
    public Transform FocusParent;

    public int Action_Index = 0;


    private void Awake()
    {
    
        instance = this;
        TutorialAction = new UnityAction[10];
        TutorialClickAction = new UnityAction[10];
        dialoguePanel.DialogueAction = StartDialogue;

        //클릭 액션에는 무조건 되돌리는게 있어야함 ㅋ
        for (int i = 0; i < TutorialClickAction.Length; i++)
        {
            TutorialClickAction[i] += ReturnTargetRect;
        }
    }

    private void Start()
    {
        StartDialogue();
    }

    public void StartDialogue()
    {
        if (MyManager.IsAction)
        {
            MyManager.IsAction = false;
            MyManager.Panel_OnOff(false);
            TutorialAction[Action_Index++]();
        }
        else
        {
            if (MyManager.SetDialogue(MyManager.Tutorial_Index))
            {
                MyManager.Tutorial_Index++;
            }
        }
    }


   



    //튜토리얼 포커스 함수 + 손가락넣어얗암
    public void SetFocusOBJ(RectTransform TargetRect)
    {
        this.TargetRect = TargetRect;
        OrgParent = TargetRect.parent;
        FocusCanvas.enabled = true;

        FocusOBJ.transform.position = TargetRect.transform.position;
        FocusOBJ.sizeDelta = new Vector2(TargetRect.sizeDelta.x+35.0f, TargetRect.sizeDelta.y + 35.0f);
        TargetRect.transform.SetParent(FocusParent); 

    }

    public void ReturnTargetRect()
    {
        TargetRect.SetParent(OrgParent);
        FocusCanvas.enabled = false;

    }



}
