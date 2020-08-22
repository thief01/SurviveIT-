using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float radius;
    public Transform interactionPoint;

    Transform character;

    public void Update()
    {
        if(character != null && GetDistance() < radius)
        {
            Interact();
        }
    }

    float GetDistance()
    {
        float distance=0;
        if(interactionPoint!=null)
        {
            distance = Vector3.Distance(character.position, interactionPoint.position);
        }
        else
        {
            distance = Vector3.Distance(character.position, this.transform.position);
        }
        return distance;
    }

    public virtual void Interact()
    {
        Debug.Log("Player is interacting.");
    }

    public void OnFoccused(Transform player)
    {
        this.character = player;
    }

    public void OnDefocused()
    {
        character = null;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0, 255, 0);
        Gizmos.DrawWireSphere(interactionPoint.position, radius);
    }
}
