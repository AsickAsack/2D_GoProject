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
            StartCoroutine(StartSetPanelSize(myRect.sizeDelta.y + myHeight, PanelChangeSpeed));
        else
            StartCoroutine(StartSetPanelSize(myRect.sizeDelta.y - myHeight, -PanelChangeSpeed));
    }

    IEnumerator StartSetPanelSize(float size,float cir)
    {
        while(true)
        {
            float y = myRect.sizeDelta.y + (Time.deltaTime * cir);
            Mathf.Clamp(y, size, myRect.sizeDelta.y);
            myRect.sizeDelta = new Vector2(myRect.sizeDelta.x, y);

            if ((int)y == (int)myRect.sizeDelta.y)
                break;

            yield return null;
        }

        Debug.Log("코루틴 끝");

    }



}
