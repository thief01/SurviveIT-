using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeHit : SkillParent
{
    public GameObject hitPrefab;
    public Transform respawnPosition;

    public override void execute()
    {
        if (cooldown > 0)
            return; 
        
        cooldown = 1 / skillOwner.stats.attackSpeed;
        controllerOwner.playAnimation(AnimationValue.attack0 , skillOwner.stats.attackSpeed);
        Invoke("respawn", 0.3f/skillOwner.stats.attackSpeed);
    }

    void respawn()
    {
        GameObject g = Instantiate(hitPrefab);
        g.transform.position = respawnPosition.position;
        FollowingObject fg = g.GetComponent<FollowingObject>();
        fg.target = skillOwner.getTarget();
        HitInfo hi = new HitInfo(skillOwner, calculateDamage(), damageType);
        fg.hi = hi;
    }
}
