﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public struct SimpleSlot
{
    public Image skillIcon;
    public Image skillColldown;
}

[System.Serializable]
public struct BarSlot
{
    public Image bar;
    public TextMeshProUGUI value;
}

public class UIController : MonoBehaviour
{
    public SimpleSlot[] skillSlots = new SimpleSlot[4];
    public BarSlot[] barSlots = new BarSlot[3];

    public CharacterClass mainCharacter;

    private void Start()
    {
        if(mainCharacter==null)
            Debug.LogError("Main character isn't definied. It may make problems..");

        for (int i = 0; i < 4; i++)
        {
            skillSlots[i].skillIcon = null; // no icon i'm not f***ing graphic
        }


    }

    public void Update()
    {

        barSlots[0].bar.fillAmount = mainCharacter.stats.healthPoints / mainCharacter.stats.healthPointsMAX;
        barSlots[1].bar.fillAmount =  mainCharacter.stats.manaPoints / mainCharacter.stats.manaPointsMAX;
        barSlots[2].bar.fillAmount = mainCharacter.experience / mainCharacter.needExperience;

        barSlots[0].value.text = mainCharacter.stats.healthPoints + " / " + mainCharacter.stats.healthPointsMAX;
        barSlots[1].value.text = mainCharacter.stats.manaPoints + " / " + mainCharacter.stats.manaPointsMAX;
        barSlots[2].value.text = (mainCharacter.experience / mainCharacter.needExperience * 100f).ToString() + " %";

        for(int i=0; i<4; i++) // too lazly :O
        {
            //skillSlots[i].skillColldown.fillAmount = mainCharacter.getCooldown(i);
        }
    }
}
