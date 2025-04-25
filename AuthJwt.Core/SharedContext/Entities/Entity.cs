namespace AuthJwt.Core.SharedContext.Entities;

public abstract class Entity : IEquatable<Guid>
{
    public Guid Id { get; init; } = Guid.NewGuid();

    public bool Equals(Guid id) 
        => Id == id;
    
    public override int GetHashCode() 
        => Id.GetHashCode();
}
