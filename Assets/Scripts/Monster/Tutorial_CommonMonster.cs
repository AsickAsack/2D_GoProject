using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_CommonMonster : MonsterPlay
{
    public GameObject Effect;
  
    public override void Initialize()
    {
      
    }

    public override void CountProcess()
    {
        if (!this.gameObject.activeSelf) return;

        Instantiate(Effect, this.transform.position, Quaternion.identity);
        TutorialPlaymanager.Instance.EnemyCount--;
        this.gameObject.SetActive(false);
        TutorialPlaymanager.Instance.CurMultiKill++;

    }

}
