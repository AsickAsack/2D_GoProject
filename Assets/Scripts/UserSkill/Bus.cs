using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bus : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.CompareTag("PlayerBall")|| collision.transform.CompareTag("EnemyBall"))
        {
            Rigidbody2D temp = collision.gameObject.GetComponent<Rigidbody2D>();
            temp.AddForce(((Vector2)collision.transform.position-collision.GetContact(0).point).normalized * 10.0f,ForceMode2D.Impulse);

            
            if(collision.transform.GetComponent<MonsterPlay>() != null)
            {
                collision.transform.GetComponent<MonsterPlay>().IsUserSKill = true;
            }
            
        }
    }


    public void StartMove()
    {
        StartCoroutine(BusMove());
    }



    IEnumerator BusMove()
    {
        while(Camera.main.WorldToViewportPoint(this.transform.position).y < 1.0f)
        {
            this.transform.position = this.transform.position + Vector3.up * Time.deltaTime * 20.0f;
            yield return null;
        }

        this.gameObject.SetActive(false);
    }


}
