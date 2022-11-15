using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeesinCharacter : CharacterPlay
{
    
    public override bool ActiveSkill(Vector2 pos, Collision2D collision = null)
    {
        return true;
    }

    public override void PassiveSkill()
    {
      
    }


}
