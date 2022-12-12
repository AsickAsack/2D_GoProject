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
            FailPanel.gameObject.SetActive(true);
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
        StageManager.instance.stage[CurStage.x].subStage[CurStage.y].IsClear = true;

        Debug.Log(PlayManager.Instance.PlayerCount);


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

        MyAnim.SetInteger("Star", starCount);


        StartCoroutine(ResultTextRoutine());
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
