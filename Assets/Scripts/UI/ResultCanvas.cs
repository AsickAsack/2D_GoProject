using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultCanvas : MonoBehaviour
{
    Canvas myCanvas;
    public GameObject[] Stars;
    public TMPro.TMP_Text[] Star_Text;

    public GameObject ClearPanel;
    public GameObject FailPanel;

    public TMPro.TMP_Text[] ResultText;
    public GameObject VeryGoodGif;

    public GameObject ReStartBtn;
    public TMPro.TMP_Text TicketCountText;

    Animator MyAnim;
    public float PlusSpeed;

    int[] CheckPoint;
    int[] ResultCount;
    int[] ResultPoint;
    string[] ResultString;


    private void Awake()
    {
        MyAnim = this.GetComponent<Animator>();
        myCanvas = this.GetComponent<Canvas>();
        CheckPoint = new int[3];
        ResultCount = new int[3];
        ResultPoint = new int[3];
        ResultString = new string[3] {"���� ĳ����","��Ƽų","ų��Ʈ��"};

    }

    public void CheckGameResult(bool IsClear)
    {
        myCanvas.enabled = true;

        if (!IsClear)
        {
            SoundManager.Instance.PlayEffect(29);
            FailPanel.gameObject.SetActive(true);
            ReStartBtn.SetActive(true);
            TicketCountText.text = $"x {StageManager.instance.stage[(int)StageManager.instance.CurStage.x - 1].subStage[(int)StageManager.instance.CurStage.y - 1].NeedTicket}";
            return;
        }

        ResultCount[0] = PlayManager.Instance.PlayerCount;
        ResultCount[1] = PlayManager.Instance.MultiKill;
        ResultCount[2] = PlayManager.Instance.KillStreaks;

        int MyPoint = 0;

        for (int i=0; i < 3;i++)
        {
            ResultPoint[i] = ResultCount[i] * 100;
            MyPoint += ResultPoint[i];
        }

        //�������� ���ƻ�(���� ���� ������)
        int CalculatePoint = ((StageManager.instance.CurCharacters.Count * 100 * 2) + (PlayManager.Instance.MaxMonsterCount * 100)) / 3;

        for (int i = 0; i < CheckPoint.Length; i++)
        {
            CheckPoint[i] = CeilingIntHundred(CalculatePoint * (i + 1));            
            Star_Text[i].gameObject.SetActive(true);

            if (i > 0)
                Star_Text[i].text = CheckPoint[i-1].ToString("N0");
            
        }
        


        var CurStage = (x: (int)StageManager.instance.CurStage.x-1, y: (int)StageManager.instance.CurStage.y-1);

        
        //�� ���������� ���� �������� isActive ��Ű��
        if(StageManager.instance.stage[CurStage.x].subStage.Length <= CurStage.y+1)
        {
            //���� ���������� ���ٸ�?
            if (StageManager.instance.stage.Length <= CurStage.x + 1)
                return;
            else
                PlayerDB.Instance.playerdata.MyStageData[CurStage.x + 1].IsAcitve[0] = true;
                //StageManager.instance.stage[CurStage.x+1].subStage[0].IsActive = true;
        }
        else
            PlayerDB.Instance.playerdata.MyStageData[CurStage.x].IsAcitve[CurStage.y + 1] = true;
        //StageManager.instance.stage[CurStage.x].subStage[CurStage.y+1].IsActive = true;
        

        int starCount = 0;

        for(int i=0;i<CheckPoint.Length;i++)
        {
            if(MyPoint < CheckPoint[i])
            {
                starCount = i + 1;
                break;
            }
            starCount = 3;
        }

        if (PlayerDB.Instance.playerdata.MyStageData[CurStage.x].StageStar[CurStage.y] < starCount)
            PlayerDB.Instance.playerdata.MyStageData[CurStage.x].StageStar[CurStage.y] = starCount;
        /*
        //���� ���� ���� �̹� ���������� ���ٸ�
        if (StageManager.instance.stage[CurStage.x].subStage[CurStage.y].Stars < starCount)
            StageManager.instance.stage[CurStage.x].subStage[CurStage.y].Stars = starCount;
        */
        //MyAnim.SetInteger("Star", starCount);

        for(int i=0;i<starCount;i++)
        {
            Stars[i].SetActive(true);
        }

        SoundManager.Instance.PlayEffect(29 + starCount);

        StartCoroutine(ResultTextRoutine());

        //��� �ְ� ����
        PlayerDB.Instance.Gold += StageManager.instance.stage[CurStage.x].subStage[CurStage.y].RewardGold;
        PlayerDB.Instance.SaveData();
    }

    IEnumerator ResultTextRoutine()
    {
        ClearPanel.SetActive(true);

        for (int i = 0; i < 3; i++)
        {
            float temp = 0;
            ResultText[i].gameObject.SetActive(true);

            while ((int)temp < ResultPoint[i])
            {
                temp += Time.deltaTime * PlusSpeed;
                ResultText[i].text = $"{ResultString[i]} x {ResultCount[i]}   :   {((int)temp).ToString()} ��";
                
                yield return null;
            }

            ResultText[i].text = $"{ResultString[i]} x {ResultCount[i]}   :   {ResultPoint[i].ToString()} ��";
            yield return null;
        }

        ResultText[3].gameObject.SetActive(true);
        ResultText[3].text = $"�� ����Ʈ {ResultPoint[0]+ ResultPoint[1]+ ResultPoint[2]} ��!";
        VeryGoodGif.gameObject.SetActive(true);
        yield return null;

    }

    public int CeilingIntHundred(int num)
    {
        int temp = num % 100;
        /*
        if (temp < 50)
            return num - temp;
        else
        */
            return num + (100 - temp);
            

    }

}
