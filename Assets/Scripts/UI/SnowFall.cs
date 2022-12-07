using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowFall : MonoBehaviour
{
    
    public Snow[] SnowOBJ;

    private void Awake()
    {
        for(int i=0;i<SnowOBJ.Length;i++)
            SnowOBJ[i].ResetPos = SnowOBJ[1].GetComponent<RectTransform>().anchoredPosition;
    }
}
