using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazyCharacter : CharacterPlay
{
    int Count = 0;
 
    public override bool ActiveSkill(Vector2 pos, Collision2D collision = null)
    {
        Count++;

        if (Count == 2)
        {
            Debug.Log("¾×Æ¼ºê!");
            Instantiate(PlayManager.Instance.objectPool.GetActiveEffects(ActivePrefab_Index, pos));
            MyRigid.velocity *= 0.2f;
            return true;
        }
        return true;
    }

    public override void PassiveSkill()
    {
       
    }
}
