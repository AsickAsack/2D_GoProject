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
        SetPanel();

    }

    //패널 사이즈 세팅
    public void SetPanel()
    {
        myRect.sizeDelta = new Vector2(myRect.sizeDelta.x,myHeight*StageManager.instance.GetNeedCharacterCount());

        for(int i=0; i< StageManager.instance.GetNeedCharacterCount();i++)
        {
            MyIcon[i].gameObject.SetActive(true);
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
