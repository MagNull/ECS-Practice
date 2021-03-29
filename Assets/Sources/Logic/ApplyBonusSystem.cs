using Entitas;
using System.Collections.Generic;
using Sources;
using UnityEngine;

public class ApplyBonusSystem : ReactiveSystem<GameEntity> 
{
    private Contexts _contexts;
    
	public ApplyBonusSystem (Contexts contexts) : base(contexts.game) 
	{
		_contexts = contexts;
	}
		
	protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
	{
		return context.CreateCollector(GameMatcher.Collusion);
	}
		
	protected override bool Filter(GameEntity entity)
	{
		return (entity.collusion.Entity1.isPlayer && entity.collusion.Entity2.hasBonus)
		       || (entity.collusion.Entity1.hasBonus && entity.collusion.Entity2.isPlayer);
	}

	protected override void Execute(List<GameEntity> entities) 
	{
		foreach (var e in entities)
		{
			GameEntity bonus;
			GameEntity player;
			if (e.collusion.Entity1.isPlayer)
			{
				bonus = e.collusion.Entity2;
				player = e.collusion.Entity1;
			}
			else
			{
				bonus = e.collusion.Entity1;
				player = e.collusion.Entity2;
			}
			switch (bonus.bonus.BonusType)
			{
				case BonusType.INCREASE_SHOT_SIZE:
					player.shooter.OneShotSize++;
					_contexts.game.CreateEntity().AddBonusTimer(bonus.bonus.Duration,BonusType.INCREASE_SHOT_SIZE);
					break;
				case BonusType.INCREASE_SHOOT_SPEED:
					player.shooter.ShootDelay -= _contexts.game.globals.value.IncreaseShootSpeed;
					_contexts.game.CreateEntity().AddBonusTimer(bonus.bonus.Duration,BonusType.INCREASE_SHOOT_SPEED);
					break;
				case BonusType.HEAL:
					int newHealth = player.health.Value + 1;
					newHealth = Mathf.Clamp(newHealth, 0, _contexts.game.globals.value.PlayerHealth);
					player.ReplaceHealth(newHealth);
					break;
			}
			bonus.isDestroyed = true;
			e.isDestroyed = true;
		}
	}
}
