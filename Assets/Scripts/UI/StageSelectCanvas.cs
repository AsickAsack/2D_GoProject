using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSelectCanvas : MonoBehaviour
{
    public StageBtnManager _StageBtnManager;
    void Start()
    {
        if (StageManager.instance.NextSet)
        {
            this.GetComponent<Canvas>().enabled = true;
            _StageBtnManager.SetBtnRoutine((int)StageManager.instance.CurStage.x);
            _StageBtnManager.stageBtn[(int)StageManager.instance.CurStage.y - 1].OpenSelectUI();
            StageManager.instance.NextSet = false;

        }
            
    }


}
