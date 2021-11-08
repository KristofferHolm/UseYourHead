using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapBackToBody : MonoBehaviour
{
    public Transform SnapBackTo;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(OVRInput.GetDown(OVRInput.Button.Start))
        {
            GetBackToHead();
        }

    }

    private void GetBackToHead()
    {
        transform.parent = SnapBackTo;
        transform.localPosition = Vector3.zero;
        GetComponent<Rigidbody>().isKinematic = true;
    }
}
