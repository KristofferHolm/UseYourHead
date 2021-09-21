using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableWindowLights : MonoBehaviour
{
    List<MeshRenderer> meshRenderers;
    void Start()
    {
        meshRenderers = new List<MeshRenderer>();
        meshRenderers.AddRange(transform.GetComponentsInChildren<MeshRenderer>(true));
        DayNightCycle.Instance.HoursBeforeNight += Activate;
        if (DayNightCycle.Instance.IsHoursBeforeNight)
            ChangeMaterialOfMeshRenderers(false, meshRenderers);
    }

    

    private void Activate(bool on)
    {
        StartCoroutine(DelayActivation(UnityEngine.Random.Range(0, 3), () => ChangeMaterialOfMeshRenderers(on,meshRenderers)));
    }
    IEnumerator DelayActivation(float clockHours, Action callback)
    {
        while (clockHours > 0)
        {
            clockHours -= DayNightCycle.Instance.ClockPerFrame;
            yield return null;
        }
        callback.Invoke();
    }

    private void ChangeMaterialOfMeshRenderers(bool on, List<MeshRenderer> mrenders)
    {
        foreach (var renders in mrenders)
        {
            renders.materials = ChangeMaterial(on, renders.materials);
        }
    }

    private Material[] ChangeMaterial(bool on, Material[] mats)
    {
        var newMats = mats;
        List<int> lightMats = new List<int>();
        //hotfix: only enable one windowlight but random which one.
        for (int i = 0; i < mats.Length; i++)
        {
            if (CheckMatName(mats[i].name))
            {
                newMats[i] = CityManager.Instance.LightOff;
                lightMats.Add(i);
            }
        }
        if (on)
        {
            int x = UnityEngine.Random.Range(0, lightMats.Count);
            int y = UnityEngine.Random.Range(0, 1);
            newMats[lightMats[x]] = y == 1 ? CityManager.Instance.LightOn1 : CityManager.Instance.LightOn2;
        }
        return newMats;
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
