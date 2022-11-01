using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageBtnManager : MonoBehaviour
{
    public StageBtn[] stageBtn;
    public RectTransform DownBtns;
    public float PaddingY;
    private int? CurIndex = null;

    private void Awake()
    {
        stageBtn = this.GetComponentsInChildren<StageBtn>();
    }

    public void BackMain()
    {
        CurIndex = null;
        ResetPos();
    }

    public void SetDownPos(int index)
    {
        if (CurIndex == index) 
        {
            BackMain();
            return; 
        }

        CurIndex = index;

        ResetPos();
        for (int i = index + 1; i < stageBtn.Length; i++)
            stageBtn[i].GoDown(PaddingY);

        DownBtns.gameObject.SetActive(true);
        DownBtns.anchoredPosition = new Vector2(stageBtn[index].myRect.anchoredPosition.x, stageBtn[index].myRect.anchoredPosition.y - stageBtn[index].myRect.sizeDelta.y);

    }

    public void ResetPos()
    {
        DownBtns.gameObject.SetActive(false);
        for (int i = 0; i < stageBtn.Length; i++)
            stageBtn[i].ResetPosition();
    }

}
