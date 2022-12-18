using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartOBJ : MonoBehaviour
{
    private void Update()
    {
        if(PlayManager.Instance.CheckMove())
        {
            this.gameObject.SetActive(false);
        }
    }
}
