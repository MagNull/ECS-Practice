using Entitas;
using UnityEngine;

namespace Sources.Logic
{
    public class TearDownSystem : ITearDownSystem
    {
        private Contexts _contexts;
        private IGroup<GameEntity> _group;

        public TearDownSystem(Contexts contexts)
        {
            _contexts = contexts;
            _group = _contexts.game.GetGroup(GameMatcher.View);
        }
        
        public void TearDown()
        {
            foreach (var e in _group)
            {
                GameObject.Destroy(e.view.View);
            }
        }
    }
}