namespace ManagementApi.Infrastructure.Exceptions
{
    public class UnAuthorizedAccessException : Exception
    {
        public UnAuthorizedAccessException(string message) : base(message) { }
    }
}
