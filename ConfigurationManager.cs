namespace WebApiJwtAuth
{
    public static class ConfigurationManager
    {
        public static IConfiguration Configuration { get; }

        static ConfigurationManager()
        {
            Configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
        }
    }
}
