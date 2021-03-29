//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentContextApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameContext {

    public GameEntity bulletPoolEntity { get { return GetGroup(GameMatcher.BulletPool).GetSingleEntity(); } }
    public BulletPoolComponent bulletPool { get { return bulletPoolEntity.bulletPool; } }
    public bool hasBulletPool { get { return bulletPoolEntity != null; } }

    public GameEntity SetBulletPool(DesperateDevs.Utils.ObjectPool<GameEntity> newPlayerBulletPool, DesperateDevs.Utils.ObjectPool<GameEntity> newEnemyBulletPool) {
        if (hasBulletPool) {
            throw new Entitas.EntitasException("Could not set BulletPool!\n" + this + " already has an entity with BulletPoolComponent!",
                "You should check if the context already has a bulletPoolEntity before setting it or use context.ReplaceBulletPool().");
        }
        var entity = CreateEntity();
        entity.AddBulletPool(newPlayerBulletPool, newEnemyBulletPool);
        return entity;
    }

    public void ReplaceBulletPool(DesperateDevs.Utils.ObjectPool<GameEntity> newPlayerBulletPool, DesperateDevs.Utils.ObjectPool<GameEntity> newEnemyBulletPool) {
        var entity = bulletPoolEntity;
        if (entity == null) {
            entity = SetBulletPool(newPlayerBulletPool, newEnemyBulletPool);
        } else {
            entity.ReplaceBulletPool(newPlayerBulletPool, newEnemyBulletPool);
        }
    }

    public void RemoveBulletPool() {
        bulletPoolEntity.Destroy();
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

    public BulletPoolComponent bulletPool { get { return (BulletPoolComponent)GetComponent(GameComponentsLookup.BulletPool); } }
    public bool hasBulletPool { get { return HasComponent(GameComponentsLookup.BulletPool); } }

    public void AddBulletPool(DesperateDevs.Utils.ObjectPool<GameEntity> newPlayerBulletPool, DesperateDevs.Utils.ObjectPool<GameEntity> newEnemyBulletPool) {
        var index = GameComponentsLookup.BulletPool;
        var component = (BulletPoolComponent)CreateComponent(index, typeof(BulletPoolComponent));
        component.PlayerBulletPool = newPlayerBulletPool;
        component.EnemyBulletPool = newEnemyBulletPool;
        AddComponent(index, component);
    }

    public void ReplaceBulletPool(DesperateDevs.Utils.ObjectPool<GameEntity> newPlayerBulletPool, DesperateDevs.Utils.ObjectPool<GameEntity> newEnemyBulletPool) {
        var index = GameComponentsLookup.BulletPool;
        var component = (BulletPoolComponent)CreateComponent(index, typeof(BulletPoolComponent));
        component.PlayerBulletPool = newPlayerBulletPool;
        component.EnemyBulletPool = newEnemyBulletPool;
        ReplaceComponent(index, component);
    }

    public void RemoveBulletPool() {
        RemoveComponent(GameComponentsLookup.BulletPool);
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

    static Entitas.IMatcher<GameEntity> _matcherBulletPool;

    public static Entitas.IMatcher<GameEntity> BulletPool {
        get {
            if (_matcherBulletPool == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.BulletPool);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherBulletPool = matcher;
            }

            return _matcherBulletPool;
        }
    }
}