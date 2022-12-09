using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameOption : MonoBehaviour
{
    public GameObject OptionPanel;
    public GameObject DontTouch_Panel;


    public void SetPanel(bool check)
    {
        OptionPanel.SetActive(check);
        DontTouch_Panel.SetActive(check);
    }


    public void Resume_Game()
    {
        SetPanel(false);
        Time.timeScale = 1;
    }

    public void Pause_Game()
    {
        SetPanel(true);
        Time.timeScale = 1;
    }

    //���� �����
    public void ReStartStage()
    {
        //Ƽ�� �ִ��� Ȯ���ؾ���

        StageManager.instance.CurCharacters.Clear();
        StageManager.instance.CurMonsters.Clear();
        PlayManager.Instance.OnBoardPlayer.Clear();

        SceneLoader.Instance.Loading_LoadScene(2);
    }

    //���� ȭ�� �̵�
    public void GoMain()
    {
        StageManager.instance.SelectCharacters.Clear();
        StageManager.instance.CurCharacters.Clear();
        StageManager.instance.CurMonsters.Clear();
        PlayManager.Instance.OnBoardPlayer.Clear();

        SceneLoader.Instance.Loading_LoadScene(0);


    }




}


