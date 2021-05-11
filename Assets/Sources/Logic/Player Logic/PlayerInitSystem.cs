using System.Numerics;
using Entitas;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

namespace Sources.Logic
{
    public class PlayerInitSystem : IInitializeSystem
    {
        private Contexts _contexts;

        public PlayerInitSystem(Contexts contexts)
        {
            _contexts = contexts;
        }
        
        public void Initialize()
        {
            GameEntity playerEntity = _contexts.game.CreateEntity();
            playerEntity.AddPosition(_contexts.game.globals.value.PlayerSpawnPoint.position);
            playerEntity.AddRotation(Quaternion.identity);
            playerEntity.isPlayer = true;
            playerEntity.isInputMove = true;
            playerEntity.isLookAtMouse = true;
            playerEntity.AddTimer(0);
            playerEntity.AddHealth(_contexts.game.globals.value.PlayerHealth);
            playerEntity.AddShooter(_contexts.game.globals.value.ShootDelay, 
                                    _contexts.game.globals.value.PlayerBulletPrefab,
                                    _contexts.game.globals.value.BulletSpeed,
                                    BulletType.PLAYER, 1);
            playerEntity.AddMoveable(Vector3.zero);
        }
    }
}