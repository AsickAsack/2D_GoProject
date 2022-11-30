using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ice : MonoBehaviour
{
   public GameObject IceObj;
    public CircleCollider2D myCollider;
   public int IceEffectIndex;

    public void SetIce(GameObject IceObj)
    {
        this.IceObj = IceObj;
        //this.IceObj.GetComponent<CircleCollider2D>().enabled = false;
        this.transform.position = IceObj.transform.position;
        this.transform.SetParent(IceObj.transform);
        this.transform.localScale = new Vector2(0.07f, 0.07f);
        IceObj.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        Invoke("ActiveCollider", 0.1f);
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.CompareTag("PlayerBall")|| collision.transform.CompareTag("EnemyBall"))
        {
            this.gameObject.SetActive(false);
            IceObj.GetComponent<IDeathProcess>().Death(IceEffectIndex);
        }
    }

    public void ActiveCollider()
    {
        myCollider.enabled = true;
    }

}
