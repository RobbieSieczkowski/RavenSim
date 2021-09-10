public class ConstantSteeringWeight : SteeringWeight
{
    public FloatContainer weight;
    public override float GetWeight()
    {
        return weight.value;
    }
}