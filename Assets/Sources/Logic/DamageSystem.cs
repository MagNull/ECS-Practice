using System.Collections.Generic;
using Entitas;

public class DamageSystem : ReactiveSystem<GameEntity>
{
	private Contexts _contexts;
	
	public DamageSystem (Contexts contexts) : base(contexts.game)
	{
		_contexts = contexts;
	}
		
	protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
	{
		return context.CreateCollector(GameMatcher.Collusion);
	}
		
	protected override bool Filter(GameEntity entity)
	{
		return (entity.collusion.Entity1.hasDamageDealer && entity.collusion.Entity2.hasHealth)
		       || (entity.collusion.Entity2.hasDamageDealer && entity.collusion.Entity1.hasHealth);
	}

	protected override void Execute(List<GameEntity> entities) 
	{
		foreach (var entity in entities) 
		{
			if (entity.collusion.Entity1.hasDamageDealer && entity.collusion.Entity2.hasHealth)
			{
				int health = entity.collusion.Entity2.health.Value;
				entity.collusion.Entity2.ReplaceHealth(health - entity.collusion.Entity1.damageDealer.Damage);
				entity.collusion.Entity1.isInPool = true;
			}
			if(entity.collusion.Entity2.hasDamageDealer && entity.collusion.Entity1.hasHealth)
			{
				int health = entity.collusion.Entity1.health.Value;
				entity.collusion.Entity1.ReplaceHealth(health - entity.collusion.Entity2.damageDealer.Damage);
				entity.collusion.Entity2.isInPool = true;
			}
			entity.isDestroyed = true;
		}
	}
}
