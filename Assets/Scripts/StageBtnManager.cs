using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageBtnManager : MonoBehaviour
{
    public StageBtn[] stageBtn;
    public GameObject DownBtns;
    public float PaddingY;
    private int? CurIndex = null;

    private void Awake()
    {
        stageBtn = this.GetComponentsInChildren<StageBtn>();
    }

    public void BackMain()
    {
        CurIndex = null;
        ResetPos();
    }

    public void SetDownPos(int index)
    {
        if (CurIndex == index) 
        {
            BackMain();
            return; 
        }

        CurIndex = index;

        ResetPos();
        for (int i = index + 1; i < stageBtn.Length; i++)
            stageBtn[i].GoDown();

        DownBtns.gameObject.SetActive(true);
        DownBtns.transform.position = new Vector3(stageBtn[index].transform.position.x, stageBtn[index].transform.position.y - PaddingY);

    }

    public void ResetPos()
    {
        DownBtns.gameObject.SetActive(false);
        for (int i = 0; i < stageBtn.Length; i++)
            stageBtn[i].ResetPosition();
    }

}
