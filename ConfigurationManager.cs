namespace WebApiJwtAuth
{
    public static class ConfigurationManager1
    {
        public static IConfiguration Configuration { get; }

        static ConfigurationManager1()
        {
            Configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
        }
    }
}
