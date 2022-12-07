using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public enum MetaDay
{
    Day,SunSet,Night
}

[Serializable]
public struct MyMetaScreen
{
    public Color BtnColor;
    public Color ArrowColor;
    public Sprite[] Sprite;
}

public class MetaScreen : MonoBehaviour
{
    public Animator MetaRotate;
    public float[] RotateSpeed;

    public GameObject TalkObject;
    public TMPro.TMP_Text MetaTalkText;
    Coroutine MetaTalkCo;

    [TextArea]
    public string[] MetaDayTalk;
    [TextArea]
    public string[] MetaSunSetTalk;
    [TextArea]
    public string[] MetaNightTalk;

    string[] CurMetaTalk;
    int TalkIndex = 0;
    int TalkLength = 0;

    [Header("[기본 오브젝트]")]

    public Image Btn;
    public Image Line;
    public Image Arrow;

    public Image[] BasicImages;

    public MyMetaScreen[] myMetaScreen;
    

    private void Awake()
    {
        SetScreen(DateTime.Now.Hour);
        
    }

    public void ClickMeta()
    {
        if(MetaTalkCo == null)
        {
            MetaTalkCo = StartCoroutine(StartTalk());
        }
        else
        {
            StopCoroutine(MetaTalkCo);
            MetaTalkCo = StartCoroutine(StartTalk());
        }
    }

    IEnumerator StartTalk()
    {
        MetaTalkText.text = CurMetaTalk[TalkIndex++ % TalkLength];
        TalkObject.SetActive(true);

        yield return new WaitForSeconds(1.5f);

        TalkObject.SetActive(false);
    }

    public void SetSpriteAndColor(MetaDay metaday)
    {
        Btn.color = Line.color = myMetaScreen[(int)metaday].BtnColor;
        Arrow.color = myMetaScreen[(int)metaday].ArrowColor;

        for(int i=0;i<BasicImages.Length;i++)
        {
            BasicImages[i].sprite = myMetaScreen[(int)metaday].Sprite[i];
        }

        TalkIndex = 0;
        MetaRotate.speed = RotateSpeed[(int)metaday];

        switch (metaday)
        {
            case MetaDay.Day:

                CurMetaTalk = MetaDayTalk;

                break;

            case MetaDay.SunSet:

                CurMetaTalk = MetaSunSetTalk;

                break;
            case MetaDay.Night:

                CurMetaTalk = MetaNightTalk;

                break;

        }

        TalkLength = CurMetaTalk.Length;
    }

    public void SetScreen(int hour)
    {
        if(0 <= hour && hour <= 5)
        {
            SetSpriteAndColor(MetaDay.Night);
        }
        else if (6 <= hour && hour <= 8)
        {
            SetSpriteAndColor(MetaDay.SunSet);
        }
        else if (9 <= hour && hour <= 15)
        {
            SetSpriteAndColor(MetaDay.Day);
        }
        else if (16 <= hour && hour <= 18)
        {
            SetSpriteAndColor(MetaDay.SunSet);
        }
        else if (19 <= hour && hour <= 23)
        {
            SetSpriteAndColor(MetaDay.Night);
        }

    }




}
