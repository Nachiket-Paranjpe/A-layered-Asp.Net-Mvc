using DataAccessLayer;
using DataAccessLayer.Entities;
using System;
using System.Linq;
using System.Data.Entity;
using System.Globalization;

namespace DataAccessLayer
{
    // The main feature here is theSeedHandler delegate which will ensure, that our code will be invoked when data is seeding. 
    internal class CreateIfRequired<Tcontext>: CreateDatabaseIfNotExists<Tcontext> where Tcontext: DbContext
    {
        public CreateIfRequired(Tcontext context)
        {
            InitializeDatabase(context);
        }
        public override void InitializeDatabase(Tcontext context)
        {
            context.Database.CreateIfNotExists();

            base.InitializeDatabase(context);
        }       
    }
}