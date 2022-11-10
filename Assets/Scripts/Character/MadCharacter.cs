using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MadCharacter : CharacterPlay
{
    public override void AcitveSkill()
    {

        PlayManager.Instance.ingameUI.OnActive();

        Collider2D[] coll = Physics2D.OverlapCircleAll(this.transform.position, 2.0f);
        GameObject obj = Instantiate(PlayManager.Instance.effectManager.EffectPrefaps[2], this.transform.position, Quaternion.identity);
        
        

        for (int i = 0; i < coll.Length; i++)
        {
            if ((coll[i].CompareTag("EnemyBall") || coll[i].CompareTag("PlayerBall")) && coll[i].transform != this.transform)
            {
                GameObject obj1 = Instantiate(PlayManager.Instance.effectManager.EffectPrefaps[1], coll[i].transform.position, Quaternion.identity);
                coll[i].GetComponent<DeathProcess>()?.Death();
                
            }
        }

        this.transform.GetComponent<Rigidbody2D>().velocity = new Vector2(1.0f, 1.0f);
    }
    
    public override void PassiveSkill()
    {
        AcitveSkill();
        Debug.Log("패시브야");
    }




}
