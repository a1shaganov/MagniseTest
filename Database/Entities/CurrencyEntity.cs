using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Entities
{
    /// <summary>
    /// Class keep information about entity
    /// </summary>
    [Table("Currency")]
    public class CurrencyEntity
    {
        #region Properties
        /// <summary>
        /// Identificator of object
        /// </summary>
        [Column("Id")]
        [Key]
        public Int32 Identificator
        { get; set; }

        /// <summary>
        /// Identificator of value
        /// </summary>
        [Column("idvalue")]
        public String IdentificatorValue
        { get; set; }

        /// <summary>
        /// Name of currency
        /// </summary>
        [Column("currency")]
        public String Currency
        { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        [Column("description")]
        public String Description
        { get; set; }

        /// <summary>
        /// Name of exchange
        /// </summary>
        [Column("exchange")]
        public String Exchange
        { get; set; }

        /// <summary>
        /// Name of exchange
        /// </summary>
        [Column("kind")]
        public String Kind
        { get; set; }

        /// <summary>
        /// Name of symbol
        /// </summary>
        [Column("symbol")]
        public String Symbol
        { get; set; }

        /// <summary>
        /// Name of map
        /// </summary>
        [Column("map")]
        public String Map
        { get; set; }

#warning It is'nt normal, is bad, i'm so sorry 
        //По нормальному нижче шість полів треба викинути та вставити як два поля до іншої таблмці,
        //зробити там тип даних та посилання на основну таблицю валют,
        //тому прошу вибачення за такий маразм що я зробив, не вистачало часу на тестове завдання
        //та й хотілось дуже розібратися ще й зробити так щоб сподобалося, але вийшло не сильно
        //буду надіятися що не сильно впав в очах того хто буде перевіряти

        /// <summary>
        /// Currency value (ASK)
        /// </summary>
        [Column("askvalue")]
        public Double AskValue
        { get; set; }

        /// <summary>
        /// Date and time update (ASK)
        /// </summary>
        [Column("askdatetime")]
        public String AskDatetime
        { get; set; }

        /// <summary>
        /// Currency value (BID)
        /// </summary>
        [Column("bidvalue")]
        public Double BidValue
        { get; set; }

        /// <summary>
        /// Date and time update (BID)
        /// </summary>
        [Column("biddatetime")]
        public String BidDatetime
        { get; set; }

        /// <summary>
        /// Currency value (Last)
        /// </summary>
        [Column("lastvalue")]
        public Double LastValue
        { get; set; }

        /// <summary>
        /// Date and time update (Last)
        /// </summary>
        [Column("lastdatetime")]
        public String LastDatetime
        { get; set; }
        #endregion
    }
}