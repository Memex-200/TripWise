using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TripWise.Domain.Entities
{
    public class Offer
    {
        [Key]
        public int OfferId { get; set; } // Renamed to OfferId for consistency

        [Required, StringLength(100)]
        public string OfferName { get; set; }

        public string Description { get; set; } // Added to match schema

        public DateTime Created { get; set; } // Renamed to match schema

        public DateTime ActiveFrom { get; set; }

        public bool ActiveTo { get; set; } // Changed to bool to match schema

        public DateTime? TimeAccepted { get; set; }

        public bool IsAccepted { get; set; } // Renamed to match schema

        [NotMapped]
        public string Image { get; set; } // Marked as NotMapped

        [ForeignKey("HotelService")]
        public int HotelServiceId { get; set; } // Added to match schema

        [ForeignKey("TransportCompany")]
        public int TransportCompanyId { get; set; } // Added to match schema

        [ForeignKey("PromoOffer")]
        public int? PromoOfferId { get; set; }

        [ForeignKey("Customer")]
        public int CustomerId { get; set; }

        public virtual HotelService HotelService { get; set; } // Added for one-to-one relationship

        public virtual TransportCompany TransportCompany { get; set; } // Added for one-to-one relationship

        public virtual Customer Customer { get; set; }

        public virtual PromoOffer PromoOffer { get; set; }

        public virtual ICollection<Contract> Contracts { get; set; }

        [NotMapped]
        public object HotelServices { get; internal set; }
    }
}