using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReBirth_Zone : Special_Zone
{
    public override void Skill(Collider2D collision)
    {
        if (collision.transform.CompareTag("PlayerBall"))
        {
            //�� ȿ�� �� ĳ���� ��Ŀ
            SoundManager.Instance.PlayEffect(16);
            PlayManager.Instance.ReBirthRoutine(collision.transform);
        }
    }


}
