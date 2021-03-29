using Entitas;
using UnityEngine;

namespace Sources.Logic
{
    public class InputMoveSystem : IExecuteSystem
    {
        private readonly Contexts _contexts;
        private readonly IGroup<GameEntity> _group;

        public InputMoveSystem(Contexts contexts)
        {
            _contexts = contexts;
            _group = _contexts.game.GetGroup(GameMatcher.InputMove);
        }
    
        public void Execute()
        {
            float speed = _contexts.game.globals.value.PlayerMovementSpeed;
            foreach (var entity in _group)
            {
                Vector3 movement = new Vector3(Input.GetAxis("Horizontal") * speed, 0, 
                    Input.GetAxis("Vertical") * speed);
                entity.ReplaceMoveable(movement);
            }
        }
    }
}