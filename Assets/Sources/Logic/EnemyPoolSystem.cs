using Entitas;
using System.Collections.Generic;

public class EnemyPoolSystem : ReactiveSystem<GameEntity>
{
	private Contexts _contexts;

	public EnemyPoolSystem(Contexts contexts) : base(contexts.game)
	{
		_contexts = contexts;
	}

	protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
	{
		return context.CreateCollector(GameMatcher.InPool);
	}

	protected override bool Filter(GameEntity entity)
	{
		return entity.isEnemy;
	}

	protected override void Execute(List<GameEntity> entities)
	{
		foreach (var e in entities)
		{
			_contexts.game.enemyPool.Pool.Push(e);
			e.view.View.SetActive(false);
		}
	}
}