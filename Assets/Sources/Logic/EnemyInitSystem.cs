using DesperateDevs.Utils;
using Entitas;
using UnityEngine;

namespace Sources.Logic
{
    public class EnemyInitSystem : IInitializeSystem
    {
        private Contexts _contexts;

        public EnemyInitSystem(Contexts contexts)
        {
            _contexts = contexts;
        }
        
        public void Initialize()
        {
            _contexts.game.CreateEntity().AddEnemyPool(new ObjectPool<GameEntity>(CreateEnemy));
            for (int i = 0; i < 30; i++)
            {
                _contexts.game.enemyPool.Pool.Push(CreateEnemy());
            }
        }

        private GameEntity CreateEnemy()
        {
            var entity = _contexts.game.CreateEntity();
            entity.AddPosition(Vector3.zero);
            entity.AddRotation(Quaternion.identity);
            entity.AddFollower(Vector3.zero);
            entity.AddHealth(1000);
            entity.AddMoveable(Vector3.zero);
            GameObject enemy = GameObject.Instantiate(_contexts.game.globals.value.EnemyPrefab);
            ParticleSystem deathVFX = GameObject.Instantiate(_contexts.game.globals.value.DeathVFX);
            entity.AddView(enemy);
            entity.AddVFX(deathVFX);
            enemy.GetComponent<EntityLink>().Link(entity);
            entity.AddTimer(0);
            entity.isEnemy = true;
            entity.isLookAtMovement = true;
            return entity;
        }
    }
}