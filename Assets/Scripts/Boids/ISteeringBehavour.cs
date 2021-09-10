using UnityEngine;

public interface ISteeringBehavour
{
    public Vector3 CalculateSteering(AgentInfo agentInfo);
}