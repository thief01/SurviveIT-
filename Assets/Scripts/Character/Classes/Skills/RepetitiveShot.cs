using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepetitiveShot : SkillParent
{
    public GameObject arrow;
    public Transform respawnPosition;
    public float arrows = 5;
    public float breakBetweenShots=0.3f;

    bool activated = false;
    float arrowsToShot=0;

    float timeAfterShot=0;
    private void Update()
    {
        if(activated)
        {
            if(timeAfterShot>breakBetweenShots)
            {
                if (arrowsToShot > 0)
                {
                    Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit rh;
                    if (!Physics.Raycast(r, out rh))
                        return;

                    Vector3 direction = (rh.point - this.transform.position).normalized;
                    direction.y = 0;
                    this.transform.rotation = Quaternion.LookRotation(direction, Vector2.up);

                    GameObject g = Instantiate(arrow);
                    g.transform.position = respawnPosition.position;
                    g.transform.rotation = this.transform.rotation * Quaternion.Euler(0, 90, 0);
                    FlyingToFront ftf = g.GetComponent<FlyingToFront>();
                    ftf.hi = new HitInfo(skillOwner, calculateDamage(), damageType);

                    timeAfterShot = 0;
                    arrowsToShot--;
                }
                else
                {
                    activated = false;
                }
            }
            timeAfterShot += Time.deltaTime;
        }

        if(cooldown>0)
           cooldown -= Time.deltaTime;
    }

    public override void execute()
    {
        if (takeResource() && cooldown > 0)
            return;
        cooldown = cooldownTime;
        arrowsToShot = arrows;
        activated = true;
    }
}
