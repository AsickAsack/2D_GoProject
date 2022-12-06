using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartUIProcess : MonoBehaviour
{
    public GameObject LogoPanel;
    public GameObject GameStartPanel;
    public GameObject DownDbPanel;

    public TMPro.TMP_Text WaitDownDBText;
    string[] LoadingString;

    private void Awake()
    {
        LoadingString = new string[3] { "DB 다운로드중..", "DB 다운로드중...", "DB 다운로드중...." };
    }

    private void Start()
    {
        StartCoroutine(WaitPanel(2.1f));
    }

    IEnumerator WaitPanel(float time)
    {
        yield return new WaitForSeconds(time);
        LogoPanel.SetActive(false);
        GameStartPanel.SetActive(true);
        StartCoroutine(WaitDownDb());
    }

    IEnumerator WaitDownDb()
    {
        if(Application.internetReachability == NetworkReachability.NotReachable)
        {
            PopUpManager.Instance.OpenPopup(0, "알림", "인터넷 연결에 실패했습니다.\n게임을 종료합니다.", () => Application.Quit());
            yield break;
        }

        int temp = 0;

        while(PlayerDB.Instance.IsFirst)
        {
            WaitDownDBText.text = LoadingString[temp++ % 3];

            yield return new WaitForSeconds(0.5f);
        }

        DownDbPanel.SetActive(false);
    }







}
