using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReBirth_Zone : Special_Zone
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.CompareTag("PlayerBall"))
        {
            //�� ȿ�� �� ĳ���� ��Ŀ
            PlayManager.Instance.ReBirthRoutine(collision.transform);

        }
    }


}
