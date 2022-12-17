using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager instance;

    public DialoguePanel dialoguePanel;
    public DialogueManager MyManager;

    public int Action_Index = 0;
    public UnityAction[] TutorialAction;

    public int TutorialClick_index = 0;
    public UnityAction[] TutorialClickAction;

    public Canvas FocusCanvas;
    public RectTransform FocusOBJ;

    RectTransform TargetRect;
    public Transform OrgParent;
    public Transform FocusParent;
    public GameObject FocusFinger;
    public FocusClickOBJ ClickOBJ;
    int OrgSibling;

    public GameObject PowerUpZone;


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

        TutorialAction[4] = LookAtZone;
        TutorialAction[5] = LookAtOrg;
    }

    private void Start()
    {
        StartDialogue();
    }

    public void StartDialogue()
    {
        MyManager.SetDialogue(MyManager.Tutorial_Index);
    }


    public void FailRoutine()
    {
        MyManager.Tutorial_Index--;
        //Action_Index--;
        StartDialogue();
    }
   



    //튜토리얼 포커스 함수
    public void SetFocusOBJ(RectTransform TargetRect,bool FocusFinger)
    {
        this.TargetRect = TargetRect;
        OrgSibling = TargetRect.transform.GetSiblingIndex();
        OrgParent = TargetRect.parent;
        FocusCanvas.enabled = true;

        FocusOBJ.transform.position = TargetRect.transform.position;
        FocusOBJ.sizeDelta = new Vector2(TargetRect.sizeDelta.x+35.0f, TargetRect.sizeDelta.y + 35.0f);
        TargetRect.transform.SetParent(FocusParent);
        this.FocusFinger.SetActive(FocusFinger);

    }

    public void ReturnTargetRect()
    {
        TargetRect.SetParent(OrgParent);
        TargetRect.SetSiblingIndex(OrgSibling);
        FocusCanvas.enabled = false;
        FocusFinger.SetActive(false);

    }


    public void LookAtZone()
    {
        PowerUpZone.SetActive(true);
        Camera.main.transform.position = new Vector3(PowerUpZone.transform.position.x, PowerUpZone.transform.position.y, Camera.main.transform.position.z);
        Invoke("StartDialogue", 1.5f);
    }

    public void LookAtOrg()
    {
        Camera.main.transform.position = new Vector3(0, 1, Camera.main.transform.position.z);
        StartDialogue();
    }


}
