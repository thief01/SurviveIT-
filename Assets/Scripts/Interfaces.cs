using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    void GetDamage(HitInfo hi);
    void GetHeal(float i);
}

public interface IKillable
{
    bool Kill();
}

public interface IInteractableCharacter
{
    void Pickup();
    void Drop();
    void OpenDoor();
    void SetSitting(bool active);
}
