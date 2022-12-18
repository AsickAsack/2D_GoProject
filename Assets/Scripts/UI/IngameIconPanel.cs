using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngameIconPanel : MonoBehaviour
{
    float myHeight = 0.0f;
    public InGameIcon[] MyIcon;
    public RectTransform myRect;
    public float RectSpace;
    public float PanelChangeSpeed;

    private void Awake()
    {
        myHeight = MyIcon[0].GetComponent<RectTransform>().rect.height + RectSpace;
        SetPanel(PlayerDB.Instance.playerdata.PlayFirst);

    }

    private void Start()
    {
        if (PlayerDB.Instance.playerdata.PlayFirst)
        {
            TutorialManager.instance.TutorialAction[0] = FocusFirst;
            TutorialManager.instance.TutorialAction[2] = FocusSecond;
        }
    }

    public void FocusFirst()
    {
        TutorialManager.instance.SetFocusOBJ(MyIcon[0].GetComponent<RectTransform>(),true);
    }

    public void FocusSecond()
    {
        MyIcon[0].gameObject.SetActive(false);
        SetPanelSize(false);
        MyIcon[1].transform.position = MyIcon[0].transform.position;

        TutorialManager.instance.ClickOBJ.IsIgnore = true;
        TutorialPlaymanager.Instance.ingameUI.InfoIcon.SetActive(true);
        TutorialPlaymanager.Instance.ingameUI.SetCharacterPopUP(true);
        StartCoroutine(FocusTwice());
    }

    IEnumerator FocusTwice()
    {
        TutorialManager.instance.SetFocusOBJ(TutorialPlaymanager.Instance.ingameUI.InfoIcon.GetComponent<RectTransform>(), false);
        

        yield return new WaitForSeconds(1.5f);
        TutorialManager.instance.ReturnTargetRect();
        TutorialManager.instance.SetFocusOBJ(MyIcon[1].transform.GetComponent<RectTransform>(), false);
        
        yield return new WaitForSeconds(1.5f);
        TutorialManager.instance.ReturnTargetRect();
        TutorialPlaymanager.Instance.ingameUI.PopupAnim.SetTrigger("GoRight");

        TutorialManager.instance.ClickOBJ.IsIgnore = false;
        TutorialManager.instance.StartDialogue();
        
    }

    //패널 사이즈 세팅
    public void SetPanel(bool IsTutorial)
    {
        if (IsTutorial)
        {
            myRect.sizeDelta = new Vector2(myRect.sizeDelta.x, myHeight * 2);

            MyIcon[0].gameObject.SetActive(true);
            MyIcon[1].gameObject.SetActive(true);
        }
        else
        {
            if(StageManager.instance.IsMorphing)
            {
                myRect.sizeDelta = new Vector2(myRect.sizeDelta.x, myHeight);
                MyIcon[0].gameObject.SetActive(true);
                return;
            }

            myRect.sizeDelta = new Vector2(myRect.sizeDelta.x, myHeight * StageManager.instance.GetNeedCharacterCount());

            for (int i = 0; i < StageManager.instance.GetNeedCharacterCount(); i++)
            {
                MyIcon[i].gameObject.SetActive(true);
            }
        }
    }

    public void SetPanelSize(bool check)
    {
        if (check)
            myRect.sizeDelta = new Vector2(myRect.sizeDelta.x, myRect.sizeDelta.y + myHeight);
        else
            myRect.sizeDelta = new Vector2(myRect.sizeDelta.x, myRect.sizeDelta.y - myHeight);
    }

 


}
