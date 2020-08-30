using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UISkillDescription : MonoBehaviour
{
    public TextMeshProUGUI name;
    public TextMeshProUGUI cost;
    public TextMeshProUGUI cd;
    public TextMeshProUGUI description;
    public TextMeshProUGUI damage;

    public void set(CharacterClass chc, int which)
    {
        name.text = chc.attacks[which].skillName;
        cost.text = chc.attacks[which].getCostDescription();
        cd.text = chc.attacks[which].getCooldownDescription();
        description.text = chc.attacks[which].descritpion;
        damage.text = chc.attacks[which].getDamageDescription();
    }
}
