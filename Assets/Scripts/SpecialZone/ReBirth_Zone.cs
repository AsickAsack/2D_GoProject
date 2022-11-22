using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReBirth_Zone : Special_Zone
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.CompareTag("PlayerBall"))
        {
            //링 효과 후 캐릭터 복커
            PlayManager.Instance.ReBirthRoutine(collision.transform);

        }
    }


}
