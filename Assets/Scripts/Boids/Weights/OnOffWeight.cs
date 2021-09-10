using UnityEngine;

    public class OnOffWeight : SteeringWeight
    {
        public float onWeight = 2;

        public bool IsOn
        {
            get; 
            set;
        }
        
        public override float GetWeight()
        {
            return IsOn ? onWeight : 0;
        }
    }
