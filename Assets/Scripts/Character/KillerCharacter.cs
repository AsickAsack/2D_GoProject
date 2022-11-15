using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillerCharacter : CharacterPlay
{

    public override void ChangeONBorad()
    {
        if (this != null)
        {
            PlayManager.Instance.OnBoardPlayer.Add(this);
            PassiveRangeObj.SetActive(true);
            OnBoard = true;
            IsConfilct = true;
        }
        
    }

    public override bool ActiveSkill(Vector2 pos, Collision2D collision = null)
    {
        IsConfilct = false;
        collision.gameObject.GetComponent<DeathProcess>()?.Death();
        Instantiate(PlayManager.Instance.objectPool.GetActiveEffects(ActivePrefab_Index, pos));
        this.MyRigid.AddForce(MyRigid.velocity * this.character.Active_Figure, ForceMode2D.Impulse);
        return true;
    }

    public override void PassiveSkill()
    {
        
    }


    

}
