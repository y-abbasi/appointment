using DevArt.Core.Domain;

namespace DevArt.Core.IdentityAccess;

public record TenantId(string Value) : IValueObject;