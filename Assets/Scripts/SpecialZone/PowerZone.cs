using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerZone : Special_Zone
{
    public override void Skill(Collider2D collision)
    {
        if (collision.transform.CompareTag("PlayerBall"))
        {
                SoundManager.Instance.PlayEffect(15);
                PlayManager.Instance.ingameUI.SetNotify(GameDB.Instance.GetNotifySpirte(NotifyIcon.PowerUp), $"최대 파워 업!");
                collision.gameObject.SetActive(false);
                PlayManager.Instance.objectPool.GetEffect(6, this.transform.position, Quaternion.identity);
                PlayManager.Instance.MultiplyPower += 0.1f;
        }
    }

}
