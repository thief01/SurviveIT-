using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitInfo
{
    public float damage;
    public DamageType damageType;
    public CharacterClass owner;

    public HitInfo(CharacterClass owner, float damage, DamageType dt)
    {
        this.damage = damage;
        this.owner = owner;
        damageType = dt;
    }
}
