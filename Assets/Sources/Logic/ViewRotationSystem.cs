using Entitas;
using System.Collections.Generic;

public class ViewRotationSystem : ReactiveSystem<GameEntity> 
{
    private readonly Contexts _contexts;

    public ViewRotationSystem(Contexts context) : base(context.game)
    {
	    _contexts = context;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
	    return context.CreateCollector(GameMatcher.Rotation);
    }

    protected override bool Filter(GameEntity entity)
    {
	    return entity.hasView;
    }

    protected override void Execute(List<GameEntity> entities)
    {
	    if (!_contexts.game.globals.value.IsPaused)
	    {
		    foreach (var entity in entities)
		    { 
			    entity.view.View.transform.rotation = entity.rotation.Rotation;
		    } 
	    }
    }
}
