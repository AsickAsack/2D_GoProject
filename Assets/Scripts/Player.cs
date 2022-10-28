using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    Vector3 StartDir = Vector2.zero;
    Vector3 EndDir = Vector2.zero;
    Vector3 CurDir = Vector2.zero;
    Vector3 TargetPos = Vector3.zero;
    bool IsAttack;

    public RectTransform Arrow;

    

    private void Update()
    {
        

        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = new Ray();
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray,out RaycastHit hit, float.MaxValue, 1<<LayerMask.NameToLayer("Player")))
            {
                IsAttack = true;
                StartDir = Input.mousePosition;
            }

        }

        if(Input.GetMouseButton(0))
        {
            //Arrow.LookAt(Input.mousePosition);

            //화면 설정
            if(!IsAttack)
            {
                float X = -Input.GetAxis("Mouse X") * Time.deltaTime * 100.0f;
                float Y = -Input.GetAxis("Mouse Y") * Time.deltaTime * 100.0f;

                Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position,
                    new Vector3(Camera.main.transform.position.x + X, Camera.main.transform.position.y, Camera.main.transform.position.z + Y),Time.deltaTime *20.0f);
            }
            else
            {

                Ray ray = new Ray();
                ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, 1 << LayerMask.NameToLayer("RayCaster")))
                {
                    IsAttack = true;
                    CurDir = hit.point;

                }
                //화살 설정
                Debug.DrawRay(this.transform.position, -((CurDir - this.transform.position).normalized), Color.red);
                TargetPos = -((CurDir - this.transform.position).normalized);
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (IsAttack)
            {
                EndDir = Input.mousePosition;
                GoForward(TargetPos, Vector3.Distance(StartDir,EndDir)*Time.deltaTime*5.0f);
                IsAttack = false;
            }
            
        }

        Ray ray1 = new Ray();
        ray1.origin = this.transform.position;
        ray1.direction = this.transform.forward;
        if (Physics.SphereCast(ray1, 0.5f, out RaycastHit hit1, 0.1f,1<<LayerMask.NameToLayer("Enemy")))
        {
            Vector3 temp = new Vector3(hit1.point.x, 0, hit1.point.z);
            hit1.transform.GetComponent<Rigidbody>().AddForce((hit1.transform.position - this.transform.position).normalized * 10.0f);
            this.GetComponent<Rigidbody>().AddForce(new Vector3(hit1.point.z,0.0f,-hit1.point.x)-this.transform.position* 5.0f);
            Debug.Log("맞음");
        }
    }

    private void OnDrawGizmos()
    {
        Ray ray = new Ray();
        ray.origin = this.transform.position;
        ray.direction = this.transform.forward;
        Gizmos.color = Color.red;

        if (Physics.SphereCast(ray, 0.5f, out RaycastHit hit, 0.1f))
        {

            Gizmos.DrawWireSphere(this.transform.position + this.transform.forward * hit.distance, 0.5f);
        }
        else
        {
            Gizmos.DrawLine(this.transform.position, this.transform.position + this.transform.forward * hit.distance);
        }
    }

    public void GoForward(Vector3 dir,float Power)
    {
        this.GetComponent<Rigidbody>().AddForce(dir* Power,ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.CompareTag("Enemy"))
        {
            

            
            
            
        }
    }

}
