using Entitas;
using UnityEngine;

namespace Sources.Logic
{
    public class LookAtMovementSystem : IExecuteSystem
    {
        private IGroup<GameEntity> _group;

        public LookAtMovementSystem(Contexts contexts)
        {
            _group = contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.Rotation, 
                GameMatcher.LookAtMovement,
                GameMatcher.Moveable));
        }

        public void Execute()
        {
            foreach (var entity in _group)
            {
                if (entity.moveable.Movement != Vector3.zero)
                {
                    entity.ReplaceRotation(Quaternion.LookRotation(entity.moveable.Movement));
                }
            }
        }
    }
}