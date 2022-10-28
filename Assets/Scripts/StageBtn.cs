using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageBtn : MonoBehaviour
{
    [SerializeField]
    private int stageIndex;
    [SerializeField]
    private Text StageText;
    public Sprite Icon;

    Vector2 OrgPos = Vector2.zero;

    private void Awake()
    {
        Initailize();
        OrgPos = this.transform.position;
    }

    public void Initailize()
    {
        StageText.text = "스테이지 " + (stageIndex+1);
        this.transform.GetChild(0).GetComponent<Image>().sprite = Icon;
    }

    public void GoDown()
    {
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - 500.0f);
    }
    public void ResetPosition()
    {
        this.transform.position = OrgPos;

    }

}
