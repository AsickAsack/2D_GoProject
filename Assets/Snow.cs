using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snow : MonoBehaviour
{
    public Vector2 ResetPos;
    public float Speed;
    RectTransform myRect;

    private void Awake()
    {
        myRect = this.transform.GetComponent<RectTransform>();
    }

    void Update()
    {
        if (Camera.main.ScreenToViewportPoint(this.transform.position).y < 0)
            myRect.anchoredPosition = ResetPos;

        this.transform.Translate(Vector2.down * Time.deltaTime * Speed);
    }
}
