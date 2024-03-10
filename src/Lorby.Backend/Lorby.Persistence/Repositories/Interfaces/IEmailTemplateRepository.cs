using System.Linq.Expressions;
using Lorby.Domain.Entities;

namespace Lorby.Persistence.Repositories.Interfaces;

/// <summary>
/// Represents a repository for managing email templates in the data store.
/// </summary>
public interface IEmailTemplateRepository
{
    /// <summary>
    /// Gets a queryable collection of email templates based on the provided predicate.
    /// </summary>
    /// <param name="predicate">The predicate for filtering email templates (optional).</param>
    /// <param name="asNoTracking">Flag indicating whether to use query tracking or not.</param>
    /// <returns>An <see cref="IQueryable"/> of email templates.</returns>
    IQueryable<EmailTemplate> Get(Expression<Func<EmailTemplate, bool>>? predicate = default, bool asNoTracking = false);
}