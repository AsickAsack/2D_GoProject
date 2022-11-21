using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrainWashSkill : ConflictAndSKill
{

    public override void CheckSKill(GameState SkillState)
    {
        switch (SkillState)
        {

            case GameState.End:

                for(int i=0;i< StageManager.instance.CurMonsters.Count;i++)
                {
                    if(StageManager.instance.CurMonsters[i].gameObject.activeSelf)
                    {
                        StageManager.instance.CurMonsters[i].myRigid.AddForce((this.transform.position - StageManager.instance.CurMonsters[i].transform.position).normalized * 2.0f, ForceMode2D.Impulse);
                    }

                }

                break;
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("PlayerBall") || collision.gameObject.CompareTag("EnemyBall"))
        {
            ConflictProcess(collision, 0.0f);
        }
    }

}
