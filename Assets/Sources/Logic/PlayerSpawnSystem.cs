using System.Collections.Generic;
using Entitas;
using Entitas.Unity;
using UnityEngine;

namespace Sources.Logic
{
    public class PlayerSpawnSystem : ReactiveSystem<GameEntity>
    {
        private Contexts _contexts;

        public PlayerSpawnSystem(Contexts contexts) : base(contexts.game)
        {
            _contexts = contexts;
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.Player);
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.isPlayer;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var entity in entities)
            {
                GameObject player = GameObject.Instantiate(_contexts.game.globals.value.PlayerPrefab);
                player.GetComponent<EntityLink>().Link(entity);
                entity.AddView(player);
                player.transform.position = entity.position.Position;
            }
        }
    }
}