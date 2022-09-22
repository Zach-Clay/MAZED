using System;
namespace api_backend
{
    public class DBContext
    {
        //Returns the connection string for the DB
        public static string ConnectionString()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false);
            IConfiguration config = builder.Build();

            return config.GetValue<string>("ConnectionStrings:DB");
        }//end ConnectionString
    }
}
