using DataAccessLayer.Context;
using DataAccessLayer.Entities;
using System.Linq;
using System.Globalization;
using System;
using System.Data.Entity;

namespace DataAccessLayer
{
    internal class DropCreateIfModelChanges<Tcontext> : DropCreateDatabaseIfModelChanges<Tcontext> where Tcontext : DbContext
    {
        public DropCreateIfModelChanges(Tcontext context)
        {
            InitializeDatabase(context);
        }
        public override void InitializeDatabase(Tcontext context)
        {
            try
            {
                if (context.Database.CompatibleWithModel(false))
                {
                    context.Database.Delete();
                    context.Database.Create();
                }               
            }
            catch (Exception)
            {
                throw;
            }
            base.InitializeDatabase(context);
        }        
    }
}