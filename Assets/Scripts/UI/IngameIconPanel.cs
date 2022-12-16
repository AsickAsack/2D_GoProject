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
        if(PlayerDB.Instance.playerdata.PlayFirst)
            TutorialManager.instance.TutorialAction[0] = SetTestMethod;
    }

    public void SetTestMethod()
    {
        TutorialManager.instance.SetFocusOBJ(MyIcon[0].GetComponent<RectTransform>());
    }


    //패널 사이즈 세팅
    public void SetPanel(bool IsTutorial)
    {
        if (IsTutorial)
        {
            myRect.sizeDelta = new Vector2(myRect.sizeDelta.x, myHeight * 1);

            MyIcon[0].gameObject.SetActive(true);
            //MyIcon[1].gameObject.SetActive(true);
        }
        else
        {
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
