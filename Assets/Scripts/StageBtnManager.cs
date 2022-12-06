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

        SetButtonText(index);
        DownBtns.gameObject.SetActive(true);
        DownBtns.anchoredPosition = new Vector2(stageBtn[index].myRect.anchoredPosition.x, stageBtn[index].myRect.anchoredPosition.y - stageBtn[index].myRect.sizeDelta.y);

        SetBtnListeners(index);
    }

    // # 여기 코드 예쁘게 좀....;;;
    public void SetButtonText(int index)
    {
        for(int i = 0; i < StageDownBtns.Length; i++)
        {
            StageDownBtns[i].transform.GetChild(0).GetComponent<Text>().text = $"스테이지 {(index + 1)} - {(i + 1)}";
        }
    }


    public void ResetPos()
    {
        DownBtns.gameObject.SetActive(false);
        for (int i = 0; i < stageBtn.Length; i++)
            stageBtn[i].ResetPosition();
    }


    public void SetBtnListeners(int Stage)
    {

        for(int i = 0; i < StageDownBtns.Length; i++)
        {
            //그냥 i로 버튼 배열에 접근하면 참조가 일어남 (AddListener라서.....)
            int temp = i;
            StageDownBtns[temp].onClick.RemoveAllListeners();
            StageDownBtns[temp].onClick.AddListener(() =>
            {
                StageManager.instance.CurStage.x = Stage + 1;
                StageManager.instance.CurStage.y = temp + 1;

                CharacterSelectManager.Instance.OpenCharacterSelectUI(Stage+1,temp+1);

            });

        }
        

    }


    
}
