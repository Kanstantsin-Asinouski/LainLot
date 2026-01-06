namespace DatabaseRepository.Classes
{
    internal class DatabaseConnectionException : Exception
    {
        public DatabaseConnectionException() { }

        public DatabaseConnectionException(string errorMessage) : base(errorMessage) { }
    }
}