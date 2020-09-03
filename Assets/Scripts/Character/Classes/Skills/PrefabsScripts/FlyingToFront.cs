using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingToFront : MonoBehaviour
{
    public float speed;
    public Vector3 hitBox;
    public bool activeOnFirst=false;
    [HideInInspector] public HitInfo hi;

    public float lifeTime = 1f;

    List<Transform> hited;

    public virtual void cast(Collider[] colliders)
    {
        
        foreach (Collider c in colliders)
        {
            CharacterClass cc = c.GetComponent<CharacterClass>();
            if(cc != null && cc != hi.owner)
                cc.GetDamage(hi);
        }
        Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(Vector3.left * speed * Time.deltaTime);

        Collider[] colliders = Physics.OverlapBox(this.transform.position, hitBox,this.transform.rotation);
        if (activeOnFirst)
        {
            if (colliders != null)
            {
                cast(new Collider[] { colliders[0] });
            }
        }
        if(lifeTime<0)
        {
            cast(colliders);
        }
        lifeTime -= Time.deltaTime;
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(255, 0, 0);
        Gizmos.DrawWireCube(this.transform.position, hitBox);
    }
}
