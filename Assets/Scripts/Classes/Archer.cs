using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : CharacterClass
{
    public GameObject arrow;
    public Transform arrowRespawnPoint;

    #region Dash

    float dashSpeed = 45f;

    Vector3 dashDirection;

    float maxRangeDash=7f;
    float distanceInDash;
    bool dashActive;

    public void activeDash(Vector3 point)
    {
        this.gameObject.GetComponent<CharacterController>().nma.isStopped =true;
        
        dashActive = true;
        distanceInDash = 0;

        dashDirection = (point - this.transform.position).normalized*-1;
    }

    #endregion

    void Start()
    {

    }

    public override void skillAttack(int i, RaycastHit rh)
    {
        switch(i)
        {
            case 0:
                // animate basic attack and do damage on target
                break;
            case 1:
                activeDash(rh.point);
                break;
            case 2:
                // animate W skill
                break;
            case 3:
                // animate E skill
                break;
                
        }
    }

    public override bool tryAttack()
    {
        if (attackTime < 1 / stats.attackSpeed)
            return false;

        //IDamageable target = this.target.GetComponent<IDamageable>();
        HitInfo i = new HitInfo(stats.attackDamage,stats.attackPower, this);
        GameObject a = Instantiate(arrow);
        a.transform.position = arrowRespawnPoint.position;
        a.GetComponent<Arrow>().target = target.transform;
        a.GetComponent<Arrow>().hi = i;
        //target.GetDamage(i);
        this.gameObject.GetComponent<CharacterController>().attack(1, stats.attackSpeed);
        attackTime = 0;
        return true;
    }

    public override float getCooldown(int i)
    {
        return 0f;
    }

    void Update()
    {
        levelOverCharacter.text = level.ToString();
        hpBarOverCharacter.localScale = new Vector3(stats.healthPoints / stats.healthPointsMAX * 8.8074f, 1, 1);

        GetHeal(stats.healthRegen * Time.deltaTime);

        if (target!=null && Vector3.Distance(transform.position, target.GetComponent<Transform>().position) < stats.attackRange)
        {
            if(target.life)
                tryAttack(); 
            else
                target = null;

            Vector3 offset = (target.transform.position - this.transform.position).normalized;
            Quaternion q = Quaternion.LookRotation(offset, Vector3.up);
            this.transform.rotation = q;
        }
        updateTopValues();

        attackTime += Time.deltaTime;

        if (target != null && !target.life)
        {
            target = null;
        }

        if (dashActive && distanceInDash < maxRangeDash)
        {
            Vector3 offset = dashDirection * Time.deltaTime * dashSpeed;
            distanceInDash += Vector3.Distance(this.transform.position, offset)/100;
            this.gameObject.GetComponent<CharacterController>().nma.Move(offset);
            Debug.Log(distanceInDash);
        }
        else if(dashActive)
        {
            dashActive = false;
            this.gameObject.GetComponent<CharacterController>().nma.SetDestination(this.transform.position);
            this.gameObject.GetComponent<CharacterController>().nma.isStopped = false;
        }
    }

}
