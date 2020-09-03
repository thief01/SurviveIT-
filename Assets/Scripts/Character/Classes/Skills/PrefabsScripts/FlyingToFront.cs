using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class FlyingToFront : MonoBehaviour
{
    public float speed;
    public Vector3 hitBox;
    [HideInInspector] public HitInfo hi;

    public float lifeTime = 1f;

    public float damageRange=5f;

    public virtual void cast(Collider collider)
    {
        if(damageRange==0)
        {
            collider.GetComponent<CharacterClass>().GetDamage(hi);
        }

        Collider[] colliders = Physics.OverlapSphere(collider.transform.position, damageRange);
        foreach(Collider c in colliders)
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
           
        foreach(Collider c in colliders)
        {
            if(c.GetComponent<CharacterClass>() != null && c.transform != hi.owner.transform)
            {
                cast(c);
            }
        }
        if (lifeTime < 0)
        {
            Destroy(this.gameObject);
        }
        lifeTime -= Time.deltaTime;
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.matrix = this.transform.localToWorldMatrix;
        Gizmos.color = new Color(255, 0, 0);
        Gizmos.DrawWireCube(Vector3.zero, hitBox);

        Gizmos.matrix = Matrix4x4.identity;
        foreach(CharacterClass cc in FindObjectsOfType<CharacterClass>())
        {
            Gizmos.color = new Color(255, 255, 0);
            Gizmos.DrawWireSphere(cc.transform.position, damageRange);
        }
    }
}
