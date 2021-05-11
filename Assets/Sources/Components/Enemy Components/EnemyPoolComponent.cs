using DesperateDevs.Utils;
using Entitas;
using Entitas.CodeGeneration.Attributes;

[Game, Unique]
public class EnemyPoolComponent : IComponent
{
    public ObjectPool<GameEntity> Pool;
}