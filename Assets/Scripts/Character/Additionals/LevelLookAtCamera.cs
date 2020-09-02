using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLookAtCamera : MonoBehaviour
{
    public void lookAt()
    {
        Vector3 distanse = (this.transform.position - Camera.main.transform.position).normalized;
        Quaternion q = Quaternion.LookRotation(distanse);

        this.transform.rotation = q;
    }
}
