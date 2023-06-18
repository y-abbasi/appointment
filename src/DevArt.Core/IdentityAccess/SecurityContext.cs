namespace DevArt.Core.IdentityAccess;

public class SecurityContext
{
    private static readonly AsyncLocal<SecurityContext> CurrentContext = new();
    public static SecurityContext Current => CurrentContext.Value ?? (CurrentContext.Value = new());
    public UserId UserId { get; internal set; }
    public TenantId TenantId { get; internal set; }
}

public class SecurityContextSetter
{
    public static void SetUserId(UserId userId) => SecurityContext.Current.UserId = userId;
    public static void SetTenantId(TenantId tenantId) => SecurityContext.Current.TenantId = tenantId;
}