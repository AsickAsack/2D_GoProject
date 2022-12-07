using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SilenceMonster : MonsterPlay
{
    public GameObject SilenceMark;

    public override void Initialize()
    {
        IsSKill = true;
    }

   public void Skill(Transform tr)
    {
        if(tr.CompareTag("PlayerBall"))
        {
            if (!tr.GetComponent<CharacterSkill>().IsSilence)
            {
                tr.GetComponent<CharacterSkill>().IsSilence = true;
                GameObject obj = Instantiate(SilenceMark, tr.position,Quaternion.identity,tr);
                obj.transform.localScale = new Vector2(2.0f, 2.0f);

            }
        }
    }

    public override void GoForward(Vector2 Dir, float Power, Transform tr)
    {
        Skill(tr);
        MyRigid.AddForce(Dir * Power, ForceMode2D.Impulse);
    }

}
