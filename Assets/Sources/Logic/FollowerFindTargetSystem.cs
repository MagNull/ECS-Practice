using System.Collections.Generic;
using Entitas;

namespace Sources.Logic
{
    public class FollowerFindTargetSystem : IExecuteSystem
    {
        private Contexts _contexts;
        private IGroup<GameEntity> _followers;
        
        public FollowerFindTargetSystem(Contexts context)
        {
            _contexts = context;
            _followers = _contexts.game.GetGroup(GameMatcher.Follower);
        }
        

        public void Execute()
        {
            foreach (var follower in _followers)
            {
                follower.follower.TargetPosition = _contexts.game.playerEntity.position.Position;
            }
        }
    }
}