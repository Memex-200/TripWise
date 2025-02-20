using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripWise.Domain.Entities
{
    public class Offer
    {
        [Key]
        public int OfferCode { get; set; }

        [Required, StringLength(100)]
        public string OfferName { get; set; }

        public DateTime TimeCreated { get; set; }

        public DateTime ActiveFrom { get; set; }

        public DateTime ActiveTo { get; set; }

        public DateTime? TimeAccepted { get; set; }

        public bool Accepted { get; set; }

        public int? PromoOfferId { get; set; }

        public int AgentId { get; set; }

        public int CustomerId { get; set; }

        public Agent Agent { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual PromoOffer PromoOffer { get; set; }

        public virtual ICollection<Contract> Contracts { get; set; }

        public ICollection<OfferHotelService> OfferHotelServices { get; set; }

        public ICollection<OfferTransportService> OfferTransportServices { get; set; }

        [NotMapped]
        public object HotelServices { get; internal set; }
    }
}
