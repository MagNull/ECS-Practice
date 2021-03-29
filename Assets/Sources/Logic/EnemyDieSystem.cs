using System.Collections.Generic;
using Entitas;

public class EnemyDieSystem : ReactiveSystem<GameEntity> 
{
    private Contexts _contexts;
    
	public EnemyDieSystem (Contexts contexts) : base(contexts.game) 
	{
		_contexts = contexts;
	}
		
	protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
	{
		return context.CreateCollector(GameMatcher.Health);
	}
		
	protected override bool Filter(GameEntity entity)
	{
		return entity.health.Value <= 0 && entity.isEnemy;
	}

	protected override void Execute(List<GameEntity> entities) 
	{
		foreach (var e in entities)
		{
			if (e.hasShooter)
			{
				e.RemoveShooter();
			}
			int killCountValue = _contexts.game.killCount.Value;
			_contexts.game.ReplaceKillCount(killCountValue + 1);

			e.vFX.VFX.transform.position = e.position.Position;
			e.vFX.VFX.Play();
			
			e.isInPool = true;
		}
	}
}
