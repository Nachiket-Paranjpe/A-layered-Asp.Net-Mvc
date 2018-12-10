using DataAccessLayer.Context;
using DataAccessLayer.Entities;
using System.Linq;
using System.Globalization;
using System;
using System.Data.Entity;

namespace DataAccessLayer
{
    internal class DropCreateAlways<Tcontext> : DropCreateDatabaseAlways<Tcontext> where Tcontext : DbContext 
    {
        public DropCreateAlways(Tcontext context)
        {
            InitializeDatabase(context);
        }
        public override void InitializeDatabase(Tcontext context)
        {
            if (context.Database.Exists())
            {
                context.Database.Delete();
                context.Database.Create();
            }            

            base.InitializeDatabase(context);
        }        
    }
}