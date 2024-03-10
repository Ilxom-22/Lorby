namespace Lorby.Domain.Common.Entities;

public interface IEntity
{
    /// <summary>
    /// Gets or sets the unique identifier of the entity.
    /// </summary>
    public Guid Id { get; set; }
}