using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSim : MonoBehaviour
{
    public Vector3 WindDirection;
    public Vector3 Edge;
    List<GameObject> Clouds;

    private void Start()
    {
        Clouds = new List<GameObject>();
        for (int i = 0; i < transform.childCount; i++)
        {
            Clouds.Add(transform.GetChild(i).gameObject);
        }
    }
    private void Update()
    {
        foreach (var cloud in Clouds)
        {
            cloud.transform.position += WindDirection * DayNightCycle.Instance.ClockPerFrame;
            if (CheckEdge(cloud.transform.position))
                ResetCloud(cloud);        
        }
    }
    void ResetCloud(GameObject cloud)
    {
        var pos = cloud.transform.position;
        pos.x = Mathf.Clamp(-pos.x,-Edge.x,Edge.x);
        pos.z = Mathf.Clamp(-pos.z, -Edge.z, Edge.z);
        cloud.transform.position = pos;
    }
    bool CheckEdge(Vector3 pos)
    {
        if (pos.x > Edge.x || pos.x < -Edge.x) return true;
        if (pos.z > Edge.z || pos.z < -Edge.z) return true;
        return false;
    }
}
