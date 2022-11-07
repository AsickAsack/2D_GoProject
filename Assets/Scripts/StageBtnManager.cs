using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageBtnManager : MonoBehaviour
{
    public StageBtn[] stageBtn;
    public RectTransform DownBtns;
    public Button[] StageDownBtns;
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

        SetBtnListeners(index);
    }

    public void ResetPos()
    {
        DownBtns.gameObject.SetActive(false);
        for (int i = 0; i < stageBtn.Length; i++)
            stageBtn[i].ResetPosition();
    }

    public void setDebug(int index)
    {
        Debug.Log(index);
    }

    public void SetBtnListeners(int Stage)
    {

        for(int i = 0; i < StageDownBtns.Length; i++)
        {
            //그냥 i로 버튼 배열에 접근하면 참조가 일어남 
            int temp = i;
            StageDownBtns[i].onClick.AddListener(() =>
            {
                StageManager.instance.CurStage.x = Stage + 1;
                StageManager.instance.CurStage.y = temp + 1;

            });

        }
        

    }


    
}
