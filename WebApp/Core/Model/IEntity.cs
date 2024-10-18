namespace Core.Model;

public interface IEntity
{
}

public interface ISoftDeletableEntity: IEntity
{
    bool IsDeleted { get; set; }

    DateTime? DeletedOnUtc { get; set; }
}

public class Entity : IEntity
{
}

public class SoftDeletableEntity : ISoftDeletableEntity
{
    public bool IsDeleted { get; set; }
    public DateTime? DeletedOnUtc { get; set; }
}
