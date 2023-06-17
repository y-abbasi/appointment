namespace Suzianna.Rest.OAuth
{
    public static class GetAccessToken
    {
        public static RopcFlowTask UsingResourceOwnerPasswordCredentialFlow()
        {
            return new();
        }
    }
}