using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TripWise.Domain.Entities
{
    public class Offer
    {
        [Key]
        public int OfferId { get; set; } 

        [Required, StringLength(100)]
        public string OfferName { get; set; }

        public string Description { get; set; } 

        public DateTime Created { get; set; } 

        public DateTime ActiveFrom { get; set; }

        public DateTime ActiveTo { get; set; } 

        public DateTime? TimeAccepted { get; set; }

        public bool IsAccepted { get; set; } 

        [ForeignKey("HotelService")]
        public int HotelServiceId { get; set; } 

        [ForeignKey("TransportCompany")]
        public int TransportCompanyId { get; set; } 

        [ForeignKey("PromoOffer")]
        public int? PromoOfferId { get; set; }

        [ForeignKey("Customer")]
        public int CustomerId { get; set; }

        public virtual HotelService HotelService { get; set; } 

        public virtual TransportCompany TransportCompany { get; set; } 

        public virtual Customer Customer { get; set; }

        public virtual PromoOffer PromoOffer { get; set; }

        public virtual ICollection<Contract> Contracts { get; set; }

       
    }
}