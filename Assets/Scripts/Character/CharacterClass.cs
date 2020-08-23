using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;

[System.Serializable]
[RequireComponent(typeof(SkillController))]
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
    [HideInInspector] public bool life=true;

    protected float attackTime=0;

    public string className="Noob!";

    public Stats stats;
    [SerializeField] Stats scalingPerLevelStats;

    public float experience;
    public float needExperience;
    public int level;

    public GameObject mainWeapon; // control prefab weapon
    public GameObject subWeapon; // control prefab weapon

    protected CharacterClass target;

    private CharacterController myCHC;

    void Start()
    {
        myCHC = GetComponent<CharacterController>();
    }

    void Update()
    {
        GetHeal(stats.healthRegen * Time.deltaTime); // maybe should be every second

        if(target !=null && Vector3.Distance(transform.position, target.GetComponent<Transform>().position) < stats.attackRange) // leave it for now
        {
            if (target.life)
                tryAttack();
            else
                target = null;

            Vector3 offset = (target.transform.position - this.transform.position).normalized;
            Quaternion q = Quaternion.LookRotation(offset, Vector3.up);
            this.transform.rotation = q;
        } // ^

        // look whether new level is avaiable
    }

    public void addExp(float exp) // don't check lvl
    {
        experience += exp;
    }
    public virtual void skillAttack(int i, RaycastHit rh) // delete raycasthit
    {
        Debug.LogWarning("Noob hasn't skills... Learn it!");
    }

    public virtual float getCooldown(int i) // for UI? should be in UI
    {
        Debug.LogWarning("Noob hasn't skills.. He is just a noob");
        return 0f;
    }

    private void OnDrawGizmosSelected() // draw a attack range
    {
        Gizmos.color = new Color(255, 0, 0);
        Gizmos.DrawWireSphere(this.transform.position+Vector3.up*0.5f, stats.attackRange);
    }

    public virtual bool tryAttack() // do something with this
    {
        IDamageable target = this.target.GetComponent<IDamageable>();
        HitInfo i = new HitInfo();
        i.adDamage = stats.attackDamage;
        i.apDamage = stats.attackPower;
        i.owner = this;
        target.GetDamage(i);
        this.gameObject.GetComponent<CharacterController>().attack(1, stats.attackSpeed);
        attackTime = 0;
        return true;
    }

    void showDamage(float dmg) // make log with delegate
    {
        // Debug.Log("Damage "+ dmg); should be list but later xD maybe use delegate?
    }

    public void GetDamage(HitInfo hi) // 
    {
        if (life)
        {
            float damage = calculateRealDamage(hi);
            showDamage(damage);
            stats.healthPoints -= damage;
            if(Kill())
            {
                hi.owner.addExp(100 * level);
            }
        }
    }

    virtual protected float calculateRealDamage(HitInfo hi) // 
    {
        float damage;

        float ap = hi.apDamage-stats.magicResist;
        float ad = hi.adDamage-stats.physicsResist;

        if (ap < 0)
            ap = 0;
        if (ad < 0)
            ad = 0;

        damage = ap + ad;
        return damage;
    }

    public void GetHeal(float i)
    {
        stats.healthPoints += i;
        if (stats.healthPoints > stats.healthPointsMAX)
            stats.healthPoints = stats.healthPointsMAX;
    }

    public bool Kill() // maybe delete this func
    {
        if (stats.healthPoints > 0)
            return false;
        life = false;
        stats.healthPoints = 0;
        this.GetComponent<CharacterController>().kill();
        Destroy(this.gameObject, 1.5f);
        return true;
    }

    public void setTarget(Transform t) // kk
    {
        myCHC.setTarget(t, stats.attackRange);
        target = t.GetComponent<CharacterClass>();
    }

    public void removeTarget()
    {
        target = null;
        myCHC.removeTarget();
    }
}
