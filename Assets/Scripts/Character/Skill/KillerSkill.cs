using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillerSkill : ConfiltAndSKill, ComparePassive
{
    public int PassivePriority {get; set;}
    public bool IsPassive { get; set; } = false;

    int MurDerCount = 0;


    public bool GetPassivePriority(ComparePassive other)
    {
        if (PassivePriority > other.PassivePriority)
            return true;
        else
            return false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.CompareTag("PlayerBall"))
        {
            if (OnBoard)
            {
                IsPassive = true;
                ComparePassive temp = collision.transform.GetComponent<ComparePassive>();

                //상대가 패시브가 있다면
                if (temp != null)
                {
                    //상대 패시브가 실행 조건이 아니라면
                    if(!temp.IsPassive)
                    {
                        //실행
                        MurderSkill(collision);
                    }
                    //상대 보다 내가 우선순위가 더 높다면
                    else if(temp.GetPassivePriority(this))
                    {
                        //실행
                        MurderSkill(collision);
                    }
                }
                //상대가 패시브가 없다면
                else
                {
                    if (MurDerCount > 2)
                    {
                        //밀려야지 
                    }
                    else
                    {
                        //실행
                        MurderSkill(collision);
                    }

                }
            }
            else
            {
                // CurPlayher라면 밀려야지
            }
        }
    }


    public void MurderSkill(Collision2D Other)
    {
        MurDerCount++;
        PlayManager.Instance.objectPool.GetActiveEffects(Skill_index, Other.transform.position);
        Other.transform.GetComponent<DeathProcess>()?.Death();
        IsPassive = false;
    }

}
