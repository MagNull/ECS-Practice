using System;
using System.Collections;
using System.Collections.Generic;
using Entitas;
using Sources.Logic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] private Slider _healthSlider;
    [SerializeField] private Globals _globals;
    [SerializeField] private Text _killCountText;
    [SerializeField] private GameObject _menu;
    private Systems _systems;
    private Contexts _contexts;

    private void Awake()
    {
        _contexts = Contexts.sharedInstance;
        _globals.IsPaused = false;
        
        _contexts.game.SetGlobals(_globals);
        _contexts.game.SetKillCount(0);
        
        CalculateBounds(_contexts);
        
        _systems = CreateSystems(_contexts);

        InitSystems();
    }

    public void Restart()
    {
        _systems.DeactivateReactiveSystems();
        _systems.TearDown();
        _contexts.Reset();
        _contexts.game.SetGlobals(_globals);
        _contexts.game.SetKillCount(0);
        _menu.SetActive(false);
        _globals.IsPaused = false;
        InitSystems();
    }

    public void Exit() => Application.Quit();

    public void Pause()
    {
        _globals.IsPaused = true;
    }
    
    private void InitSystems()
    {
        _systems.ActivateReactiveSystems();
        _systems.Initialize();
    }

    private void Update()
    {
        if(_contexts.game.GetEntities().Length > 0) _systems.Execute();
    }

    private void CalculateBounds(Contexts contexts)
    {
        var camera = Camera.main;
        var globals = contexts.game.globals;
        
        Plane plane = new Plane(Vector3.up, Vector3.zero);
        Ray lowerBoundRay = camera.ScreenPointToRay(Vector3.zero);
        Ray upperBoundRay = camera.ScreenPointToRay(new Vector2(camera.pixelWidth, camera.pixelHeight));
        if (plane.Raycast(lowerBoundRay, out float d1))
        {
            globals.value.PlayerMinBoundX = lowerBoundRay.GetPoint(d1).x;
            globals.value.PlayerMinBoundZ = lowerBoundRay.GetPoint(d1).z;
        }
        
        if (plane.Raycast(upperBoundRay, out float d2))
        {
            globals.value.PlayerMaxBoundX = upperBoundRay.GetPoint(d2).x;
            globals.value.PlayerMaxBoundZ = upperBoundRay.GetPoint(d2).z;
        }
    }
    private Systems CreateSystems(Contexts contexts)
    {
        return new Feature("Systems")
                .Add(new PlayerInitSystem(contexts))
                .Add(new PlayerSpawnSystem(contexts))
                .Add(new PlayerDieSystem(contexts, _menu, Pause))
                
                .Add(new BulletInitSystem(contexts))
                
                .Add(new BonusSpawnSystem(contexts))
                
                .Add(new EnemyPoolSystem(contexts))
                .Add(new EnemyInitSystem(contexts))
                .Add(new EnemySpawnSystem(contexts))
                .Add(new EnemyDieSystem(contexts))

                .Add(new InputMoveSystem(contexts))
                .Add(new ShootSystem(contexts))
                
                .Add(new FollowerFindTargetSystem(contexts))
                .Add(new FollowerMoveSystem(contexts))
                
                .Add(new LookAtMovementSystem(contexts))
                .Add(new LookAtMouseSystem(contexts))
            
                .Add(new PlayerBoundingSystem(contexts))
                .Add(new DestroySystem(contexts))
                
                .Add(new BulletPoolSystem(contexts))
                .Add(new BulletOutOfScreenSystem(contexts))
                
                .Add(new ApplyBonusSystem(contexts))
                .Add(new BonusTimerSystem(contexts))
                .Add(new BonusOutOfScreenSystem(contexts))
            
                .Add(new TimerSystem(contexts))
                .Add(new DamageSystem(contexts))
                
                .Add(new MoveSystem(contexts))
            
                .Add(new RotateSystem(contexts))
                .Add(new ViewRotationSystem(contexts))
                .Add(new ViewPositionSystem(contexts))
                .Add(new PlayerHealthViewSystem(contexts, _healthSlider))

                .Add(new KillCountingSystem(contexts, _killCountText))
                .Add(new ChangeSpawnRateSystem(contexts))
            
                .Add(new TearDownSystem(contexts))
            ;
    }
}
