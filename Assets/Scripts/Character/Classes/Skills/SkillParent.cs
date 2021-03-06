﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

#region defs

public enum Parameter
{
    basicDamage,
    attackPower,
    attackDamage,
    magicResist,
    physicResist,
    health,
    maxHealth,
    mana,
    maxMana,
    attackSpeed
}

public enum TypeValue
{
    percent,
    _const
}

[System.Serializable]
public struct SkillModifier
{
    public Parameter statistic;
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

public enum DamageType
{
    magicDamage,
    physicDamage,
    trueDamage,
}

#endregion

public abstract class SkillParent : MonoBehaviour
{
    public Sprite icon;
    public string skillName;
    public string descritpion;
    public float cooldownTime=1f; // time needed to get again skill in seconds
    [HideInInspector] public float cooldown = 0f; // if 0 skill is possible to use
    [SerializeField] SkillModifier[] skillModifier; // skills modifier everything will modify skill damage/heal by stats
    [SerializeField] Cost[] cost = new Cost[2]; // skills cost, possible is healt or mana
    public DamageType damageType; // Means 200% AP can make Physic damage, or true damage

    [HideInInspector] public string damageDescriptionForUI = "";

    protected CharacterClass skillOwner;
    protected CharacterController controllerOwner;
    abstract public void execute();

    private void Start()
    {
        skillOwner = this.gameObject.GetComponent<CharacterClass>();
        controllerOwner = this.gameObject.GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (cooldown > 0)
            cooldown -= Time.deltaTime;
    }

    // for futhure if i would need instant reset skill
    public void reset()
    {
        cooldown = -1f;
    }

    protected virtual bool takeResource()
    {
        float substractHP = 0;
        float substractMP = 0;
        foreach(Cost c in cost)
        {
            if(c.costType == CostType.healthPoint)
            {
                if(c.typeValue == TypeValue.percent)
                {
                    substractHP = skillOwner.stats.healthPointsMAX * c.value;
                }
                else if(c.typeValue == TypeValue._const)
                {
                    substractHP = c.value;
                }

                if(skillOwner.stats.healthPoints < 10f)
                {
                    substractHP = skillOwner.stats.healthPoints - 10;
                    skillOwner.stats.healthPoints = 10f;
                }
            }
            if(c.costType == CostType.manaPoint)
            {
                if (c.typeValue == TypeValue.percent)
                {
                    substractMP = skillOwner.stats.manaPointsMAX * c.value;
                }
                else if (c.typeValue == TypeValue._const)
                {
                    substractMP = c.value;
                }

                if(skillOwner.stats.manaPoints < substractMP)
                {
                    return false;
                }
            }
        }
        skillOwner.stats.manaPoints -= substractMP;
        skillOwner.stats.healthPoints -= substractHP;
        return true;
    }

    protected virtual float calculateDamage()
    {
        float damage=0;
        damageDescriptionForUI = "";
        for(int i=0; i<skillModifier.Length; i++)
        {
            switch(skillModifier[i].statistic)
            {
                case Parameter.attackDamage:
                    damage += skillOwner.stats.attackDamage * skillModifier[i].value;
                    damageDescriptionForUI += "<color=\"orange\">" + ((int)skillOwner.stats.attackDamage * skillModifier[i].value).ToString();
                    break;
                case Parameter.attackPower:
                    damage += skillOwner.stats.attackPower * skillModifier[i].value;
                    damageDescriptionForUI += "<color=\"green\">" + ((int)skillOwner.stats.attackPower * skillModifier[i].value).ToString();
                    break;
                case Parameter.attackSpeed:
                    damage += skillOwner.stats.attackSpeed * skillModifier[i].value;
                    damageDescriptionForUI += "<color=\"white\">" + (skillOwner.stats.attackSpeed * skillModifier[i].value).ToString();
                    break;
                case Parameter.basicDamage:
                    damage += skillModifier[i].value;
                    damageDescriptionForUI += "<color=\"white\">" + ((int)skillModifier[i].value).ToString();
                    break;
                case Parameter.health:
                    damage += skillOwner.stats.healthPoints * skillModifier[i].value;
                    damageDescriptionForUI += "<color=\"red\">" + ((int)skillOwner.stats.healthPoints * skillModifier[i].value).ToString();
                    break;
                case Parameter.magicResist:
                    damage += skillOwner.stats.magicResist * skillModifier[i].value;
                    damageDescriptionForUI += "<color=#FF00FF>" + ((int)skillOwner.stats.magicResist * skillModifier[i].value).ToString();
                    break;
                case Parameter.physicResist:
                    damage += skillOwner.stats.physicsResist * skillModifier[i].value;
                    damageDescriptionForUI += "<color=\"yellow\">" + ((int)skillOwner.stats.magicResist * skillModifier[i].value).ToString();
                    break;
                case Parameter.mana:
                    damage += skillOwner.stats.manaPoints * skillModifier[i].value;
                    damageDescriptionForUI += "<color=\"blue\">" + ((int)skillOwner.stats.manaPoints * skillModifier[i].value).ToString();
                    break;
                case Parameter.maxHealth:
                    damage += skillOwner.stats.healthPointsMAX * skillModifier[i].value;
                    damageDescriptionForUI += "<color=\"red\">" + ((int)skillOwner.stats.healthPointsMAX * skillModifier[i].value).ToString();
                    break;
                case Parameter.maxMana:
                    damage += skillOwner.stats.manaPointsMAX * skillModifier[i].value;
                    damageDescriptionForUI += "<color=\"blue\">" + ((int)skillOwner.stats.manaPointsMAX * skillModifier[i].value).ToString();
                    break;
            }
            damageDescriptionForUI += " ";
        }
        return damage;
    }

    // gets for UI

    public string getCostDescription()
    {
        string description="";
        for(int i=0; i<cost.Length; i++)
        {
            switch(cost[i].costType)
            {
                case CostType.healthPoint:
                    switch(cost[i].typeValue)
                    {
                        case TypeValue.percent:
                            description += "<color=\"red\">" + ((int)skillOwner.stats.healthPointsMAX * cost[i].value).ToString();
                            break;
                        case TypeValue._const:
                            description += "<color=\"red\">" + cost[i].value.ToString();
                            break;
                    }
                    break;
                case CostType.manaPoint:
                    switch (cost[i].typeValue)
                    {
                        case TypeValue.percent:
                            description += "<color=\"blue\">" + ((int)skillOwner.stats.manaPointsMAX * cost[i].value).ToString();
                            break;
                        case TypeValue._const:
                            description += "<color=\"blue\">" + cost[i].value.ToString();
                            break;
                    }
                    break;
            }
            description += " ";
        }
        return description;
    }

    public string getDamageDescription()
    {
        calculateDamage();
        return damageDescriptionForUI;
    }

    public string getCooldownDescription()
    {
        return cooldownTime.ToString();
    }
}
