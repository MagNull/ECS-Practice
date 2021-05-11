using Entitas;

namespace Sources.Logic
{
    public class FollowerMoveSystem : IExecuteSystem
    {
        private Contexts _contexts;
        private IGroup<GameEntity> _followers;
        
        public FollowerMoveSystem(Contexts contexts)
        {
            _contexts = contexts;
            _followers = _contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.Follower,GameMatcher.Moveable));
        }
        
        public void Execute()
        {
            float movementSpeed = _contexts.game.globals.value.EnemyMovementSpeed;
            foreach (var entity in _followers)
            {
                entity.ReplaceMoveable((entity.follower.TargetPosition - entity.position.Position).normalized *
                                       movementSpeed);
            }
        }
    }
}