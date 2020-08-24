using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#region defs

public enum Parameter
{
    basicDamage,
    cooldownReduction,
    attackPower,
    attackDamage,
    magicResist,
    physicResist,
    health,
    maxHealth,
    mana,
    maxMana,
    moevementSpeed,
    attackSpeed
}

public enum TypeValue
{
    percent,
    _float
}

[System.Serializable]
public struct SkillModifier
{
    public Parameter statistic;
    public TypeValue typeValue;
    public float value;
}

[System.Serializable]
public enum CostType
{
    healthPoint,
    manaPoint
}

[System.Serializable]
public struct Cost
{
    public CostType costType;
    public TypeValue typeValue;
    public float value;
}

#endregion

public abstract class SkillParent : MonoBehaviour
{
    public Image icon;
    public string skillName;
    public string descritpion;
    public float cooldownTime=1f; // time needed to get again skill in seconds
    [HideInInspector] public float cooldown = 0f; // if 0 skill is possible to use
    [SerializeField] SkillModifier[] sm; // skills modifier everything will modify skill damage/heal by stats
    [SerializeField] Cost[] cost = new Cost[2]; // skills cost, now possible is healt or mana

    CharacterClass skillOwner;
    abstract public void use();

    // for futhure if i would need instant reset skill
    public void reset()
    {
        cooldown = -1f;
    }

    protected virtual bool takeResource()
    {
        foreach(Cost c in cost)
        {
            if(c.costType == CostType.healthPoint)
            {
                if(c.typeValue == TypeValue.percent)
                {
                    skillOwner.stats.healthPoints -= skillOwner.stats.healthPointsMAX * c.value;
                }
                else if(c.typeValue == TypeValue._float)
                {
                    skillOwner.stats.healthPoints -= c.value/100f;
                }

                if(skillOwner.stats.healthPoints < 10f)
                {
                    skillOwner.stats.healthPoints = 10f;
                }
            }
            if(c.costType == CostType.manaPoint)
            {
                float valueToGet = 0f;
                if (c.typeValue == TypeValue.percent)
                {
                    valueToGet = skillOwner.stats.manaPointsMAX * c.value/100f;
                }
                else if (c.typeValue == TypeValue._float)
                {
                    valueToGet = c.value;
                }

                if(skillOwner.stats.manaPoints < valueToGet)
                {
                    return false;
                }
                skillOwner.stats.manaPoints -= valueToGet;
            }
        }
        return true;
    }
}
