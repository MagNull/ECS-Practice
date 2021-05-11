using System;
using Entitas;
using System.Collections.Generic;
using Sources;
using UnityEngine;
using Random = UnityEngine.Random;

public class BonusSpawnSystem : ReactiveSystem<GameEntity> 
{
    private Contexts _contexts;
    
	public BonusSpawnSystem (Contexts contexts) : base(contexts.game) 
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
		var globals = _contexts.game.globals.value;
		foreach (var e in entities)
		{
			int roll = Random.Range(0, 100);
			if(roll <= globals.BonusChance)SpawnBonus(e, globals);
		}
	}

	private void SpawnBonus(GameEntity e, Globals globals)
	{
		var bonusEntity = _contexts.game.CreateEntity();
		bonusEntity.AddPosition(e.position.Position);
		int bonus = Random.Range(0, Enum.GetValues(typeof(BonusType)).Length);
		BonusType bonusType = (BonusType) Enum.ToObject(typeof(BulletType), bonus) ;
		bonusEntity.AddBonus(bonusType, globals.BonusDuration);
		bonusEntity.AddMoveable(Vector3.back * globals.BonusFallSpeed);
		bonusEntity.AddRotation(Quaternion.identity);
		bonusEntity.AddRotatable(_contexts.game.globals.value.BonusRotationSpeed);
		GameObject bonusView = GameObject.Instantiate(globals.RandomBonusPrefab);
		bonusEntity.AddView(bonusView);
		bonusView.GetComponent<EntityLink>().Link(bonusEntity);
	}
}
