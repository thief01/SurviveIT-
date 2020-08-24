using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitInfo
{
    public float apDamage;
    public float adDamage;
    public CharacterClass owner;

    public HitInfo(float ad, float ap, CharacterClass own)
    {
        apDamage = ap;
        adDamage = ad;
        owner = own;
    }
}
