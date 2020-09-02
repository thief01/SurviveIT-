using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingToFront : MonoBehaviour
{
    public float speed;
    public Vector3 hitBox;
    public bool activeOnFirst=false;
    [HideInInspector] public HitInfo hi;

    public virtual void cast(Collider[] colliders)
    {

    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(Vector3.forward * speed * Time.deltaTime);

        Collider[] colliders = Physics.OverlapBox(this.transform.position, hitBox);
        if (activeOnFirst)
        {
            if (colliders != null)
            {
                CharacterClass cc = colliders[0].gameObject.GetComponent<CharacterClass>();
                cc.GetDamage(hi);
            }
        }
        else
        {

        }
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(255, 0, 0);
        Gizmos.DrawWireCube(this.transform.position, hitBox);
    }
}
