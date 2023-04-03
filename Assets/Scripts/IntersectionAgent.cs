using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;
using TrafficSimulation;

public class IntersectionAgent : Agent
{
    public Intersection intersection;
    private int actionIndex = 0;
    private float totalWaitTime = 0.0f;
    private float maxWaitTime = 60.0f; // limite de tempo de espera antes de terminar o episódio
    private Dictionary<GameObject, float> vehicleArrivalTimes = new Dictionary<GameObject, float>(); // dicionário para armazenar os tempos de chegada dos veículos


    // variáveis para controle do tempo de troca de ações
    private float lastActionTime = 0.0f;
    public float minActionInterval = 8.0f; // tempo mínimo entre trocas de ações

    void Start()
    {
        Debug.Log("Start called!");
    }

    public override void OnEpisodeBegin()
    {
        Debug.Log("Episode begin!");
        totalWaitTime = 0.0f;
        vehicleArrivalTimes.Clear(); // limpa o dicionário de tempos de chegada dos veículos
    }

    // public override void CollectObservations(VectorSensor sensor)
    // {
    //     sensor.AddObservation(intersection.vehiclesQueue.Count);
    // }

    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        int action = actionBuffers.DiscreteActions[0];

        // check if enough time has passed since last action switch
        if (Time.time - lastActionTime < minActionInterval)
        {
            // not enough time has passed, do not switch the lights
            SetReward(-0.1f);
            return;
        }

        // enough time has passed, switch the lights
        if (action == 0)
        {
            intersection.SwitchLightsToGroup1();
        }
        else if (action == 1)
        {
            intersection.SwitchLightsToGroup2();
        }

        // update the timer
        lastActionTime = Time.time;
        // Calcula a recompensa baseada no tempo de espera dos veículos na fila
        float reward = 0.0f;
        foreach (GameObject vehicle in intersection.vehiclesQueue)
        {
            float waitingTime = 0.0f;
            if (!vehicleArrivalTimes.TryGetValue(vehicle, out waitingTime)) // se o veículo não está no dicionário, adiciona-o com o tempo de chegada atual
            {
                vehicleArrivalTimes.Add(vehicle, Time.time);
            }
            else // se o veículo já está no dicionário, calcula o tempo de espera atual
            {
                waitingTime = Time.time - waitingTime;
            }

            if (waitingTime > maxWaitTime)
            {
                SetReward(-0.01f);
                EndEpisode(); // Termina o episódio se um veículo esperou por mais tempo do que o permitido
                return;
            }
            else if (waitingTime > 0.0f)
            {
                reward += 1.0f / waitingTime; // Adiciona uma recompensa proporcional ao inverso do tempo de espera
            }
        }
        SetReward(reward);

        totalWaitTime += Time.deltaTime;

        // Termina o episódio se o tempo de espera máximo for ultrapassado
        if (totalWaitTime > maxWaitTime)
        {
            SetReward(-0.01f);
            EndEpisode();
        }
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        Debug.Log("Heuristic called!");
        if (Input.GetKeyDown(KeyCode.Space))
        {
            actionIndex = (actionIndex == 0) ? 1 : 0; // Alterna entre ação 0 e 1
        }

        ActionSegment<int> discreteActions = actionsOut.DiscreteActions;
        discreteActions[0] = actionIndex;
    }
}