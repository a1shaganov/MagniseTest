using Database.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Database
{
    public abstract class AbstractRepository<TEntity> : IDisposable where TEntity : class
    {
        #region Fields
        /// <summary>
        /// Database repository
        /// </summary>
        internal DatabaseRepository repository;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public AbstractRepository()
        {
            repository = new DatabaseRepository();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Add entity item to database
        /// </summary>
        /// <typeparam name="TEntity">Type of entity</typeparam>
        public abstract void AddEntityItem(TEntity entity);

        /// <summary>
        /// Remove entity item from database
        /// </summary>
        /// <param name="entity">Entity item</param>
        public abstract void RemoveEntityItem(TEntity entity);

        /// <summary>
        /// Get all entity items asynchronously
        /// </summary>
        /// <returns>Entity items</returns>
        public abstract Task<IEnumerable<TEntity>> GetAllEntityItemsAsync();

        /// <summary>
        /// Get all entity items
        /// </summary>
        /// <returns>Entity items</returns>
        public abstract IEnumerable<TEntity> GetAllEntityItems();

        /// <summary>
        /// Async get entity item by identificator asynchronously
        /// </summary>
        /// <param name="identificator">Entity item identificator</param>
        /// <returns>Entity item</returns>
        public abstract Task<TEntity> GetEntityItemByIdAsync(Int32 identificator);

        /// <summary>
        /// Get entity item by identificator
        /// </summary>
        /// <param name="identificator">Entity item identificator</param>
        /// <returns>Entity item</returns>
        public abstract TEntity GetEntityItemById(Int32 identificator);

        /// <summary>
        /// Async get entity item by name asynchronously
        /// </summary>
        /// <param name="identificatorValue">Name of entity item</param>
        /// <returns>Entity item</returns>
        public abstract Task<TEntity> GetEntityItemBySpecialIdAsync(String identificatorValue);

        /// <summary>
        /// Get entity item by name
        /// </summary>
        /// <param name="identificatorValue">Name of entity item</param>
        /// <returns>Entity item</returns>
        public abstract TEntity GetEntityItemBySpecialId(String identificatorValue);

        /// <summary>
        /// Async get entity item by name asynchronously
        /// </summary>
        /// <param name="identificatorValue">Name of entity item</param>
        /// <returns>Entity item</returns>
        public abstract Task<IEnumerable<TEntity>> GetEntityItemBySpecialsIdAsync(String identificatorValue);

        /// <summary>
        /// Get entity item by name
        /// </summary>
        /// <param name="identificatorValue">Name of entity item</param>
        /// <returns>Entity item</returns>
        public abstract IEnumerable<TEntity> GetEntityItemBySpeciaslId(String identificatorValue);

        /// <summary>
        /// Save data to database
        /// </summary>
        /// <returns>Result execution</returns>
        public virtual async Task SaveDataToDatabaseAsync()
        {
            await repository.SaveChangesAsync();
        }

        /// <summary>
        /// Save data to database
        /// </summary>
        public virtual void SaveDataToDatabase()
        {
            repository.SaveChanges();
        }

        /// <summary>
        /// Dispose all variables in class
        /// </summary>
        public virtual void Dispose()
        {
            if (repository != null)
                repository.Dispose();
        }
        #endregion
    }
}