using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingObject : MonoBehaviour
{
    [HideInInspector] public HitInfo hi;

    [HideInInspector] public CharacterClass target;
    public float range = 1f;
    public float speed = 1f;

    private void Update()
    {
        if (target == null || !target.alive)
            Destroy(this.gameObject);
        
        if(Vector3.Distance(this.transform.position, target.transform.position + Vector3.up * 0.5f) < range)
        {
            hit();
            Destroy(this.gameObject);
        }
        Vector3 direction = (target.transform.position - this.transform.position + Vector3.up * 0.5f).normalized;

        Quaternion q = Quaternion.LookRotation(direction);
        this.transform.rotation = q;
        this.transform.position = Vector3.MoveTowards(this.transform.position, target.transform.position+Vector3.up*0.5f, speed*Time.deltaTime);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(255, 0, 255);
        Gizmos.DrawWireSphere(this.transform.position, range);
    }

    public virtual void hit()
    {
        target.GetDamage(hi);
    }
}
