namespace Started.Project.RabbitMq.Shared
{
    public static class Settings
    {
        public static string ConnectionString { get; set; }
        public static string DbType { get; set; } = "SQLSERVER";

        public static string VirtualDirectory { get; set; }
    }
}