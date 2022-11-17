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

                //��밡 �нú갡 �ִٸ�
                if (temp != null)
                {
                    //��� �нú갡 ���� ������ �ƴ϶��
                    if(!temp.IsPassive)
                    {
                        //����
                        MurderSkill(collision);
                    }
                    //��� ���� ���� �켱������ �� ���ٸ�
                    else if(temp.GetPassivePriority(this))
                    {
                        //����
                        MurderSkill(collision);
                    }
                }
                //��밡 �нú갡 ���ٸ�
                else
                {
                    if (MurDerCount > 2)
                    {
                        //�з����� 
                    }
                    else
                    {
                        //����
                        MurderSkill(collision);
                    }

                }
            }
            else
            {
                // CurPlayher��� �з�����
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
