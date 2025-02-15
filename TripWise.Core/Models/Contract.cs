using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripWise.EntityFramework.Models;

namespace TripWise.Core.Models
{
    public class Contract
    {
        [Key]
        public int ContractCode { get; set; }

        public int CustomerId { get; set; }

        public int AgentId { get; set; }

        public int OfferCode { get; set; }

        public DateTime TimeSigned { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalPrice { get; set; }

        public DateTime PaymentDate { get; set; }

        public TimeSpan PaymentTime { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal PaymentAmount { get; set; }

        public bool Refunded { get; set; }

        public DateTime? RefundedTime { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? RefundedAmount { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual Agent Agent { get; set; }

        public virtual Offer Offer { get; set; }

    }
}
