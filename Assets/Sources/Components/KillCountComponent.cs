using Entitas;
using Entitas.CodeGeneration.Attributes;

[Game, Unique]
public class KillCountComponent : IComponent
{
    public int Value;
}