using System.Linq.Expressions;
using Lorby.Domain.Entities;
using Lorby.Persistence.DataContext;
using Lorby.Persistence.Repositories.Interfaces;

namespace Lorby.Persistence.Repositories;

public class EmailTemplateRepository(AppDbContext dbContext)
    : EntityRepositoryBase<EmailTemplate, AppDbContext>(dbContext), IEmailTemplateRepository
{
    public new IQueryable<EmailTemplate> Get(Expression<Func<EmailTemplate, bool>>? predicate = default,
                                             bool asNoTracking = false) 
        => base.Get(predicate, asNoTracking);
}