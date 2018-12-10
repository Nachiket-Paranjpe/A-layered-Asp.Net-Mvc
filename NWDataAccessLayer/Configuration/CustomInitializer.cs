using System;
using System.Data.Entity;
namespace DataAccessLayer.Configuration
{
    internal class CustomInitializer<Tcontext> : IDatabaseInitializer<Tcontext> where Tcontext: DbContext
    {
        private  string Strategy { get; set; }

        private Tcontext context;
        public CustomInitializer(Tcontext tcontext)
        {
            this.context = tcontext;
            InitializeDatabase(context);
        }
        public void InitializeDatabase(Tcontext context)
        {
            Strategy = "CreateDatabaseIfNotExists";
            IDatabaseInitializer<Tcontext> result;

            switch (Strategy)
            {
                case "CreateDatabaseIfNotExists":
                   result =  new CreateIfRequired<Tcontext>(context);
                    break;

                case "DropCreateDatabaseAlways":
                   result = new DropCreateAlways<Tcontext>(context);
                    break;

                case "DropCreateDatabaseIfModelChanges":
                    result = new DropCreateIfModelChanges<Tcontext>(context);
                    break;

                default:
                    throw new ArgumentOutOfRangeException("Strategy for database initialization is not currently available");
            }            
        }      
    }
}
