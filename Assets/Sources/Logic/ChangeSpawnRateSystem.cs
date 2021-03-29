using Entitas;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSpawnRateSystem : ReactiveSystem<GameEntity>, IInitializeSystem
{
    private Contexts _contexts;
    
	public ChangeSpawnRateSystem (Contexts contexts) : base(contexts.game) 
	{
		_contexts = contexts;
	}
		
	protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
	{
		return context.CreateCollector(GameMatcher.KillCount);
	}
		
	protected override bool Filter(GameEntity entity)
	{
		return true;
	}

	public void Initialize()
	{
		var globals = _contexts.game.globals.value;
		if (globals.DifficultyDictionary.TryGetValue(_contexts.game.killCount.Value,
			out Vector2Int newDifficulty))
		{
			globals.OneWaveSize = newDifficulty.x;
			globals.WaveDelay = newDifficulty.y;
		}
	}

	protected override void Execute(List<GameEntity> entities)
	{
		var globals = _contexts.game.globals.value;
		foreach (var e in entities) 
		{
			if (globals.DifficultyDictionary.TryGetValue(e.killCount.Value,
				out Vector2Int newDifficulty))
			{
				globals.OneWaveSize = newDifficulty.x;
				globals.WaveDelay = newDifficulty.y;
			}
		}
	}
}
