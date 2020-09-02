using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeHit : SkillParent
{
    public GameObject hitPrefab;
    public Transform respawnPosition;

    private void Update()
    {
        cooldownTime -= Time.deltaTime;
    }

    public override void execute()
    {
        if (cooldownTime > 0)
            return; 
        
        cooldownTime = 1 / skillOwner.stats.attackSpeed;
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
