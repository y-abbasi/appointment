using DevArt.Core.Application;
using DevArt.Core.Domain;
using DevArt.Core.IdentityAccess;

namespace DevArt.Core.Queries;

public record GetById<TIdentity>(TIdentity Id) : ICommand<TIdentity> where TIdentity : IIdentifier
{
    public UserId UserId { get; }
    public TenantId TenantId { get; }
}