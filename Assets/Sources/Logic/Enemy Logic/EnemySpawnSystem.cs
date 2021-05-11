using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace Sources.Logic
{
    public class EnemySpawnSystem : IExecuteSystem, IInitializeSystem
    {
        private Contexts _contexts;
        private GameEntity _coolDowner;
        public EnemySpawnSystem(Contexts contexts)
        {
            _contexts = contexts; ;
        }

        public void Initialize()
        {
            _coolDowner = _contexts.game.CreateEntity();
            _coolDowner.AddTimer(0);
        }

        public void Execute()
        {
            if (_coolDowner.timer.Tick <= 0)
            {
                SpawnWave();
                _coolDowner.ReplaceTimer(_contexts.game.globals.value.WaveDelay);
            }
        }

        private void SpawnWave()
        {
            int waveSize = _contexts.game.globals.value.OneWaveSize;
            for (int i = 0; i < waveSize; i++)
            {
                float radius = Random.Range(_contexts.game.globals.value.EnemySpawnMinRadius, 
                    _contexts.game.globals.value.EnemySpawnMaxRadius);
                float angle = Random.Range(0, 360);
                float z = Mathf.Sin(angle) * radius;
                float x = Mathf.Cos(angle) * radius;
                var entity = _contexts.game.enemyPool.Pool.Get();
                entity.view.View.SetActive(true);
                entity.ReplacePosition(new Vector3(x,0,z));
                entity.ReplaceHealth(1);
                entity.ReplaceDamageDealer(1);
                entity.isInPool = false;
                ShootTest(entity);
            }
        }
        
        
        private void ShootTest(GameEntity entity)
        {
            int roll = Random.Range(0, 101);
            if (roll < _contexts.game.globals.value.ChanceToShoot && !entity.hasShooter)
            {
                entity.AddShooter(_contexts.game.globals.value.EnemyShootDelay,
                    _contexts.game.globals.value.EnemyBulletPrefab,
                    _contexts.game.globals.value.EnemyBulletSpeed,
                    BulletType.ENEMY, 1);
            }
        }
    }
}