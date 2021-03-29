using Entitas;
using System.Collections.Generic;
using DesperateDevs.Utils;
using Sources;

public class BulletPoolSystem : ReactiveSystem<GameEntity> 
{
    private Contexts _contexts;

    public BulletPoolSystem (Contexts contexts) : base(contexts.game) 
	{
		_contexts = contexts;
	}
		
	protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
	{
		return context.CreateCollector(GameMatcher.InPool);
	}
		
	protected override bool Filter(GameEntity entity)
	{
		return entity.hasBullet;
	}

	protected override void Execute(List<GameEntity> entities) 
	{
		foreach (var e in entities) 
		{
			switch (e.bullet.BulletType)
			{
				case BulletType.PLAYER:
					_contexts.game.bulletPool.PlayerBulletPool.Push(e);
					break;
				case BulletType.ENEMY:
					_contexts.game.bulletPool.EnemyBulletPool.Push(e);
					break;
			}
			e.view.View.SetActive(false);
		}
	}
}
