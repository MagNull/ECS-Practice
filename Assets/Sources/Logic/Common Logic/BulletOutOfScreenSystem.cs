using Entitas;

public class BulletOutOfScreenSystem : IExecuteSystem  
{
	private Contexts _contexts;
	private IGroup<GameEntity> _bullets;

    public BulletOutOfScreenSystem(Contexts contexts) 
    {
    	_contexts = contexts;
        _bullets = _contexts.game.GetGroup(GameMatcher.Bullet);
    }

	public void Execute()
	{
		foreach (var bullet in _bullets)
		{
			if (!_contexts.game.globals.value.CheckBound(bullet.position.Position))
			{
				bullet.isInPool = true;
			}
		}
	}
}