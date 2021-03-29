using Entitas;
using UnityEngine;

namespace Sources.Logic
{
	public class MoveSystem : IExecuteSystem  
	{
		private Contexts _contexts;
		private IGroup<GameEntity> _group;

		public MoveSystem(Contexts contexts) 
		{
			_contexts = contexts;
			_group = _contexts.game.GetGroup(GameMatcher.Moveable);
		}

		public void Execute()
		{
			if (!_contexts.game.globals.value.IsPaused)
			{
				foreach (var entity in _group)
				{
					Vector3 oldPos = entity.position.Position;
					entity.ReplacePosition(oldPos + entity.moveable.Movement * Time.deltaTime);
				}
			}
		}
	}
}