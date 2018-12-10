using DataAccessLayer.Context;
using DataAccessLayer.Interfaces.Context;
using DataAccessLayer.RepositoryImpl;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace DataAccessLayer
{
    public class UnitOfWork :  IUnitofwork, IDisposable
    {
        private  DbContext _context;
        private Hashtable _repositories;
        private Dictionary<string, string> Recordsmismatch =null;
        private bool ConcurrencyResolved { get; set; }
        public UnitOfWork(DbContext context)
        {   
           this._context = context;          
        }
        public UnitOfWork()
        {
            _context = new NwDBcontext();
        }
        //public GenericRepository<Products> ProductRepository
        //{
        //    get
        //    {
        //        if (this._productRepository == null)
        //        {
        //            this._productRepository = new GenericRepository<Products>(_context);
        //        }

        //        return _productRepository;
        //    }
        //}
        //public GenericRepository<Orders> OrderRepository
        //{
        //    get
        //    {
        //        if (this._ordersRepository == null)
        //        {
        //            this._ordersRepository = new GenericRepository<Orders>(_context);
        //        }

        //        return _ordersRepository;
        //    }
        //}
        //public GenericRepository<Customer> CustomerRepository
        //{
        //    get
        //    {
        //        if (this._customerRepository == null)
        //        {
        //            this._customerRepository = new GenericRepository<Customer>(_context);
        //        }

        //        return _customerRepository;
        //    }
        //}
        public bool Disposed { get; set; } = false;
        public void Save(bool saveChanges)
        {
            try
            {               
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException dbconExp)
            {
                //IEnumerable<DbEntityEntry> entries = dbconExp.Entries;
                //await DetectConflictsAsync(entries);                
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!this.Disposed)
            {
                if (disposing)
                    _context.Dispose();
            }

            this.Disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        public async Task<Dictionary<string, string>> DetectConflictsAsync(DbContext context, bool concurrencyCheckClientwins)
        {
            Dictionary<string, string> ConflictsDetected = null;
            DbPropertyValues databaseValues = null;
            ConcurrencyResolved = concurrencyCheckClientwins;

            await ResolveConflictsIfAny(_context);

            await Task.Run(() =>
            {
                foreach (var entry in context.ChangeTracker.Entries())
                {
                    var userenteredvalues = entry.Entity;                    
                    databaseValues = entry.GetDatabaseValues();

                    if (databaseValues != null && !ConcurrencyResolved)
                    {
                        ConflictsDetected = new Dictionary<string, string>();
                        foreach (var propertyName in entry.OriginalValues.PropertyNames)
                        {
                            var databasevalue = databaseValues[propertyName];
                            if (entry.State == EntityState.Modified)
                            {
                                var currentvalue = entry.CurrentValues[propertyName];                                

                                if (propertyName == "RowVersion")
                                {
                                if (ConvertObjectToByteArray(currentvalue).SequenceEqual(ConvertObjectToByteArray(databasevalue))) 
                                    ConflictsDetected = null;
                                }
                            
                               else if (propertyName != "RowVersion" && propertyName !="SupplierID" && propertyName !="CategoryID")
                                {
                                    if (!currentvalue.Equals(databasevalue))
                                    {
                                        ConflictsDetected.Add(propertyName, databasevalue.ToString());                                      
                                    }
                                }
                            }
                            else if (entry.State == EntityState.Deleted)
                            {
                                    ConflictsDetected.Add(propertyName, "Record deleted from database");
                            }
                        }
                    }                       
                }
            });

            return ConflictsDetected;
        }

        private byte[] ConvertObjectToByteArray(object currentvalue)
        {
            using (var ms = new MemoryStream())
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(ms, currentvalue);                
                return ms.ToArray();
            }
        }

        public async Task ResolveConflictsIfAny(DbContext context)
        {
            if (ConcurrencyResolved)
            {
              foreach (var entry in context.ChangeTracker.Entries())
                {
                    var entity = entry.State == EntityState.Modified ? entry.Entity : null;

                    if (entity !=null)
                        context.Entry(entity).OriginalValues.SetValues(await context.Entry(entity).GetDatabaseValuesAsync());                    
                }
            }
        }
        public IGenericRepository<T> Repository<T>() where T : class
        {
            if (_repositories == null)
                _repositories = new Hashtable();
            var type = typeof(T).Name;

            if (!_repositories.ContainsKey(type))
            {
                var repositoryType = typeof(Repository<>);
                var repositoryInstance =
                Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T)), _context);
                _repositories.Add(type, repositoryInstance);
            }

            return (IGenericRepository<T>) _repositories[type];
        }

        public async Task<Dictionary<string, string>> CheckandResolveConcurrencyIssuesAsync(bool concurrencyCheckClientWins)
        {
           return await DetectConflictsAsync(_context, concurrencyCheckClientWins);
        }
    }
}
