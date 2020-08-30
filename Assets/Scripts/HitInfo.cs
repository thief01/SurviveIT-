using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitInfo
{
    public float damage;
    public DamageType damageType;
    public CharacterClass owner;

    public HitInfo(float dmg,DamageType dmgT, CharacterClass own)
    {
        damage = dmg;
        damageType = dmgT;
        owner = own;
    }
}
