using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageBtn : MonoBehaviour
{
    public int MyIndex;
    public TMPro.TMP_Text SubStageText;
    public GameObject[] StarOBJ;
    public Button MyBtn;

    public void OpenSelectUI()
    {
        StageManager.instance.CurStage.y = MyIndex + 1;
        CharacterSelectManager.Instance.OpenCharacterSelectUI((int)StageManager.instance.CurStage.x, (int)StageManager.instance.CurStage.y);
    }

    public void SetStageBtn()
    {
        SubStageText.text = $"{((int)StageManager.instance.CurStage.x).ToString()}-{MyIndex + 1}";
        InitializeBtn();

        if (!PlayerDB.Instance.playerdata.MyStageData[(int)StageManager.instance.CurStage.x - 1].IsAcitve[MyIndex])
        {

            return;
        }
        /*
        if (!StageManager.instance.stage[(int)StageManager.instance.CurStage.x-1].subStage[MyIndex].IsActive)
        {
            
            return;
        }
        */

        MyBtn.interactable = true;


        for (int i=0;i< PlayerDB.Instance.playerdata.MyStageData[(int)StageManager.instance.CurStage.x - 1].StageStar[MyIndex]; i++)
        {
            StarOBJ[i].SetActive(true);
        }

        //아직 클리어 하지 않았으면 버튼 상호작용 꺼야함
        //앞뒤 확인
    }

    public void InitializeBtn()
    {
        MyBtn.interactable = false;

        for (int i=0;i < StarOBJ.Length;i++)
            StarOBJ[i].SetActive(false);
    }
}
