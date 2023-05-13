using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TrafficSimulation;

public class IntersectionController : MonoBehaviour
{
    public Intersection intersection;
    public GameObject[] group1Colliders;
    public GameObject[] group2Colliders;
    private float timeSinceLastSwitch = 0f;
    private bool canSwitchLights = true;

    void Update()
    {
        if (canSwitchLights)
        {
            int numObjectsInGroup1 = CountObjectsInsideColliders(group1Colliders);
            int numObjectsInGroup2 = CountObjectsInsideColliders(group2Colliders);

            if (numObjectsInGroup1 > numObjectsInGroup2)
            {
                intersection.SwitchLightsToGroup1();
            }
            else
            {
                intersection.SwitchLightsToGroup2();
            }

            timeSinceLastSwitch = 0f;
            canSwitchLights = false;
        }
        else
        {
            timeSinceLastSwitch += Time.deltaTime;
            if (timeSinceLastSwitch >= 7f)
            {
                canSwitchLights = true;
            }
        }
    }

    int CountObjectsInsideColliders(GameObject[] colliders)
    {
        int numObjects = 0;
        foreach (GameObject collider in colliders)
        {
            numObjects += collider.GetComponent<AgentCollider>().objectsInsideCollider.Count;
        }
        return numObjects;
    }
}
