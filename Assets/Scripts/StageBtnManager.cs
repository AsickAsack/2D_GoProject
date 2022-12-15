using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageBtnManager : MonoBehaviour
{
    public Canvas StageSelectCanvas;
    public Text StageText;
    public StageBtn[] stageBtn;



    public void SetAllBtns_Main(int index)
    {
        StageManager.instance.CurStage.x = index;
        SetBtnRoutine((int)StageManager.instance.CurStage.x);
    }

    public void SetAllBtns_Select(float index)
    {
        StageManager.instance.CurStage.x = StageManager.instance.CurStage.x + index;
        SetBtnRoutine((int)StageManager.instance.CurStage.x);
    }

    public void SetBtnRoutine(int CurStage)
    {
        if (CurStage <= 0 || CurStage > StageManager.instance.stage.Length)
        {
            StageManager.instance.CurStage.x = CurStage <= 0 ? 1 : StageManager.instance.stage.Length;
            return;
            
        }

        for (int i = 0; i < stageBtn.Length; i++)
            stageBtn[i].SetStageBtn();
    }

    
}
