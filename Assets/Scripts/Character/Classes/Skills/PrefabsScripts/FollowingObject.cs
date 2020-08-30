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
        if(Vector3.Distance(this.transform.position, target.transform.position) < range)
        {
            hit();
        }
        Vector3 direction = (target.transform.position - this.transform.position).normalized;

        Quaternion q = Quaternion.LookRotation(direction);
        this.transform.rotation = q;
        this.transform.position = Vector3.MoveTowards(this.transform.position, target.transform.position, speed);
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
