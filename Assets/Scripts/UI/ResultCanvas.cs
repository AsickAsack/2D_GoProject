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
        ResultString = new string[3] {"남은 캐릭터","멀티킬","킬스트릭"};

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

        //마지막에 ㄱㅖ산(몬스터 숫자 때문에)
        int CalculatePoint = ((StageManager.instance.CurCharacters.Count * 100 * 2) + (PlayManager.Instance.MaxMonsterCount * 100)) / 3;

        for (int i = 0; i < CheckPoint.Length; i++)
        {
            CheckPoint[i] = CeilingIntHundred(CalculatePoint * (i + 1));            
            Star_Text[i].gameObject.SetActive(true);

            if (i > 0)
                Star_Text[i].text = CheckPoint[i-1].ToString("N0");
            
        }
        


        var CurStage = (x: (int)StageManager.instance.CurStage.x-1, y: (int)StageManager.instance.CurStage.y-1);

        
        //깬 스테이지의 다음 스테이지 isActive 시키기
        if(StageManager.instance.stage[CurStage.x].subStage.Length <= CurStage.y+1)
        {
            //서브 스테이지가 없다면?
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
        //현재 받은 별이 이미 받은별보다 높다면
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

        //골드 주고 저장
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
                ResultText[i].text = $"{ResultString[i]} x {ResultCount[i]}   :   {((int)temp).ToString()} 점";
                
                yield return null;
            }

            ResultText[i].text = $"{ResultString[i]} x {ResultCount[i]}   :   {ResultPoint[i].ToString()} 점";
            yield return null;
        }

        ResultText[3].gameObject.SetActive(true);
        ResultText[3].text = $"총 포인트 {ResultPoint[0]+ ResultPoint[1]+ ResultPoint[2]} 점!";
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
