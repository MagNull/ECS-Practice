using System.Numerics;
using Entitas;
using Vector3 = UnityEngine.Vector3;

[Game]
public class PositionComponent : IComponent
{
    public Vector3 Position;
}