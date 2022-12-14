using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageRain : MonoBehaviour
{
    RectTransform Myrect;
    Vector2 ResetPos;
    public float DropSpeed;


    private void Awake()
    {
        Myrect = this.GetComponent<RectTransform>();
        ResetPos = new Vector2(0, 1);
    }

    private void Update()
    {

        if(Camera.main.ScreenToViewportPoint(Myrect.anchoredPosition).y < -1.0f)
        {
            Myrect.anchoredPosition = Camera.main.ViewportToScreenPoint(ResetPos);
        }

        Myrect.anchoredPosition += Vector2.down * Time.deltaTime * DropSpeed;
    }
}
