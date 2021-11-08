using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTransform : MonoBehaviour
{

    public Transform TransformToFollow;
    private void Update()
    {
        transform.position = TransformToFollow.position;
    }


}

