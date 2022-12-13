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

    public void SetStageBtn()
    {
        //다시 세팅할때는 별 다 꺼야함
        SubStageText.text = $"{((int)StageManager.instance.CurStage.x).ToString()}-{MyIndex}";

        for(int i=0;i<StageManager.instance.stage[(int)StageManager.instance.CurStage.x].subStage[MyIndex].Stars;i++)
        {
            StarOBJ[i].SetActive(true);
        }

        //아직 클리어 하지 않았으면 버튼 상호작용 꺼야함
        //앞뒤 확인
    }

}
