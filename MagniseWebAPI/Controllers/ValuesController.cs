using Database.Entities;
using MagniseWebAPI.Storages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MagniseWebAPI.Controllers
{
    /// <summary>
    /// Class controller for get currencies information
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ValuesController : ControllerBase
    {
        #region Fields
        /// <summary>
        /// Logger
        /// </summary>
        private readonly ILogger<ValuesController> _logger;
        #endregion
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logger">Logger</param>
        public ValuesController(ILogger<ValuesController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Get all currency values from data storage with prices
        /// </summary>
        /// <returns>List of currency values</returns>
        [HttpGet]
        public async Task<IEnumerable<CurrencyEntity>> Get()
        {
            return await DataStorage.Instance.GetAllCurrencyValues();
        }

        /// <summary>
        /// Get currency by identificator from data storage with prices
        /// </summary>
        /// <param name="id">Identificator of currency</param>
        /// <returns>List of data for one currency</returns>
        [HttpGet("{id}")]
        public async Task<IEnumerable<CurrencyEntity>> Get(String id)
        {
            return await DataStorage.Instance.GetSelectedCurrencyValues(new List<String>() { id }, null);
        }

        /// <summary>
        /// Get currency by identificator and provider from data storade with proces
        /// </summary>
        /// <param name="id">Identificator of currency</param>
        /// <param name="provider">Provider</param>
        /// <returns>Currency data</returns>
        [HttpGet("{id}/{provider}")]
        public async Task<CurrencyEntity> Get(String id, String provider)
        {
            IEnumerable<CurrencyEntity> result = await DataStorage.Instance.GetSelectedCurrencyValues(new List<String>() { id },
                new List<String>() { provider });

            return result.FirstOrDefault(item => item.Map == provider);
        }

        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
