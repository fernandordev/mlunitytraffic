using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;
using TrafficSimulation;

public class IntersectionAgent : Agent
{
    public Intersection intersection;
    public GameObject colliderA;
    public GameObject colliderB;
    public GameObject colliderC;
    public GameObject colliderD;
    private float lastActionTime;

    public override void CollectObservations(VectorSensor sensor)
    {
        int numObjectsInColliderA = colliderA.GetComponent<AgentCollider>().objectsInsideCollider.Count;
        sensor.AddObservation(numObjectsInColliderA);

        int numObjectsInColliderB = colliderB.GetComponent<AgentCollider>().objectsInsideCollider.Count;
        sensor.AddObservation(numObjectsInColliderB);

        int numObjectsInColliderC = colliderC.GetComponent<AgentCollider>().objectsInsideCollider.Count;
        sensor.AddObservation(numObjectsInColliderC);

        int numObjectsInColliderD = colliderD.GetComponent<AgentCollider>().objectsInsideCollider.Count;
        sensor.AddObservation(numObjectsInColliderD);
    }

    public override void OnEpisodeBegin()
    {
        Debug.Log("Episode begin!");
    }

    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        int action = actionBuffers.DiscreteActions[0];

        // Recompensa baseada no número de objetos em cada collider
        int numObjectsInColliderA = colliderA.GetComponent<AgentCollider>().objectsInsideCollider.Count;
        int numObjectsInColliderB = colliderB.GetComponent<AgentCollider>().objectsInsideCollider.Count;
        int numObjectsInColliderC = colliderC.GetComponent<AgentCollider>().objectsInsideCollider.Count;
        int numObjectsInColliderD = colliderD.GetComponent<AgentCollider>().objectsInsideCollider.Count;

        if (numObjectsInColliderA < 6 && numObjectsInColliderB < 6)
        {
            SetReward(1.0f);
        }

        if (numObjectsInColliderC < 6 && numObjectsInColliderD < 6)
        {
            SetReward(1.0f);
        }

        // Verifica se o tempo de troca está dentro dos limites
        float timeSinceLastAction = Time.time - lastActionTime;
        if (timeSinceLastAction > 8f && timeSinceLastAction < 60f)
        {
            SetReward(1.0f);
        }
        else
        {
            EndEpisode();
        }


        if (action == 0)
        {
            intersection.SwitchLightsToGroup1();
        }
        else if (action == 1)
        {
            intersection.SwitchLightsToGroup2();
        }

        lastActionTime = Time.time;
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var discreteActions = actionsOut.DiscreteActions;
        discreteActions[0] = -1;

        if (Input.GetKey(KeyCode.A))
        {
            discreteActions[0] = 0;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            discreteActions[0] = 1;
        }
    }
}