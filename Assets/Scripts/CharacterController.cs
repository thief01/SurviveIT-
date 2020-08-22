using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum AnimationValue
{
    drop,
    pickup,
    open,
    sitting,
    attack0,
    attack1,
    attack2,
    attack3
}

public class CharacterController : MonoBehaviour
{
    public string nickname = "Newbie! Kek.";

    public GameObject deadVFX;

    [HideInInspector] public NavMeshAgent nma;
    Transform target;
    Animator anim;
    void Start()
    {
        nma = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (nma.velocity != Vector3.zero)
        {
            anim.speed = 1f;
            anim.Play("Run");
            anim.SetInteger("attackType", 0);
            anim.SetBool("run", true);
        }
        else
        {
            anim.SetBool("run", false);
        }

        if(target != null)
        {
            nma.SetDestination(target.position);
        }
    }

    public void setTarget(Interactable i)
    {
        target = i.GetComponent<Transform>();
        i.OnFoccused(this.transform);
    }

    public void setTarget(Transform target, float range)
    {
        this.target = target;
        nma.stoppingDistance = range;
    }

    public void removeTarget()
    {
        if (target == null)
            return;
        Interactable i = target.GetComponent<Interactable>();
            
        if(i!=null)
        {
            i.OnDefocused();
        }
        target = null;
    }

    public void setDestination(Vector3 des)
    {
        removeTarget();
        nma.stoppingDistance = 0.2f;
        nma.SetDestination(des);
        
    }
    public void kill()
    {
        GameObject g = Instantiate(deadVFX);
        g.transform.position = this.transform.position + Vector3.up;
        Destroy(g, 3f);
        anim.speed = 1f;
        anim.SetBool("death", true);
    }

    public void attack(int i, float attackSpeed)
    {
        anim.speed = attackSpeed;
        anim.Play("Attack" + i.ToString());
    }

    public void playAnimation(AnimationValue av , float speed = 1f)
    {
        anim.speed = speed;
        switch(av)
        {
            case AnimationValue.sitting:
                anim.Play("sit");
                break;
            case AnimationValue.drop:
                anim.Play("drop");
                break;
            case AnimationValue.open:
                anim.Play("open");
                break;
            case AnimationValue.pickup:
                anim.Play("pickup");
                break;
        }
    }
}
