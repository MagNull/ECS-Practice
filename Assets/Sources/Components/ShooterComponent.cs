using Entitas;
using Sources;
using UnityEngine;

[Game]
public class ShooterComponent : IComponent
{
    public float ShootDelay;
    public GameObject BulletPrefab;
    public float BulletSpeed;
    public BulletType BulletType;
    public int OneShotSize;
}