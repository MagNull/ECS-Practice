using System;
using System.Linq;
using Entitas.Unity;
using UnityEngine;

public class CollusionEmitter : MonoBehaviour
{
    private Contexts _contexts;

    private void Start()
    {
        _contexts = Contexts.sharedInstance;
    }

    private void OnCollisionEnter(Collision other)
    {
        var entity1 = gameObject.GetComponent<EntityLink>().LinkedEntity;
        var entity2 = other.gameObject.GetComponent<EntityLink>().LinkedEntity;
        var collusion = _contexts.game.CreateEntity();
        collusion.AddCollusion(entity1, entity2);
    }
}