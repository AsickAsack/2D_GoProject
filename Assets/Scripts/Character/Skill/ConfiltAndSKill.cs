using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Skill_Type
{
    Active,Passive
}

public enum Skill_Condition
{
    Ready,Shot,Confilct
}

public class ConfiltAndSKill : MonoBehaviour, IObserver
{
    public int Skill_index;
    public bool OnBoard;
    public GameState SkillState;
    public Skill_Type mySkill_Type;
    public Skill_Condition mySkill_Condition;

    public delegate void SkillDeligate();
    public SkillDeligate skillDeligate;



    public void ListenToSubeject(Skill_Type Skill_Type, Skill_Condition Skill_Condition)
    {
        if (this.mySkill_Type == Skill_Type && mySkill_Condition == Skill_Condition)
        {
            //스킬을 받아야 할때

            

            if (this.mySkill_Type == Skill_Type)//범위 검색
            {

            }
            else
                return; //범위에 없을시
        }
        else
            return;
    }

    public void ConflictProcess(Collision2D collision, float Power)
    {
        PlayManager.Instance.objectPool.GetPoolEffect(EffectName.StoneHit, collision.GetContact(0).point, Quaternion.identity);
        Rigidbody2D TempRigid = collision.gameObject.GetComponent<Rigidbody2D>();
        TempRigid.AddForce((collision.GetContact(0).point - (Vector2)this.transform.position).normalized * Power, ForceMode2D.Impulse);
    }
}
