using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightAtNight : MonoBehaviour
{
    Light light;
    MeshRenderer parentMeshRenderer;
    private void Start()
    {
        light = GetComponent<Light>();
        parentMeshRenderer = GetComponentInParent<MeshRenderer>();
        DayNightCycle.Instance.NightTime += Switch;
    }
    public void Switch(bool on)
    {
        light.enabled = on;
        ChangeMaterial(on);
    }

    private void ChangeMaterial(bool on)
    {
        var mats = parentMeshRenderer.materials;
        var newMats = mats;
        for (int i = 0; i < mats.Length ; i++)
        {
            if (CheckMatName(mats[i].name))
            {
                newMats[i] = on ? CityManager.Instance.LightOn1 : CityManager.Instance.LightOff;
            }
        }
        parentMeshRenderer.materials = newMats;
    }
    bool CheckMatName(string name)
    {
        if (name == "LightOn1") return true;
        if (name == "LightOn2") return true;
        if (name == "LightOff") return true;
        if (name == "LightOn1 (Instance)") return true;
        if (name == "LightOn2 (Instance)") return true;
        if (name == "LightOff (Instance)") return true;
        return false;
    }
}
