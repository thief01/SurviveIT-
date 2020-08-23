using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ControllerStatusOverCharacter : MonoBehaviour
{
    public TextMeshPro level;
    new public TextMeshPro name;
    public Transform hpBar;
    public CharacterClass chc;

    void Update()
    {
        level.SetText(chc.level.ToString());
        hpBar.localScale = new Vector3(chc.stats.healthPoints / chc.stats.healthPointsMAX * 8.8074f, 1, 8.8074f);
        name.SetText(chc.name);
    }
}
