using DesperateDevs.Utils;
using Entitas;
using Sources;
using UnityEngine;

public class BulletInitSystem : IInitializeSystem  
{
	private Contexts _contexts;

    public BulletInitSystem(Contexts contexts) 
    {
    	_contexts = contexts;
    }

	public void Initialize() 
	{
		_contexts.game.CreateEntity()
			.AddBulletPool(
				new ObjectPool<GameEntity>(CreatePlayerBullet),
							    new ObjectPool<GameEntity>(CreateEnemyBullet));
		for (int i = 0; i < 10; i++)
		{
			_contexts.game.bulletPool.PlayerBulletPool.Push(CreatePlayerBullet());
		}
		for (int i = 0; i < 10; i++)
		{
			_contexts.game.bulletPool.EnemyBulletPool.Push(CreateEnemyBullet());
		}
		
	}	
	
	private GameEntity CreatePlayerBullet()
	{
		var bulletEntity = _contexts.game.CreateEntity();
		bulletEntity.AddDamageDealer(1);
		bulletEntity.AddMoveable(Vector3.zero);
		bulletEntity.AddRotation(Quaternion.identity);
		bulletEntity.AddPosition(Vector3.zero);
		bulletEntity.isLookAtMovement = true;
		GameObject bullet = GameObject.Instantiate(_contexts.game.globals.value.PlayerBulletPrefab);
		bullet.SetActive(false);
		bulletEntity.AddView(bullet);
		bullet.GetComponent<EntityLink>().Link(bulletEntity);
		bulletEntity.AddBullet(BulletType.PLAYER);
		return bulletEntity;
	}
	
	private GameEntity CreateEnemyBullet()
	{
		var bulletEntity = _contexts.game.CreateEntity();
		bulletEntity.AddDamageDealer(1);
		bulletEntity.AddMoveable(Vector3.zero);
		bulletEntity.AddRotation(Quaternion.identity);
		bulletEntity.AddPosition(Vector3.zero);
		bulletEntity.isLookAtMovement = true;
		GameObject bullet = GameObject.Instantiate(_contexts.game.globals.value.EnemyBulletPrefab);
		bullet.SetActive(false);
		bulletEntity.AddView(bullet);
		bullet.GetComponent<EntityLink>().Link(bulletEntity);
		bulletEntity.AddBullet(BulletType.ENEMY);
		return bulletEntity;
	}
}