using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityManager : MonoBehaviour
{
    public static CityManager Instance;

    public Material LightOn1, LightOn2, LightOff;
    
    private void Awake()
    {
        Instance = this;
    }

}
