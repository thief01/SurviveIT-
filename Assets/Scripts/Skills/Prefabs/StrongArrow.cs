using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrongArrow : MonoBehaviour
{
    public Vector3 colliderRange;
    public float speed=5f;
    public float maxDistanse;

    List<Transform> damaged;

    [HideInInspector] public HitInfo hitInfo;

    float distanse=0;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (distanse > maxDistanse)
            Destroy(this.gameObject);
        this.transform.Translate(Vector3.left * speed * Time.deltaTime);
        distanse += speed * Time.deltaTime;
        Collider[] c = Physics.OverlapBox(this.transform.position, colliderRange);


        foreach(Collider sc in c)
        {
            IDamageable id = sc.GetComponent<IDamageable>();
            if(id!= null && sc.gameObject.transform != hitInfo.owner.transform)
            {
                id.GetDamage(hitInfo);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(255, 0, 0);
        Gizmos.DrawWireCube(this.transform.position, Vector3.Scale(colliderRange,this.transform.localScale));
    }

    public void setRotation(Vector3 r)
    {
        Vector3 offset = (r - transform.position).normalized;
        transform.rotation = Quaternion.LookRotation(offset, Vector3.up);
    }
}
