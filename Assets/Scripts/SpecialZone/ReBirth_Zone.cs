using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReBirth_Zone : Special_Zone
{
    bool IsRebirth = false;

    public override void Skill(Collider2D collision)
    {
        if (collision.transform.CompareTag("PlayerBall")&& !IsRebirth)
        {
            //링 효과 후 캐릭터 복커
            SoundManager.Instance.PlayEffect(16);
            PlayManager.Instance.ReBirthRoutine(collision.transform);
            IsRebirth = true;
        }
    }


}
