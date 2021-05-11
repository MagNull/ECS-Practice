using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class DestroySystem : ReactiveSystem<GameEntity> 
{
    private Contexts _contexts;
    public DestroySystem (Contexts contexts) : base(contexts.game) 
	{
		_contexts = contexts;
	}
		
	protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
	{
		return context.CreateCollector(GameMatcher.Destroyed);
	}
		
	protected override bool Filter(GameEntity entity)
	{
		return true;
	}

	protected override void Execute(List<GameEntity> entities) 
	{
		foreach (var e in entities) 
		{
			if (e.hasView)
			{
				GameObject.Destroy(e.view.View);
			}
			e.Destroy();
		}
	}
}
