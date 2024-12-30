using System.Configuration;

namespace Shared.Models
{
    public class ConnectionStringProvider
    {
        public ConnectionStrings GetConnectionStrings()
        {
            return new ConnectionStrings
            {
                MyDBConnectionString = ConfigurationManager.ConnectionStrings["MyDBConnectionString"].ConnectionString,
                FOConnectionString = ConfigurationManager.ConnectionStrings["FOConnectionString"].ConnectionString
            };
        }
    }
}