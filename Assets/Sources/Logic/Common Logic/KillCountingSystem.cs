using Entitas;
using System.Collections.Generic;
using UnityEngine.UI;

public class KillCountingSystem : ReactiveSystem<GameEntity>, IInitializeSystem
{
    private Contexts _contexts;
    private Text _killCountText;
    
	public KillCountingSystem (Contexts contexts, Text killCountText) : base(contexts.game) 
	{
		_contexts = contexts;
		_killCountText = killCountText;
	}

	public void Initialize()
	{
		IGroup<GameEntity> entities = _contexts.game.GetGroup(GameMatcher.KillCount);
		foreach (var e in entities)
		{
			_killCountText.text = e.killCount.Value.ToString();
		}
	}

	protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
	{
		return context.CreateCollector(GameMatcher.KillCount);
	}
		
	protected override bool Filter(GameEntity entity)
	{
		return true;
	}

	protected override void Execute(List<GameEntity> entities) 
	{
		foreach (var e in entities)
		{
			_killCountText.text = e.killCount.Value.ToString();
		}
	}
}
