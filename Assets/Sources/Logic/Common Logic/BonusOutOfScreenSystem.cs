using Entitas;

public class BonusOutOfScreenSystem : IExecuteSystem  
{
	private Contexts _contexts;
	private IGroup<GameEntity> _bonuses;

	public BonusOutOfScreenSystem(Contexts contexts) 
	{
		_contexts = contexts;
		_bonuses = _contexts.game.GetGroup(GameMatcher.Bonus);
	}

	public void Execute()
	{
		foreach (var bonus in _bonuses)
		{
			if (!_contexts.game.globals.value.CheckBound(bonus.position.Position))
			{
				bonus.isDestroyed = true;
			}
		}
	}
}