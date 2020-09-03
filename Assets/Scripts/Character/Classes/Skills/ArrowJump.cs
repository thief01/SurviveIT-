using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ArrowJump : SkillParent
{
    public float maxDashRange;
    public float dashSpeed;

    public GameObject arrow;

    CharacterController chc;
    void Update()
    {
        if(dashIsActive && actualyDashDistance < maxDashRange)
        {
            controllerOwner.nma.Move(direction * dashSpeed * Time.deltaTime);
            controllerOwner.setDestination(this.transform.position);
            actualyDashDistance += Vector3.Distance(Vector3.zero, direction)*dashSpeed*Time.deltaTime;
        }
        if(dashIsActive == true && actualyDashDistance > maxDashRange)
        {
            dashIsActive = false;
        }
        if(cooldown>-1)
            cooldown -= Time.deltaTime;

        skillOwner.lvlLook.lookAt();
    }

    bool dashIsActive=false;
    float actualyDashDistance = 0f;
    Vector3 direction;

    public override void execute()
    {
        if(cooldown > 0)
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
        calculateDamage();
        cooldown = cooldownTime;
        direction = (rh.point - this.transform.position).normalized;
        direction.y = 0;
        this.transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
        
        direction *= -1;

        actualyDashDistance = 0f;
        dashIsActive = true;

        GameObject g = Instantiate(arrow);
        g.transform.position = this.transform.position;
        g.transform.rotation = Quaternion.LookRotation(direction, Vector3.up) *Quaternion.Euler(0,-90,0);
        FlyingToFront ftf = g.GetComponent<FlyingToFront>();
        ftf.hi = new HitInfo(skillOwner, calculateDamage(), damageType);

        controllerOwner.removeTarget();
    }

    private void OnDrawGizmosSelected()
    {
        
    }
}
