using Entitas;
using UnityEngine;

public class RotateSystem : IExecuteSystem  
{
	private Contexts _contexts;
	private IGroup<GameEntity> _group;
	
    public RotateSystem(Contexts contexts) 
    {
    	_contexts = contexts;
        _group = _contexts.game.GetGroup(GameMatcher.Rotatable);
    }

	public void Execute() 
	{
		if (!_contexts.game.globals.value.IsPaused)
		{
			foreach (var e in _group)
			{
				Quaternion newRotation = e.rotation.Rotation * e.rotatable.RotationSpeed;
				e.ReplaceRotation(newRotation);
			}
		}
	}
}