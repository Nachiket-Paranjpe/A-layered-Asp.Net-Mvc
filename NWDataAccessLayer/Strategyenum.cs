using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Configuration
{
   internal enum DatabaseStrategy
    {
        CreateDatabaseIfRequired,
        DropCreateDatabaseAlways,
        DropCreateIfModelChanges
    }
}
