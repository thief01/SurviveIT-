using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    CharacterController charController;
    CharacterClass characterClass;

    public Vector3 offsetCamera;
    void Start()
    {
        charController = GetComponent<CharacterController>();
        characterClass = GetComponent<CharacterClass>();
    }

    Ray r;
    RaycastHit rh;

    // Update is called once per frame
    void Update()
    {
        r = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(r, out rh);


        if(Input.GetKeyDown(KeyCode.Q))
        {
            characterClass.attack(1);
        }
        if(Input.GetKeyDown(KeyCode.W))
        {
            characterClass.attack(2);
        }
        if(Input.GetKeyDown(KeyCode.E))
        {
            characterClass.attack(3);
        }
        if(Input.GetKeyDown(KeyCode.R))
        {
            characterClass.attack(4);
        }

        if(Input.GetMouseButtonDown(0))
        {
            r = Camera.main.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(r, out rh))
            {
                if(rh.collider.GetComponent<CharacterClass>() != null)
                {
                    characterClass.setTarget(rh.collider.transform);
                    return;
                }
                Interactable i = rh.collider.GetComponent<Interactable>();
                if(i != null)
                {
                    charController.setTarget(i);
                    return;
                }

                charController.setDestination(rh.point);
                characterClass.removeTarget();
            }
        }

        Camera.main.transform.position = this.transform.position + offsetCamera;
    }
}
