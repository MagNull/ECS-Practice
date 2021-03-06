//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentContextApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameContext {

    public GameEntity enemyPoolEntity { get { return GetGroup(GameMatcher.EnemyPool).GetSingleEntity(); } }
    public EnemyPoolComponent enemyPool { get { return enemyPoolEntity.enemyPool; } }
    public bool hasEnemyPool { get { return enemyPoolEntity != null; } }

    public GameEntity SetEnemyPool(DesperateDevs.Utils.ObjectPool<GameEntity> newPool) {
        if (hasEnemyPool) {
            throw new Entitas.EntitasException("Could not set EnemyPool!\n" + this + " already has an entity with EnemyPoolComponent!",
                "You should check if the context already has a enemyPoolEntity before setting it or use context.ReplaceEnemyPool().");
        }
        var entity = CreateEntity();
        entity.AddEnemyPool(newPool);
        return entity;
    }

    public void ReplaceEnemyPool(DesperateDevs.Utils.ObjectPool<GameEntity> newPool) {
        var entity = enemyPoolEntity;
        if (entity == null) {
            entity = SetEnemyPool(newPool);
        } else {
            entity.ReplaceEnemyPool(newPool);
        }
    }

    public void RemoveEnemyPool() {
        enemyPoolEntity.Destroy();
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public EnemyPoolComponent enemyPool { get { return (EnemyPoolComponent)GetComponent(GameComponentsLookup.EnemyPool); } }
    public bool hasEnemyPool { get { return HasComponent(GameComponentsLookup.EnemyPool); } }

    public void AddEnemyPool(DesperateDevs.Utils.ObjectPool<GameEntity> newPool) {
        var index = GameComponentsLookup.EnemyPool;
        var component = (EnemyPoolComponent)CreateComponent(index, typeof(EnemyPoolComponent));
        component.Pool = newPool;
        AddComponent(index, component);
    }

    public void ReplaceEnemyPool(DesperateDevs.Utils.ObjectPool<GameEntity> newPool) {
        var index = GameComponentsLookup.EnemyPool;
        var component = (EnemyPoolComponent)CreateComponent(index, typeof(EnemyPoolComponent));
        component.Pool = newPool;
        ReplaceComponent(index, component);
    }

    public void RemoveEnemyPool() {
        RemoveComponent(GameComponentsLookup.EnemyPool);
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentMatcherApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed partial class GameMatcher {

    static Entitas.IMatcher<GameEntity> _matcherEnemyPool;

    public static Entitas.IMatcher<GameEntity> EnemyPool {
        get {
            if (_matcherEnemyPool == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.EnemyPool);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherEnemyPool = matcher;
            }

            return _matcherEnemyPool;
        }
    }
}
