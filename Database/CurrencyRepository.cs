using Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Database
{
    /// <summary>
    /// Class repository for currencies
    /// </summary>
    public class CurrencyRepository : AbstractRepository<CurrencyEntity>
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public CurrencyRepository()
        { }
        #endregion

        #region Methods
        /// <summary>
        /// Add currency to database
        /// </summary>
        /// <param name="entity">Extity item</param>
        /// <exception cref="ArgumentNullException">Exception describe null warrior</exception>
        public override void AddEntityItem(CurrencyEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException("Currency is null");

            repository.Currency.Add(entity);
        }

        /// <summary>
        /// Remove currency from database
        /// </summary>
        /// <param name="entity">Entity item</param>
        /// <exception cref="ArgumentNullException">Exception describe null warrior</exception>
        public override void RemoveEntityItem(CurrencyEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException("Currency is null");

            repository.Currency.Remove(entity);
        }

        /// <summary>
        /// Get all currencies asynchronously
        /// </summary>
        /// <returns>Entity items</returns>
        public override async Task<IEnumerable<CurrencyEntity>> GetAllEntityItemsAsync()
        {
            return await Task.Run(() => GetAllEntityItems());
        }

        /// <summary>
        /// Get all currencies
        /// </summary>
        /// <returns>Entity items</returns>
        public override IEnumerable<CurrencyEntity> GetAllEntityItems()
        {
            return repository.Currency.ToList();
        }

        /// <summary>
        /// Async get currency by identificator asynchronously
        /// </summary>
        /// <param name="identificator">Entity item identificator</param>
        /// <returns>Entity item</returns>
        public override async Task<CurrencyEntity> GetEntityItemByIdAsync(Int32 identificator)
        {
            return await Task.Run(() => GetEntityItemById(identificator));
        }

        /// <summary>
        /// Get currency by identificator
        /// </summary>
        /// <param name="identificator">Entity item identificator</param>
        /// <returns>Entity item</returns>
        /// <exception cref="ArgumentNullException">Exception describe null warrior</exception>
        public override CurrencyEntity GetEntityItemById(Int32 identificator)
        {
            CurrencyEntity entity = null;
            entity = repository.Currency.FirstOrDefault(item => item.Identificator == identificator);

            if (entity == null)
                throw new ArgumentNullException("Currency isn't exist");

            return entity;
        }

        /// <summary>
        /// Async get currency by identificator value asynchronously
        /// </summary>
        /// <param name="identificatorValue">Identificator value of entity item</param>
        /// <returns>Entity item</returns>
        public override async Task<CurrencyEntity> GetEntityItemBySpecialIdAsync(String identificatorValue)
        {
            return await Task.Run(() => GetEntityItemBySpecialId(identificatorValue));
        }

        /// <summary>
        /// Get warrior by identificator value
        /// </summary>
        /// <param name="identificatorValue">Identificator value of entity item</param>
        /// <returns>Entity item</returns>
        /// <exception cref="ArgumentNullException">Exception describe null currency</exception>
        public override CurrencyEntity GetEntityItemBySpecialId(String identificatorValue)
        {
            CurrencyEntity entity = repository.Currency.FirstOrDefault(item => item.IdentificatorValue == identificatorValue);

            if (entity == null)
                throw new ArgumentNullException("Currency isn't exist");

            return entity;
        }

        /// <summary>
        /// Async get entity item by name asynchronously
        /// </summary>
        /// <param name="identificatorValue">Name of entity item</param>
        /// <returns>Entity item</returns>
        public override async Task<IEnumerable<CurrencyEntity>> GetEntityItemBySpecialsIdAsync(String identificatorValue)
        {
            return await Task.Run(() => GetEntityItemBySpeciaslId(identificatorValue));
        }

        /// <summary>
        /// Get entity item by name
        /// </summary>
        /// <param name="identificatorValue">Name of entity item</param>
        /// <returns>Entity item</returns>
        public override IEnumerable<CurrencyEntity> GetEntityItemBySpeciaslId(String identificatorValue)
        {
            IEnumerable<CurrencyEntity> entities = repository.Currency.Where(item => item.IdentificatorValue == identificatorValue).ToList();

            if (entities == null)
                throw new ArgumentNullException("Currencise arent't exist");

            return entities;
        }
        #endregion
    }
}