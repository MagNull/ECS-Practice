using Entitas;
using System.Collections.Generic;
using Sources;
using UnityEngine;

public class BonusTimerSystem : IExecuteSystem
{
    private Contexts _contexts;
    private IGroup<GameEntity> _entities;
    
	public BonusTimerSystem (Contexts contexts)
	{
		_contexts = contexts;
		_entities = _contexts.game.GetGroup(GameMatcher.BonusTimer);
	}

	public void Execute() 
	{
		foreach (var e in _entities)
		{
			if (e.bonusTimer.Tick <= 0)
			{
				switch (e.bonusTimer.BonusType)
				{
					case BonusType.INCREASE_SHOT_SIZE:
						_contexts.game.playerEntity.shooter.OneShotSize--;
						break;
					case BonusType.INCREASE_SHOOT_SPEED:
						_contexts.game.playerEntity.shooter.ShootDelay +=
							_contexts.game.globals.value.IncreaseShootSpeed;
						break;
				}
				e.isDestroyed = true;
			}
			else
			{
				e.bonusTimer.Tick -= Time.deltaTime;
			}
		}
	}
}
