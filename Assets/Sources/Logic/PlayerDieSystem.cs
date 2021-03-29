using System;
using Entitas;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDieSystem : ReactiveSystem<GameEntity> 
{
    private Contexts _contexts;
    private GameObject _menu;
    private Action _pauseGame;
    
	public PlayerDieSystem (Contexts contexts, GameObject menu, Action pause) : base(contexts.game) 
	{
		_contexts = contexts;
		_menu = menu;
		_pauseGame = pause;
	}
		
	protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
	{
		return context.CreateCollector(GameMatcher.Health);
	}
		
	protected override bool Filter(GameEntity entity)
	{
		return entity.isPlayer;
	}

	protected override void Execute(List<GameEntity> entities) 
	{
		foreach (var e in entities) 
		{
			if (e.health.Value <= 0)
			{
				_menu.SetActive(true);
				_pauseGame.Invoke();
			}
		}
	}
}
