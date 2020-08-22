using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [HideInInspector]public HitInfo hi;
    [HideInInspector]public Transform target;
    public float speed;
    public float range;

    void Update()
    {
        if(target==null)
        {
            Destroy(this.gameObject);
            return;
        }
        this.transform.position = Vector3.MoveTowards(this.transform.position, target.position+Vector3.up*0.5f, speed * Time.deltaTime);
        Vector3 offset = (target.position+Vector3.up*0.5f-this.transform.position).normalized;
        Quaternion q = Quaternion.LookRotation(offset);
        this.transform.localRotation = q * Quaternion.Euler(0,90,0);
        if(Vector3.Distance(this.transform.position, target.position) < range)
        {
            target.GetComponent<IDamageable>().GetDamage(hi);
            Destroy(this.gameObject);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(255, 0, 0);
        Gizmos.DrawWireSphere(this.transform.position , range);
    }
}
