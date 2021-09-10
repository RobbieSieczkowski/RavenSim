using UnityEngine;

public class BoidsSteering : MonoBehaviour, ISteeringBehavour
{
    public Vector3 Steering { get; set; }
    public Vector3 CalculateSteering(AgentInfo agentInfo)
    {
        return Steering;
    }
}