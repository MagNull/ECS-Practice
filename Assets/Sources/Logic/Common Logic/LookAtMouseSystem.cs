using Entitas;
using UnityEngine;
using Plane = UnityEngine.Plane;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

namespace Sources.Logic
{
	public class LookAtMouseSystem : IExecuteSystem 
	{
		private Contexts _contexts;
		private IGroup<GameEntity> _group;
		private Camera _camera;

		public LookAtMouseSystem(Contexts contexts)
		{
			_contexts = contexts;
			_group = _contexts.game.GetGroup(GameMatcher.LookAtMouse);
			_camera = Camera.main;
		}

		public void Execute()
		{
			Plane plane = new Plane(Vector3.up, Vector3.zero);
			Vector3 mousePosition = Input.mousePosition;
			Ray ray = _camera.ScreenPointToRay(mousePosition);
			if (plane.Raycast(ray, out float enter))
			{
				foreach (var entity in _group)
				{
					Vector3 lookDirection = ray.GetPoint(enter) - entity.position.Position;
					entity.ReplaceRotation(Quaternion.LookRotation(lookDirection));
				}
			}
		}
	}
}