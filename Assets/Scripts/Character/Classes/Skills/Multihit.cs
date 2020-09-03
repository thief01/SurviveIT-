using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Multihit : SkillParent
{
    public Transform respawnPoint;
    public GameObject smallArrow;
    public int arrows;
    public float angle;
    
    public override void execute()
    {
        if (cooldown > 0)
        {
            return;
        }

        if (!takeResource())
        {
            Debug.Log("Not enough resource.");
            return;
        }


        Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit rh;

        if (!Physics.Raycast(r, out rh))
        {
            Debug.Log("Bad ray");
            return;
        }
        Vector3 direction;
        cooldown = cooldownTime;
        direction = (rh.point - this.transform.position).normalized;
        direction.y = 0;
        this.transform.rotation = Quaternion.LookRotation(direction, Vector3.up);

        float angleDifference = angle / arrows;
        for(int i=0; i<arrows; i++)
        {
            GameObject g = Instantiate(smallArrow);
            g.transform.position = respawnPoint.position;
            g.transform.rotation = Quaternion.LookRotation(direction, Vector3.up)*Quaternion.Euler(0,-angle/2+angleDifference*i +90,0);
            FlyingToFront ftf = g.GetComponent<FlyingToFront>();
            ftf.hi = new HitInfo(skillOwner, calculateDamage(), damageType);
        }
    }
}
