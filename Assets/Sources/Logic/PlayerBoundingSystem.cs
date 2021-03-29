using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace Sources.Logic
{
	public class PlayerBoundingSystem : ReactiveSystem<GameEntity>
	{
		private Contexts _contexts;
	
		public PlayerBoundingSystem (Contexts contexts) : base(contexts.game)
		{
			_contexts = contexts;
		}
		
		protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
		{
			return context.CreateCollector(GameMatcher.Position);
		}
		
		protected override bool Filter(GameEntity entity)
		{
			return entity.isPlayer;
		}

		protected override void Execute(List<GameEntity> entities) {
			Vector2 minBounds = new Vector2(_contexts.game.globals.value.PlayerMinBoundX,
				_contexts.game.globals.value.PlayerMinBoundZ);
			Vector2 maxBounds = new Vector2(_contexts.game.globals.value.PlayerMaxBoundX,
				_contexts.game.globals.value.PlayerMaxBoundZ);
			foreach (var entity in entities) 
			{
				if (entity.position.Position.x <= minBounds.x)
				{
					entity.position.Position.x = minBounds.x;
				}
				if (entity.position.Position.x >= maxBounds.x)
				{
					entity.position.Position.x = maxBounds.x;
				}

				if (entity.position.Position.z <= minBounds.y)
				{
					entity.position.Position.z = minBounds.y;
				}
				if (entity.position.Position.z >= maxBounds.y)
				{
					entity.position.Position.z = maxBounds.y;
				}
			}
		}
	}
}
