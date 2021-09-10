using System;
using UnityEngine;

public class SeekSteering : MonoBehaviour, ISteeringBehavour
{
    public string seekPointName = "SEEK POINT";

    public float seekSpeedMax = 10;
    [Range(0,1)] public float randomness = 1;
    
    private Transform _seekPoint;
    private float _boidSeekSpeed;
    
    private void Awake()
    {
        var go = GameObject.Find(seekPointName);
        if(go == null) Debug.LogError($"No Seek Point found with name: {seekPointName}");
        
        _seekPoint = go.transform;
        _boidSeekSpeed = UnityEngine.Random.Range(seekSpeedMax * (1 - randomness), seekSpeedMax);
    }

    public Vector3 CalculateSteering(AgentInfo agentInfo)
    {
        var directionToSeek =  _seekPoint.position - agentInfo.position;
        return directionToSeek.normalized * _boidSeekSpeed;
    }
}