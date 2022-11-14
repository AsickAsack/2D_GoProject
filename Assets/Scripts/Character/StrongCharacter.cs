using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrongCharacter : CharacterPlay
{

    public override void AcitveSkill()
    {   //충돌 시 밀어내는 힘 2배, 액티브 켰는지 확인해야함
        if (PlayManager.Instance.IsActive)
        {
            Debug.Log("전" + this.MyRigid.velocity);
            this.MyRigid.velocity *= 2;
            Debug.Log("후" + this.MyRigid.velocity);
        }
    }

    public override void PassiveSkill()
    {
        //주변 범위 안에서  충돌이 일어나면 2배의 힘으로 밀려나게함
    }
}
