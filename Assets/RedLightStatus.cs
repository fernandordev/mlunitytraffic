// Traffic Simulation
// https://github.com/mchrbn/unity-traffic-simulation

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TrafficSimulation;

public class RedLightStatus : MonoBehaviour
{
    public Intersection intersection;
    public int lightGroupId;  // Belong to traffic light 1 or 2?

    Light pointLight;

    void Start()
    {
        pointLight = this.transform.GetChild(0).GetComponent<Light>();
        SetTrafficLightColor();
    }

    // Update is called once per frame
    void Update()
    {
        SetTrafficLightColor();
    }

    void SetTrafficLightColor()
    {
        if (IsRedLight())
            SetLightColorToRed();
        else
            SetLightColorToGreen();
    }

    void SetLightColorToRed()
    {
        pointLight.color = new Color(1, 0, 0);
    }

    void SetLightColorToGreen()
    {
        pointLight.color = new Color(0, 1, 0);
    }

    bool IsRedLight()
    {
        return lightGroupId == intersection.currentRedLightsGroup;
    }
}
