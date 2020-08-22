using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillController : MonoBehaviour
{
    CharacterClass chClass;
    CharacterController chController;
    public Skill[] skillsList = new Skill[4];
    void Start()
    {
        chClass = this.gameObject.GetComponent<CharacterClass>();
        chController = this.gameObject.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void useSkill(int i)
    {
        if (i > skillsList.Length)
        {
            Debug.LogWarning("Value outside the list.");
        }
        else
            i = 0;
            //skillsList[i].use();
    }
}
