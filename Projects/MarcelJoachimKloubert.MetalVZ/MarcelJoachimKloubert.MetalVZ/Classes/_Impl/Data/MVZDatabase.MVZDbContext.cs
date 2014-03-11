// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using MarcelJoachimKloubert.CLRToolbox.Data;
using MarcelJoachimKloubert.CLRToolbox.Helpers;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace MarcelJoachimKloubert.MetalVZ.Classes._Impl.Data
{
    partial class MVZDatabase
    {
        #region Nested Classes (1)

        private sealed class MVZDbContext : DbContext
        {
            #region Fields (3)

            private readonly IDictionary<Type, object> _DB_SETS = new ConcurrentDictionary<Type, object>();
            private readonly Assembly[] _ENTITY_ASSEMBLIES;
            private readonly object _SYNC = new object();

            #endregion Fields

            #region Constructors (1)

            internal MVZDbContext(DbConnection conn, bool ownConnection)
                : base(conn, ownConnection)
            {
                this._ENTITY_ASSEMBLIES = new Assembly[] { typeof(global::MarcelJoachimKloubert.MetalVZ.Classes.Data.Entities.IMVZEntity).Assembly };

                foreach (var et in this.GetEntityTypes())
                {
                    // DbContext.Set<E>()
                    var setMethod = CollectionHelper.Single(this.GetType()
                                                                .GetMethods(BindingFlags.Instance | BindingFlags.Public),
                                                            m => m.Name == "Set" &&
                                                                 m.GetGenericArguments().Length == 1 &&
                                                                 m.GetParameters().Length == 0)
                                                    .MakeGenericMethod(et);

                    this._DB_SETS
                        .Add(et,
                             setMethod.Invoke(this, new object[0]));
                }
            }

            #endregion Constructors

            #region Methods (4)

            // Protected Methods (1) 

            protected override void OnModelCreating(DbModelBuilder modelBuilder)
            {
                foreach (var et in this.GetEntityTypes())
                {
                    var tableAttrib = et.GetCustomAttributes(typeof(global::MarcelJoachimKloubert.CLRToolbox.Data.TMTableAttribute), false)
                                        .Cast<TMTableAttribute>()
                                        .SingleOrDefault();

                    string tableName;
                    string schemaName = null;
                    if (tableAttrib != null)
                    {
                        tableName = tableAttrib.Name;
                        schemaName = tableAttrib.Schema;
                    }
                    else
                    {
                        tableName = et.Name;
                    }

                    // primary keys
                    var keyProperties = et.GetProperties(BindingFlags.Instance | BindingFlags.Public)
                                          .Where(p =>
                                          {
                                              return CollectionHelper.SingleOrDefault(p.GetCustomAttributes(typeof(global::MarcelJoachimKloubert.CLRToolbox.Data.TMColumnAttribute), true)
                                                                                       .Cast<TMColumnAttribute>(),
                                                                                      a => a.IsKey) != null;
                                          });

                    // not marked as scalar properties
                    var nonScalarProperties = et.GetProperties(BindingFlags.Instance | BindingFlags.Public)
                                                .Where(p =>
                                                {
                                                    return p.GetCustomAttributes(typeof(global::MarcelJoachimKloubert.CLRToolbox.Data.TMColumnAttribute), true)
                                                            .Cast<TMColumnAttribute>()
                                                            .SingleOrDefault() == null;
                                                });

                    // DbModelBuilder.Entity<E>()
                    var entityMethod = modelBuilder.GetType()
                                                   .GetMethod("Entity", BindingFlags.Instance | BindingFlags.Public)
                                                   .MakeGenericMethod(et);

                    var entityTypeConf = entityMethod.Invoke(modelBuilder, new object[0]);
                    if (entityTypeConf != null)
                    {
                        // EntityTypeConfiguration<TEntityType>.ToTable()

                        var entityTypeConfType = entityTypeConf.GetType();

                        object[] args;
                        if (string.IsNullOrWhiteSpace(schemaName))
                        {
                            args = new object[] { tableName };
                        }
                        else
                        {
                            // with schema

                            args = new object[] { tableName,
                                                  schemaName };
                        }

                        // and invoke...
                        CollectionHelper.Single(entityTypeConfType.GetMethods(BindingFlags.Instance | BindingFlags.Public),
                                                m => m.Name == "ToTable" &&
                                                     m.GetParameters().Length == args.Length)
                                        .Invoke(entityTypeConf, args);
                    }

                    // map primary keys
                    foreach (var key in keyProperties)
                    {
                        InvokeEntityTypeConfigMethodWithLambdaExpr(et,
                                                                   key,
                                                                   entityTypeConf,
                                                                   "HasKey");
                    }

                    // ignore all properties that are not marked as scalar properties
                    foreach (var prop in nonScalarProperties)
                    {
                        InvokeEntityTypeConfigMethodWithLambdaExpr(et,
                                                                   prop,
                                                                   entityTypeConf,
                                                                   "Ignore");
                    }
                }
            }
            // Private Methods (2) 

            private IEnumerable<Type> GetEntityTypes()
            {
                return this._ENTITY_ASSEMBLIES
                           .SelectMany(asm => asm.GetTypes())
                           .Where(t => t.IsClass &&
                                       t.IsAbstract == false &&
                                       t.GetInterfaces()
                                        .Contains(typeof(global::MarcelJoachimKloubert.MetalVZ.Classes.Data.Entities.IMVZEntity)));
            }

            private static void InvokeEntityTypeConfigMethodWithLambdaExpr(Type entityType,
                                                                           PropertyInfo entityProperty,
                                                                           object entityTypeConfiguration,
                                                                           string methodName)
            {
                var propertyType = entityProperty.PropertyType;

                var method = entityTypeConfiguration.GetType()
                                                    .GetMethod(methodName, BindingFlags.Instance | BindingFlags.Public)
                                                    .MakeGenericMethod(propertyType);

                Expression methodParam;
                {
                    var funcType = Expression.GetFuncType(entityType, propertyType);
                    var funcParam = Expression.Parameter(entityType, "e");

                    var propertyExpr = Expression.Property(funcParam,
                                                           entityProperty);

                    // Expression.Lambda<TDelegate>(Expression, ParameterExpression[])
                    var lambdaMethod = CollectionHelper.Single(typeof(global::System.Linq.Expressions.Expression).GetMethods(BindingFlags.Static | BindingFlags.Public),
                                                               m =>
                                                               {
                                                                   if (m.Name != "Lambda")
                                                                   {
                                                                       return false;
                                                                   }

                                                                   if (m.GetGenericArguments().Length != 1)
                                                                   {
                                                                       return false;
                                                                   }

                                                                   var @params = m.GetParameters();
                                                                   return @params.Length == 2 &&
                                                                          @params[0].ParameterType.Equals(typeof(global::System.Linq.Expressions.Expression)) &&
                                                                          @params[1].ParameterType.Equals(typeof(global::System.Linq.Expressions.ParameterExpression[]));
                                                               })
                                                       .MakeGenericMethod(funcType);

                    methodParam = (Expression)lambdaMethod.Invoke(null,
                                                                  new object[]
                                                                  {
                                                                      propertyExpr,
                                                                      new ParameterExpression[] { funcParam, },
                                                                  });
                }

                method.Invoke(entityTypeConfiguration,
                              new object[]
                              {
                                  methodParam,
                              });
            }
            // Internal Methods (1) 

            internal DbSet<E> GetDbSet<E>() where E : class, global::MarcelJoachimKloubert.MetalVZ.Classes.Data.Entities.IMVZEntity
            {
                return (DbSet<E>)this._DB_SETS[typeof(E)];
            }

            #endregion Methods
        }

        #endregion Nested Classes
    }
}
