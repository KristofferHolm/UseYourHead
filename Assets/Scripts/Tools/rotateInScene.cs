using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateInScene : MonoBehaviour
{
    public bool lockPos, rotate;
    public Vector3 Rotate;
    public Quaternion StartRot;
    private void OnValidate()
    {
        if (lockPos)
        {
            StartRot = transform.rotation;
            lockPos = false;
        }
        if (rotate)
        {
            transform.rotation = StartRot * Quaternion.Euler(Rotate);
        }
    }
}
