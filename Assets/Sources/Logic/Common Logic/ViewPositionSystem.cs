using System.Collections.Generic;
using Entitas;

namespace Sources.Logic
{
    public class ViewPositionSystem : ReactiveSystem<GameEntity>
    {
        public ViewPositionSystem(Contexts context) : base(context.game)
        {
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.Position);
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.hasView;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var entity in entities)
            { 
                entity.view.View.transform.position = entity.position.Position;
            }
        }
    }
}