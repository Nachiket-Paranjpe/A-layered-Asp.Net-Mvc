using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.SqlServer;
using DataAccessLayer.Configuration;
using DataAccessLayer;
using DataAccessLayer.Context;

namespace DataAccessLayer.Configuration
{
    public class CustomDatabaseConfiguration : DbConfiguration
    {
        public CustomDatabaseConfiguration()
        {
            this.SetDefaultConnectionFactory(new LocalDbConnectionFactory("mssqllocaldb"));            
            this.SetProviderServices("System.Data.SqlClient", System.Data.Entity.SqlServer.SqlProviderServices.Instance);            
        }
    }
}
