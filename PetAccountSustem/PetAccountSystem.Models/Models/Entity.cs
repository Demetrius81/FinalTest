using PetAccountSystem.Interfaces.Entities;
using System.Diagnostics.CodeAnalysis;

namespace PetAccountSystem.Models.Models;

/// <summary>Базовый класс сущности</summary>
public abstract class Entity : IEntity, IEquatable<Entity>
{
    public int Id { get; set; }

    public bool Equals(Entity? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return this.Id == other.Id;
    }

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Entity)obj);
    }

    public override int GetHashCode() => Id;

    public int GetHashCode([DisallowNull] Entity obj)
    {
        throw new NotImplementedException();
    }

    public static bool operator ==(Entity? left, Entity? right) => Equals(left, right);

    public static bool operator !=(Entity? left, Entity? right) => !Equals(left, right);
}
