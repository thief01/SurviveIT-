using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;

[System.Serializable]
public class Stats
{
    [Header("Health")]
    public float healthPointsMAX = 100;
    public float healthPoints = 100;
    public float healthRegen = 10;
    [Header("Mana")]
    public float manaPointsMAX = 100;
    public float manaPoints = 100;
    public float manaRegen = 10;
    [Header("Combat stats")]
    public float attackSpeed = 1;
    public float attackDamage=10;
    public float attackPower=10;
    public float magicResist=10;
    public float physicsResist=10;
    public float attackRange = 1;

    public static Stats operator+(Stats a, Stats b)
    {
        Stats temp = new Stats();
        temp.healthPointsMAX = a.healthPointsMAX + b.healthPointsMAX;
        temp.manaPointsMAX = a.manaPointsMAX + b.manaPointsMAX;
        temp.attackDamage = a.attackDamage + b.attackDamage;
        temp.attackPower = a.attackPower + b.attackPower;
        temp.magicResist = a.magicResist + b.magicResist;
        temp.physicsResist = a.physicsResist + b.physicsResist;
        return temp;
    }
}

public class CharacterClass : MonoBehaviour, IDamageable, IKillable
{
    public delegate void DamageChange(float value);
    public DamageChange dmg;

    [HideInInspector] public bool alive=true;

    protected float attackTime=0;

    public string className="Noob!";

    public Stats stats;
    [SerializeField] Stats scalingPerLevelStats;

    public float experience;
    public float needExperience;
    public int level;

    protected CharacterClass target;

    private CharacterController myCHC;

    /* attacks list
     * 0 - basic hit
     * 1 - some boost
     * 2 - dash
     * 3 - some skill
     * 4 - ulti
     */

    public SkillParent[] attacks = new SkillParent[5];

    // effect list?

    void Start()
    {
        myCHC = GetComponent<CharacterController>();
        lvlLook = GetComponentInChildren<LevelLookAtCamera>();
    }
    #region virtuals

    public LevelLookAtCamera lvlLook;
    void Update()
    {

        GetHeal(stats.healthRegen * Time.deltaTime); // maybe should be every second

        if(target !=null && Vector3.Distance(transform.position, target.GetComponent<Transform>().position) < stats.attackRange) // leave it for now
        {
            if (!target.alive)
                target = null;
            else
                attack(0);

            if (target != null && target.alive)
            {
                Vector3 offset = (target.transform.position - this.transform.position).normalized;
                Quaternion q = Quaternion.LookRotation(offset, Vector3.up);
                q = Quaternion.Slerp(this.transform.rotation, q, 0.3f);
                this.transform.rotation = q;
                
            }
        }

        lvlLook.lookAt();

        if (experience > needExperience)
        {
            level++;
            experience -= needExperience;
            stats += scalingPerLevelStats;
        }
    }

    public virtual void attack(int i)
    {
        attacks[i].execute();
    }

    public virtual void GetDamage(HitInfo hi)
    {
        if (alive)
        {
            float damage = calculateRealDamage(hi);
            stats.healthPoints -= damage;
            dmg.Invoke(damage);
            if(Kill())
            {
                hi.owner.addExp(100 * level);
            }
        }
    }

    protected virtual float calculateRealDamage(HitInfo hi)
    {
        float damage=0;
        switch(hi.damageType)
        {
            case DamageType.magicDamage:
                damage = hi.damage -= stats.physicsResist;
                break;
            case DamageType.physicDamage:
                damage = hi.damage - stats.magicResist;
                break;
            case DamageType.trueDamage:
                damage =  hi.damage;
                break;
        }
        if (damage < 0)
            damage = 0;
        return damage;
    }

    public virtual void GetHeal(float i)
    {
        stats.healthPoints += i;
        if (stats.healthPoints > stats.healthPointsMAX)
            stats.healthPoints = stats.healthPointsMAX;
    }
    #endregion

    public void addExp(float exp)
    {
        experience += exp;
    }

    public bool Kill() // maybe delete this func
    {
        if (stats.healthPoints > 0)
            return false;
        alive = false;
        stats.healthPoints = 0;
        this.GetComponent<CharacterController>().kill();
        Destroy(this.gameObject, 1.5f);
        return true;
    }

    public void setTarget(Transform t)
    {
        if (t == this.transform)
            return;
        myCHC.setTarget(t, stats.attackRange);
        target = t.GetComponent<CharacterClass>();
    }

    public void removeTarget()
    {
        target = null;
        myCHC.removeTarget();
    }

    public CharacterClass getTarget()
    {
        return target;
    }

    private void OnDrawGizmosSelected() // draw a attack range
    {
        Gizmos.color = new Color(255, 0, 0);
        Gizmos.DrawWireSphere(this.transform.position + Vector3.up * 0.5f, stats.attackRange);
    }
}
