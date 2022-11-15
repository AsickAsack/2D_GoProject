using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrongCharacter : CharacterPlay
{


    public override bool ActiveSkill(Vector2 pos, Collision2D collision = null)
    {
        Instantiate(PlayManager.Instance.objectPool.GetActiveEffects(ActivePrefab_Index, pos));
        //충돌 시 밀어내는 힘 2배
        this.MyRigid.velocity *= this.character.Active_Figure;
        return true;
    }

    public override void PassiveSkill()
    {
        //주변 범위 안에서  충돌이 일어나면 2배의 힘으로 밀려나게함
    }
}
