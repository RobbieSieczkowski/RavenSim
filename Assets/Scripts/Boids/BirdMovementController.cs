using System;
using System.Collections.Generic;
using UnityEngine;

public class BirdMovementController : MonoBehaviour
{
    
    
    public SteeringWeight seekWeight;
    public SteeringWeight flockWeight;
    
    public ISteeringBehavour seekSteering;
    public ISteeringBehavour flockSteering;

    private List<(SteeringWeight, ISteeringBehavour)> _steeringBehaviors;
    private Rigidbody _rigidbody;
    
    private void Awake()
    {
        
        seekSteering = GetComponent<SeekSteering>();
        flockSteering = GetComponent<BoidsSteering>() ?? gameObject.AddComponent<BoidsSteering>();
        
        
        _rigidbody = GetComponent<Rigidbody>();
        
        //Initialize your list of weight-steering behaviour pairs
        _steeringBehaviors = new List<(SteeringWeight, ISteeringBehavour)>();
        _steeringBehaviors.Add((seekWeight, seekSteering));
        _steeringBehaviors.Add((flockWeight, flockSteering));
    }


    private void Update()
    {
        AgentInfo agent =new AgentInfo()
        {
            position = transform.position,
            velocity = _rigidbody.velocity
        };
        Vector3 steering = Vector3.zero;

        Vector3 sumValues = Vector3.zero;
        float sumWeights = 0;
        for (int i = 0; i < _steeringBehaviors.Count; i++)
        {
            float weight = _steeringBehaviors[i].Item1.GetWeight();
            Vector3 value = weight > 0
                ? _steeringBehaviors[i].Item2.CalculateSteering(agent)
                : Vector3.zero;
            sumValues += (value * weight);
            sumWeights += weight;
        }
        steering = Mathf.Abs(sumWeights) > Mathf.Epsilon ? sumValues / sumWeights : Vector3.zero;

        _rigidbody.position += steering;
    }
}