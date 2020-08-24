using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowJump : SkillParent
{
    public float maxDashRange;
    public float dashSpeed;

    public GameObject arrow;

    CharacterController chc;
    CharacterClass cc;
    void Update()
    {
        cc = GetComponent<CharacterClass>();
        chc = GetComponent<CharacterController>();

        if(dashIsActive && actualyDashDistance < maxDashRange)
        {
            chc.nma.Move(direction * dashSpeed * Time.deltaTime);
            actualyDashDistance += Vector3.Distance(Vector3.zero, direction)*dashSpeed*Time.deltaTime;
        }
        if(dashIsActive == true && actualyDashDistance > maxDashRange)
        {
            dashIsActive = false;
        }
        if(cooldown>-1)
            cooldown -= Time.deltaTime;
    }

    bool dashIsActive=false;
    float actualyDashDistance = 0f;
    Vector3 direction;

    public override void use()
    {
        if(cooldown > 0)
        {
            return;
        }

        /*if (!takeResource())
        {
            Debug.Log("Not enough resource.");
            return;
        }*/


        Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit rh;

        if (!Physics.Raycast(r, out rh))
        {
            Debug.Log("Bad ray");
            return;
        }
        cooldown = cooldownTime;
        direction = (rh.point - this.transform.position).normalized;
        this.transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
        direction *= -1;

        actualyDashDistance = 0f;
        dashIsActive = true;

        /*GameObject g = Instantiate(arrow);
        g.transform.position = this.transform.position;
        g.transform.rotation = this.transform.rotation;


        // chc.playAnimation(dash);
        // respawn arrow
        // set direction arrow/dash
        throw new System.NotImplementedException();*/
    }
}
