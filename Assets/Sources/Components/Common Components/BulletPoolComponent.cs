using DesperateDevs.Utils;
using Entitas;
using Entitas.CodeGeneration.Attributes;

[Game, Unique]
public class BulletPoolComponent : IComponent
{
    public ObjectPool<GameEntity> PlayerBulletPool;
    public ObjectPool<GameEntity> EnemyBulletPool;
}