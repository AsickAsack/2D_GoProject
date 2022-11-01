using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageBtn : MonoBehaviour
{
    [SerializeField]
    private int stageIndex;
    [SerializeField]
    private Text StageText;
    public Sprite Icon;
    public float PaddingY;


    public RectTransform myRect;
    Vector2 OrgPos = Vector2.zero;

    private void Awake()
    {
        Initailize();
        myRect = this.GetComponent<RectTransform>();
        OrgPos = myRect.anchoredPosition;
        
    }

    public void Initailize()
    {
        StageText.text = "스테이지 " + (stageIndex+1);
        this.transform.GetChild(0).GetComponent<Image>().sprite = Icon;
    }

    public void GoDown(float y)
    {
        myRect.anchoredPosition = new Vector2(myRect.anchoredPosition.x, myRect.anchoredPosition.y - y);
    }
    public void ResetPosition()
    {
        myRect.anchoredPosition = OrgPos;

    }

}
