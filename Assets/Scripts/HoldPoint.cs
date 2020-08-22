using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldPoint : MonoBehaviour
{
    public Transform holdingPoint;

    public Vector3 point
    {
        get
        {
            return this.transform.position + holdingPoint.position;
        }
    }
}
