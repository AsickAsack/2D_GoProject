using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameArrow : MonoBehaviour
{

    public Animator MyAnim;
    public GameObject MyArrow;
    int[] SetPower;
    int divide;

    private void Awake()
    {
        SetPower = new int[5];
        
    }

    public void InitArrow(int min,int max)
    {
        divide = (max - min) / SetPower.Length;

        for (int i = 0; i < SetPower.Length; i++)
        {
            SetPower[i] = min + divide * (i + 1);
        }
    }

    public void SetArrow(int curPower)
    {
        if (curPower <= SetPower[0])
        {
            MyAnim.SetTrigger("Exit");
            return;
        }
        else if(curPower <= SetPower[1])
        {
            MyAnim.SetInteger("Power", 0);
        }
        else if (curPower <= SetPower[2])
        {
            MyAnim.SetInteger("Power", 1);
        }
        else if (curPower <= SetPower[3])
        {
            MyAnim.SetInteger("Power", 2);
        }
        else
        {
            MyAnim.SetInteger("Power", 3);
        }

        MyAnim.SetTrigger("Change");

    }

    public void EndArrow()
    {
        MyAnim.SetTrigger("Exit");
        this.gameObject.SetActive(false);
    }


}
