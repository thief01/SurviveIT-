using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HitLogController : MonoBehaviour
{
    public CharacterClass chc;
    public TextMeshPro sketch; 
    // Start is called before the first frame update
    void Start()
    {
        chc.GetComponentInParent<CharacterClass>();
        chc.dmg += respawnNew;
    }

    void respawnNew(float value)
    {
        TextMeshPro g =  Instantiate(sketch, this.transform);
        g.gameObject.SetActive(true);
        g.text = ((int)value).ToString();
        Destroy(g, 0.5f);
    }
}
