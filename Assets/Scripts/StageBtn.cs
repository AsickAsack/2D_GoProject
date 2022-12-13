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
        //�ٽ� �����Ҷ��� �� �� ������
        SubStageText.text = $"{((int)StageManager.instance.CurStage.x).ToString()}-{MyIndex}";

        for(int i=0;i<StageManager.instance.stage[(int)StageManager.instance.CurStage.x].subStage[MyIndex].Stars;i++)
        {
            StarOBJ[i].SetActive(true);
        }

        //���� Ŭ���� ���� �ʾ����� ��ư ��ȣ�ۿ� ������
        //�յ� Ȯ��
    }

}
