using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using RHMIS.Application.Repositories.Base;
using RHMIS.Infrastructure.Context;
using System.Data;
using System.Reflection;

namespace RHMIS.Infrastructure.Repositories.Base
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private IDbContextTransaction? _currentTransaction;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public async Task<T?> SaveChangesSequenceAutoGenerator<T>(T entity, CancellationToken cancellationToken)
        {

            var entities = this._context.ChangeTracker.Entries();
            var result = new
            {
                AddedEntities = this._context.ChangeTracker.Entries()
                    .Where(x => x.State == EntityState.Added)
                    .Select(e => new
                    {
                        Entity = e.Entity,
                        EntityName = e.Entity.GetType().Name,
                        State = e.State.ToString(),
                    }).ToList(),

                ModifiedEntities = this._context.ChangeTracker.Entries()
                    .Where(x => x.State == EntityState.Modified)
                    .Select(e => new
                    {
                        Entity = e.Entity,
                        EntityName = e.Entity.GetType().Name,
                        State = e.State.ToString(),
                        ChangedProperties = e.Properties
                            .Where(p => p.IsModified)
                            .Select(p => new
                            {
                                PropertyName = p.Metadata.Name,
                                OriginalValue = p.OriginalValue?.ToString() ?? "null",
                                CurrentValue = p.CurrentValue?.ToString() ?? "null",
                                IsModified = p.IsModified,
                            }).ToList(),
                    }).ToList(),

                DeletedEntities = this._context.ChangeTracker.Entries()
                    .Where(x => x.State == EntityState.Deleted)
                    .Select(e => new
                    {
                        Entity = e.Entity,
                        EntityName = e.Entity.GetType().Name,
                        State = e.State.ToString(),
                    }).ToList(),

                UnchangedEntities = this._context.ChangeTracker.Entries()
                    .Where(x => x.State == EntityState.Unchanged)
                    .Select(e => new
                    {
                        Entity = e.Entity,
                        EntityName = e.Entity.GetType().Name,
                        State = e.State.ToString(),
                    }).ToList(),

                DetachedEntities = this._context.ChangeTracker.Entries()
                    .Where(x => x.State == EntityState.Detached)
                    .Select(e => new
                    {
                        Entity = e.Entity,
                        State = e.State.ToString(),
                        EntityName = e.Entity.GetType().Name,
                    }).ToList(),
            };


            // Get all added entities
            var addedEntities = this._context.ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added)
                .ToList();

          
            var test = this._context.ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added)
                .ToList();
            // Save changes to the database
            await this._context.SaveChangesAsync(cancellationToken);




            await this._context.Entry(entity!).ReloadAsync(cancellationToken);

            return entity;
        }

        public async Task SaveChangesSequenceAutoGenerator(CancellationToken cancellationToken)
        {
            var entities = this._context.ChangeTracker.Entries();
            var result = new
            {
                AddedEntities = this._context.ChangeTracker.Entries()
                    .Where(x => x.State == EntityState.Added)
                    .Select(e => new
                    {
                        Entity = e.Entity,
                        EntityName = e.Entity.GetType().Name,
                        State = e.State.ToString(),
                    }).ToList(),

                ModifiedEntities = this._context.ChangeTracker.Entries()
                    .Where(x => x.State == EntityState.Modified)
                    .Select(e => new
                    {
                        Entity = e.Entity,
                        EntityName = e.Entity.GetType().Name,
                        State = e.State.ToString(),
                        ChangedProperties = e.Properties
                            .Where(p => p.IsModified)
                            .Select(p => new
                            {
                                PropertyName = p.Metadata.Name,
                                OriginalValue = p.OriginalValue?.ToString() ?? "null",
                                CurrentValue = p.CurrentValue?.ToString() ?? "null",
                                IsModified = p.IsModified,
                            }).ToList(),
                    }).ToList(),

                DeletedEntities = this._context.ChangeTracker.Entries()
                    .Where(x => x.State == EntityState.Deleted)
                    .Select(e => new
                    {
                        Entity = e.Entity,
                        EntityName = e.Entity.GetType().Name,
                        State = e.State.ToString(),
                    }).ToList(),

                UnchangedEntities = this._context.ChangeTracker.Entries()
                    .Where(x => x.State == EntityState.Unchanged)
                    .Select(e => new
                    {
                        Entity = e.Entity,
                        EntityName = e.Entity.GetType().Name,
                        State = e.State.ToString(),
                    }).ToList(),

                DetachedEntities = this._context.ChangeTracker.Entries()
                    .Where(x => x.State == EntityState.Detached)
                    .Select(e => new
                    {
                        Entity = e.Entity,
                        State = e.State.ToString(),
                        EntityName = e.Entity.GetType().Name,
                    }).ToList(),
            };


          

            var test = this._context.ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added)
                .ToList();
            // Save changes to the database
            await this._context.SaveChangesAsync(cancellationToken);
        }

        // Helper method to get the primary key property dynamically
        private PropertyInfo? GetPrimaryKeyProperty(Type entityType)
        {
            // Use EF Core metadata to get the primary key property
            var entityTypeModel = this._context.Model.FindEntityType(entityType);
            var primaryKey = entityTypeModel?.FindPrimaryKey();

            // If we have composite primary keys, we should not assign any value to the primary key
            if (primaryKey?.Properties.Count > 1)
                return null;

            if (primaryKey != null)
            {
                // Get the name of the primary key property
                string? primaryKeyPropertyName = primaryKey.Properties.FirstOrDefault()?.Name;
                return entityType.GetProperty(primaryKeyPropertyName!);
            }

            return null;
        }

        // Method to check if the primary key value is the default value (0 for int/long or null)
        private bool IsDefaultPrimaryKeyValue(object? primaryKeyValue)
        {
            if (primaryKeyValue == null)
            {
                return true;
            }

            if (primaryKeyValue is int intValue && intValue == 0)
            {
                return true;
            }

            if (primaryKeyValue is byte byteValue && byteValue == 0)
            {
                return true;
            }

            if (primaryKeyValue is long longValue && longValue == 0)
            {
                return true;
            }

            return false;
        }


        // Set the primary key value dynamically based on its type
        private void SetPrimaryKeyValue(PropertyInfo primaryKeyProperty, object entity, long sequenceValue)
        {
            var propertyType = primaryKeyProperty.PropertyType;

            // Handle numeric types (int, long, byte)
            if (propertyType == typeof(int))
            {
                primaryKeyProperty.SetValue(entity, Convert.ToInt32(sequenceValue));
            }
            else if (propertyType == typeof(long))
            {
                primaryKeyProperty.SetValue(entity, sequenceValue);
            }
            else if (propertyType == typeof(byte))
            {
                primaryKeyProperty.SetValue(entity, Convert.ToByte(sequenceValue));
            }
            else
            {
                throw new InvalidOperationException($"Unsupported primary key type: {propertyType.Name}");
            }
        }


        public async Task SaveChanges(CancellationToken cancellationToken)

        {
            var entities = this._context.ChangeTracker.Entries();
            //// var taxP = this._context.Entry(test);
            ///


            var result = new
            {
                AddedEntities = entities
                    .Where(x => x.State == EntityState.Added)
                    .Select(e => new
                    {
                        EntityName = e.Entity.GetType().Name,
                        State = e.State.ToString(),
                    }).ToList(),

                ModifiedEntities = entities
                    .Where(x => x.State == EntityState.Modified)
                    .Select(e => new
                    {
                        EntityName = e.Entity.GetType().Name,
                        State = e.State.ToString(),
                        ChangedProperties = e.Properties
                            .Where(p => p.IsModified)
                            .Select(p => new
                            {
                                PropertyName = p.Metadata.Name,
                                OriginalValue = p.OriginalValue?.ToString() ?? "null",
                                CurrentValue = p.CurrentValue?.ToString() ?? "null",
                                IsModified = p.IsModified,
                            }).ToList(),
                    }).ToList(),

                DeletedEntities = entities
                    .Where(x => x.State == EntityState.Deleted)
                    .Select(e => new
                    {
                        EntityName = e.Entity.GetType().Name,
                        State = e.State.ToString(),
                    }).ToList(),

                UnchangedEntities = entities
                    .Where(x => x.State == EntityState.Unchanged)
                    .Select(e => new
                    {
                        EntityName = e.Entity.GetType().Name,
                        State = e.State.ToString(),
                    }).ToList(),

                DetachedEntities = entities
                    .Where(x => x.State == EntityState.Detached)
                    .Select(e => new
                    {
                        State = e.State.ToString(),
                        EntityName = e.Entity.GetType().Name,
                    }).ToList(),
            };

          

            int x = 12;

            // Now, you can inspect the 'result' object in debug mode to see all entities and their states


            var added = entities.Where(x => x.State == EntityState.Added).ToList();
            var modified = entities.Where(x => x.State == EntityState.Modified).ToList();
            var deleted = entities.Where(x => x.State == EntityState.Deleted).ToList();
            var unchanged = entities.Where(x => x.State == EntityState.Unchanged).ToList();
            var detached = entities.Where(x => x.State == EntityState.Detached).ToList();


            // var entriesWithState = this._context.ChangeTracker.Entries()
            //         .Select(e => new
            //         {
            //             Entity = e.Entity,
            //             State = e.State

            // })
            //         .ToList();


            // var entries = this._context.ChangeTracker.Entries();

            // var enitt = entries.Where(e => e.State == EntityState.Modified).ToList();

            // var entityList = this._context.ChangeTracker.Entries()
            //       .Where(e => e.State == EntityState.Added ||
            //                   e.State == EntityState.Modified ||
            //                   e.State == EntityState.Deleted ||
            //                   e.State == EntityState.Unchanged ||
            //                   e.State == EntityState.Detached).FirstOrDefault();
            // .ToList();

            //_context.ChangeTracker.AcceptAllChanges();
            await this._context.SaveChangesAsync(cancellationToken);
        }

        // private PropertyInfo GetPrimaryKeyProperty(Type entityType)
        // {
        //    // Get the entity's primary key property dynamically using EF Core metadata
        //    var entityTypeModel = Model.FindEntityType(entityType);
        //    var primaryKey = entityTypeModel.FindPrimaryKey();

        // // If the entity has a primary key, get the corresponding property info
        //    if (primaryKey != null)
        //    {
        //        var primaryKeyPropertyName = primaryKey.Properties.FirstOrDefault()?.Name;
        //        return entityType.GetProperty(primaryKeyPropertyName);
        //    }

        // return null;
        // }


      
      
        //public async Task<string> RunRawSqlQuery(string query)
        //{
        //    var connection = this._context.Database.GetDbConnection();
        //    if (connection.State == ConnectionState.Closed)
        //    {
        //        await connection.OpenAsync();
        //    }

        //    var result = await connection.QueryAsync<string>(query);
        //    return result.FirstOrDefault()!;
        //}

        // Starts a transaction
        public async Task StartTransactionAsync(CancellationToken cancellationToken)
        {
            // Ensure only one transaction is started at a time
            if (_currentTransaction == null)
            {
                _currentTransaction = await _context.Database.BeginTransactionAsync(cancellationToken);
            }
        }

        // Commits the transaction
        public async Task CommitTransactionAsync(CancellationToken cancellationToken)
        {
            try
            {
                await this._context.SaveChangesAsync(cancellationToken);
                if (_currentTransaction != null)
                {
                    await _currentTransaction.CommitAsync(cancellationToken);
                }
            }
            catch
            {
                await RollbackTransactionAsync(cancellationToken); // Rollback if commit fails
                throw; // Rethrow the exception to handle it upstream
            }
            finally
            {
                DisposeTransaction();
            }
        }

        // Rolls back the transaction
        public async Task RollbackTransactionAsync(CancellationToken cancellationToken)
        {
            if (_currentTransaction != null)
            {
                await _currentTransaction.RollbackAsync(cancellationToken);
            }

            DisposeTransaction();
        }

        // Dispose transaction resources
        private void DisposeTransaction()
        {
            if (_currentTransaction != null)
            {
                _currentTransaction.Dispose();
                _currentTransaction = null;
            }
        }

        public void ResetUnassignedProperties<T>(T entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            Type type = typeof(T);
            PropertyInfo[] properties = type.GetProperties();

            foreach (PropertyInfo property in properties)
            {
                if (!property.CanWrite) continue; // Skip read-only properties

                object? currentValue = property.GetValue(entity);
                Type propType = property.PropertyType;

                if (currentValue != null)
                {
                    // If the property is nullable and its value is zero, set to null
                    Type? underlyingType = Nullable.GetUnderlyingType(propType);
                    if (underlyingType != null && IsZeroValue(currentValue, underlyingType))
                    {
                        property.SetValue(entity, null);
                    }

                    continue; // Skip already assigned properties
                }

                if (Nullable.GetUnderlyingType(propType) != null)
                {
                    // If property is nullable, assign null
                    property.SetValue(entity, null);
                }
                else if (propType == typeof(string) && currentValue == null)
                {
                    // Only assign empty string if it's a non-nullable string
                    //property.SetValue(entity, string.Empty);
                }
                else if (propType.IsValueType)
                {
                    // Assign zero/default value to non-nullable value types
                    property.SetValue(entity, Activator.CreateInstance(propType));
                }
            }
        }

        private bool IsZeroValue(object value, Type type)
        {
            if (type == typeof(int)) return (int)value == 0;
            if (type == typeof(long)) return (long)value == 0;
            if (type == typeof(short)) return (short)value == 0;
            if (type == typeof(byte)) return (byte)value == 0;
            if (type == typeof(float)) return (float)value == 0f;
            if (type == typeof(double)) return (double)value == 0.0;
            if (type == typeof(decimal)) return (decimal)value == 0m;
            return false;
        }

        public void ResetUnassignedPropertiesWithZeroCheck<T>(T entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            Type type = typeof(T);
            PropertyInfo[] properties = type.GetProperties();

            foreach (PropertyInfo property in properties)
            {
                if (!property.CanWrite) continue; // Skip read-only properties

                object? currentValue = property.GetValue(entity);

                // If the property has been assigned a value, skip it
                if (currentValue != null) continue;

                Type propType = property.PropertyType;

                if (Nullable.GetUnderlyingType(propType) != null)
                {
                    // If property is nullable and the value is zero, assign null
                    if (currentValue is int intValue && intValue == 0)
                    {
                        property.SetValue(entity, null);
                    }
                    else if (currentValue is long longValue && longValue == 0)
                    {
                        property.SetValue(entity, null);
                    }
                    else if (currentValue is byte byteValue && byteValue == 0)
                    {
                        property.SetValue(entity, null);
                    }
                }
                else if (propType == typeof(string))
                {
                    // If it's a non-nullable string, assign an empty string
                    property.SetValue(entity, string.Empty);
                }
                else if (propType.IsValueType)
                {
                    // If it's a value type (int, double, bool, etc.), assign default (zero for numbers)
                    property.SetValue(entity, Activator.CreateInstance(propType));
                }
            }
        }
    }
}
