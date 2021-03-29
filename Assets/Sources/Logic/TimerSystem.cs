using Entitas;
using UnityEngine;

namespace Sources.Logic
{
	public class TimerSystem : IExecuteSystem  
	{
		private Contexts _contexts;
		private IGroup<GameEntity> _timers;

		public TimerSystem(Contexts contexts) 
		{
			_contexts = contexts;
			_timers = _contexts.game.GetGroup(GameMatcher.Timer);
		}

		public void Execute()
		{
			foreach (var entity in _timers)
			{
				float tick = entity.timer.Tick - Time.deltaTime;
				if (tick <= 0)
				{
					entity.ReplaceTimer(0);
				}
				else
				{
					entity.ReplaceTimer(tick);
				}
			}
		}
	}
}