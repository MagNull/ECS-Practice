using Entitas;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthViewSystem : IExecuteSystem, IInitializeSystem
{
    private Contexts _contexts;
    private Slider _healthSlider;
    
	public PlayerHealthViewSystem (Contexts contexts, Slider slider)
	{
		_contexts = contexts;
		_healthSlider = slider;
	}

	public void Execute()
	{
		_healthSlider.value = _contexts.game.playerEntity.health.Value;
	}

	public void Initialize()
	{
		_healthSlider.value = _contexts.game.playerEntity.health.Value;
		_healthSlider.maxValue = _contexts.game.globals.value.PlayerHealth;
	}
}
