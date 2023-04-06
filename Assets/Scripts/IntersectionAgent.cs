using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;
using TrafficSimulation;

public class IntersectionAgent : Agent
{
    public Intersection intersection;
    // variáveis para controle do tempo de troca de ações
    void Start()
    {
        Debug.Log("Start called!");
    }

    public override void OnEpisodeBegin()
    {
        Debug.Log("Episode begin!");
    }

    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        int action = actionBuffers.DiscreteActions[0];

        if (action == 0)
        {
            intersection.SwitchLightsToGroup1();
        }
        else if (action == 1)
        {
            intersection.SwitchLightsToGroup2();
        }

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