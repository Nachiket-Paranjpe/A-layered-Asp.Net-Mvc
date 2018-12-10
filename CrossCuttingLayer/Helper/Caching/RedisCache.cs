using Newtonsoft.Json;
using PostSharp.Aspects;
using PostSharp.Extensibility;
using PostSharp.Patterns.Caching;
using PostSharp.Patterns.Caching.Backends.Redis;
using PostSharp.Serialization;
using StackExchange.Redis;
using System;
using System.Configuration;
using System.Runtime.Caching;
using System.Text;

namespace CrossCuttingLayer.Helper.Caching
{
    [PSerializable]
    [AttributeUsage(AttributeTargets.All)]
    [MulticastAttributeUsage(MulticastTargets.Default)]
    public sealed class RedisCacheAttribute : OnMethodBoundaryAspect
    {
        private static object syncLock = new object();
        private static ConnectionMultiplexer connection;
        private int _expiration;
        private IDatabase _cacheDatabase;
        private string cachename;
        private RedisCachingBackendConfiguration _redisCachingConfiguration;
        JsonSerializerSettings jsonSettings;
        public int CacheExpiration {
            get {
                if (_expiration > 0)
                    return _expiration;
                else
                    return _expiration = 5;
                }
            set => _expiration = value; }

        // Below code to lazy initialize the cache connection is not supported by the PostSharp version 5.0.55 which is installed currently.
        //private Lazy<ConnectionMultiplexer> lazyConnection = new Lazy<ConnectionMultiplexer>(() =>
        //{
        //    string configString = ConfigurationManager.AppSettings["CacheConnection"];
        //    var options = ConfigurationOptions.Parse(configString);
        //    connection = ConnectionMultiplexer.Connect(options);

        //    return connection;
        //});

        public RedisCacheAttribute(string name="RedisCache")
        {
            this.cachename = name;
        }

        /// <summary>
        ///   Method executed <i>before</i> the target method of the aspect.
        /// </summary>
        /// <param name="args">Method execution context.</param>       
        public override void OnEntry(MethodExecutionArgs args)
        {
            lock (syncLock)
            {
                if (connection == null)
                {
                    string configString = ConfigurationManager.AppSettings["CacheConnection"];
                    var options = ConfigurationOptions.Parse(configString);
                    connection = ConnectionMultiplexer.Connect(options);

                    _redisCachingConfiguration = new RedisCachingBackendConfiguration();
                    CachingServices.DefaultBackend = RedisCachingBackend.Create(connection, _redisCachingConfiguration);                                        
                }

                   jsonSettings = new JsonSerializerSettings()
                    {
                        TypeNameHandling = TypeNameHandling.All,
                        PreserveReferencesHandling = PreserveReferencesHandling.All
                    };               

                _cacheDatabase = connection.GetDatabase();
            }

            // Build the cache key.

            var stringBuilder = new StringBuilder();
            Buildkey(args, stringBuilder);
            var cachekey = stringBuilder.ToString();          

            // Check if the cache key exists in the Redis Cache database
            if (_cacheDatabase.KeyExists(cachekey))
            {
                // Get the value from the cache.
                var cachedValue = _cacheDatabase.StringGet(cachekey);

                // If the value is already in the cache, don't even execute the method. Set the return value from the cache and return immediately.

                args.ReturnValue = JsonConvert.DeserializeObject(cachedValue, jsonSettings);
                args.FlowBehavior = FlowBehavior.Return;
            }
            else
            {
                // If the value is not in the cache, continue with method execution, but store the cache key so we can reuse it when the method exits.
                args.MethodExecutionTag = cachekey;
                args.FlowBehavior = FlowBehavior.Continue;
            }
        }

        private static void Buildkey(MethodExecutionArgs args, StringBuilder stringBuilder)
        {
            // Append type and method name.
            //var declaringType = args.Method.DeclaringType;
            //stringBuilder.Append(declaringType).Append(".").Append(args.Method.Name); 

            stringBuilder.Append(args.Method.Name);

            Type[] genericArguments;

            // Append arguments
            if (args.Method.IsGenericMethod)
            {
                genericArguments = args.Method.GetGenericArguments();
                stringBuilder.Append(genericArguments);
            }
            else
            {
                stringBuilder.Append(args.Arguments);
            }

            if (args.Method.GetParameters().Length ==2)
            {
                stringBuilder.Append(args.Arguments.GetArgument(0) + "." + args.Arguments.GetArgument(1));
            }
            else
            {
                if (args.Method.GetParameters().Length == 1)
                {
                    stringBuilder.Append(args.Arguments.GetArgument(0));
                }
            }

        }

        /// <summary>
        ///   Method executed <i>after</i> the target method of the aspect.
        /// </summary>
        /// <param name="args">Method execution context.</param>
        public override void OnSuccess(MethodExecutionArgs args)
        {
            var cacheKey = (string) args.MethodExecutionTag;
            _cacheDatabase.StringSet(cacheKey, JsonConvert.SerializeObject(args.ReturnValue, formatting: Formatting.Indented, jsonSettings), TimeSpan.FromMinutes(CacheExpiration));
        }

        public override void OnExit(MethodExecutionArgs args)
        {
            base.OnExit(args);  
        }

        public override void OnException(MethodExecutionArgs args)
        {
            base.OnException(args); 
        }

    }

    [PSerializable]
    [AttributeUsage(AttributeTargets.All)]
    [MulticastAttributeUsage(MulticastTargets.Default)]    
    public sealed class InvalidateRedisCacheAttribute : OnMethodBoundaryAspect
    {
        private static object syncLock = new object();
        private static ConnectionMultiplexer connection;
        private int _expiration;
        private IDatabase _cacheDatabase;
        private string cachename;
        private RedisCachingBackendConfiguration _redisCachingConfiguration;
        JsonSerializerSettings jsonSettings;
        public string CachedMethod { get; set; }
        public override void OnEntry(MethodExecutionArgs args)
        {
            // Build the cache key.

            var stringBuilder = new StringBuilder();
            Buildkey(args, stringBuilder);
            var cachekey = stringBuilder.ToString();

            args.MethodExecutionTag = cachekey;
            args.FlowBehavior = FlowBehavior.Continue;
        }

        public override void OnSuccess(MethodExecutionArgs args)
        {
            lock (syncLock)
            {
                if (connection == null)
                {
                    string configString = ConfigurationManager.AppSettings["CacheConnection"];
                    var options = ConfigurationOptions.Parse(configString);
                    connection = ConnectionMultiplexer.Connect(options);

                    _redisCachingConfiguration = new RedisCachingBackendConfiguration();
                    CachingServices.DefaultBackend = RedisCachingBackend.Create(connection, _redisCachingConfiguration);
                }

                jsonSettings = new JsonSerializerSettings()
                {
                    TypeNameHandling = TypeNameHandling.All,
                    PreserveReferencesHandling = PreserveReferencesHandling.All
                };

                _cacheDatabase = connection.GetDatabase();
            }

               var cacheKey = (string)args.MethodExecutionTag;

                if (_cacheDatabase.KeyExists(cacheKey))
                {
                    _cacheDatabase.KeyExpireAsync(cacheKey, TimeSpan.FromMilliseconds(2), CommandFlags.None);
                }
           
        }

        public override void OnExit(MethodExecutionArgs args)
        {
            _redisCachingConfiguration = null;
            connection.Dispose();
        }

        public override void OnException(MethodExecutionArgs args)
        {
            base.OnException(args); 
        }

        private static void Buildkey(MethodExecutionArgs args, StringBuilder stringBuilder)
        {
            // Append type and method name.
            //var declaringType = args.Method.DeclaringType;
            //stringBuilder.Append(declaringType).Append(".").Append(args.Method.Name); 

            stringBuilder.Append(args.Method.Name);

            Type[] genericArguments;

            // Append arguments
            if (args.Method.IsGenericMethod)
            {
                genericArguments = args.Method.GetGenericArguments();
                stringBuilder.Append(genericArguments);
            }
            else
            {
                stringBuilder.Append(args.Arguments);
            }

            if (args.Method.GetParameters().Length == 2)
            {
                stringBuilder.Append(args.Arguments.GetArgument(0) + "." + args.Arguments.GetArgument(1));
            }
            else
            {
                if (args.Method.GetParameters().Length == 1)
                {
                    stringBuilder.Append(args.Arguments.GetArgument(0));
                }
            }

        }
    }
}