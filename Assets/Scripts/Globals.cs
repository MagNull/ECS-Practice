using System.Collections.Generic;
using Entitas.CodeGeneration.Attributes;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Rendering;

[Game, Unique, CreateAssetMenu]
public class Globals : SerializedScriptableObject
{
    [Header("Player Configs")]
    public GameObject PlayerPrefab;
    public Transform PlayerSpawnPoint;
    public float PlayerMovementSpeed;
    public float PlayerMinBoundX;
    public float PlayerMaxBoundX;
    public float PlayerMinBoundZ;
    public float PlayerMaxBoundZ;
    public int PlayerHealth;
    
    [Header("Player Shoot Configs")]
    public GameObject PlayerBulletPrefab;
    public float ShootDelay;
    public float BulletSpeed;
    
    [Header("Enemy Configs")]
    public float EnemyMovementSpeed;
    public float EnemySpawnMinRadius;
    public float EnemySpawnMaxRadius;
    public GameObject EnemyPrefab;
    public ParticleSystem DeathVFX;
    
    
    [Header("Enemy Shoot Configs")]
    public float ChanceToShoot;
    public float EnemyShootDelay;
    public float EnemyBulletSpeed;
    public GameObject EnemyBulletPrefab;
    
    [Header("Gameplay Configs")]
    public int OneWaveSize;
    public int WaveDelay;
    public Dictionary<int, Vector2Int> DifficultyDictionary = new Dictionary<int, Vector2Int>();
    
    [Header("Bonuses Configs")]
    public float BonusFallSpeed;
    public Quaternion BonusRotationSpeed;
    public float BonusChance;
    public GameObject RandomBonusPrefab;
    public float BonusDuration;
    
    [Header("Increase Shoot Speed Config")]
    public float IncreaseShootSpeed;

    public bool IsPaused;
    public bool CheckBound(Vector3 position)
    {
        return position.x < PlayerMaxBoundX &&
               position.x > PlayerMinBoundX &&
               position.z < PlayerMaxBoundZ &&
               position.z > PlayerMinBoundZ;
    }
}