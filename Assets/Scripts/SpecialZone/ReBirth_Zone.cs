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
            int index = StageManager.instance.CurCharacters.FindIndex(x => x.character == collision.transform.GetComponent<CharacterPlay>().character);
            PlayManager.Instance.objectPool.GetEffect(7, this.transform.position, Quaternion.identity);
            PlayManager.Instance.CharacterIcons[index].SetActive(true);
            

        }
    }


}
