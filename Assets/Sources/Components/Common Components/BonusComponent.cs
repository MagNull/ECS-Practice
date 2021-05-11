using Entitas;
using Sources;

[Game]
public class BonusComponent : IComponent
{
    public BonusType BonusType;
    public float Duration;
}