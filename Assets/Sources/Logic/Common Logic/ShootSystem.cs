using DesperateDevs.Utils;
using Entitas;
using UnityEngine;

namespace Sources.Logic
{
	public class ShootSystem : IExecuteSystem
	{
		private Contexts _contexts;
		private IGroup<GameEntity> _shooters;

		public ShootSystem(Contexts contexts)
		{
			_contexts = contexts;
			_shooters = _contexts.game.GetGroup(GameMatcher.Shooter);
		}

		public void Execute()
		{
			if (!_contexts.game.globals.value.IsPaused)
			{
				foreach (var entity in _shooters)
				{
					if (entity.timer.Tick <= 0)
					{
						Vector3 startVector =  Quaternion.Euler(0, -30, 0) * entity.view.View.transform.forward;
						for (int i = 1; i < entity.shooter.OneShotSize + 1; i++)
						{
							float rotation = 60 * i / (entity.shooter.OneShotSize + 1);
							Vector3 direction = Quaternion.Euler(0,  rotation, 0) * startVector;
							CreateBullet(entity, direction);
						}
						entity.ReplaceTimer(entity.shooter.ShootDelay);
					}
				}
			}
		}
		private void CreateBullet(GameEntity entity, Vector3 direction)
		{
			GameEntity bulletEntity;
			switch (entity.shooter.BulletType)
			{
				case BulletType.PLAYER:
					bulletEntity = _contexts.game.bulletPoolEntity.bulletPool.PlayerBulletPool.Get();
					break;
				default:
					bulletEntity = _contexts.game.bulletPoolEntity.bulletPool.EnemyBulletPool.Get();
					break;
			}
			bulletEntity.isInPool = false;
			bulletEntity.ReplacePosition(entity.position.Position + entity.view.View.transform.forward * 3);
			bulletEntity.ReplaceRotation(entity.rotation.Rotation);
			bulletEntity.ReplaceMoveable(entity.shooter.BulletSpeed * direction);
			bulletEntity.view.View.SetActive(true);
		}
	}
}